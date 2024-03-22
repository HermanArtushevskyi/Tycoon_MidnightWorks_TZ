using System;
using System.Collections.Generic;
using System.Reflection;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using Zenject;

namespace _Project.CodeBase.Services.Saving
{
    public class JsonSaver : SaverBase
    {
        private Dictionary<Type, IMiddleware> _middlewares = new();
        private IMiddleware _defaultMiddleware;
        
        public JsonSaver(SavingConfig config,
            [InjectOptional(Id = typeof(IInventory))] IMiddleware inventoryMiddleware,
            [InjectOptional(Id = typeof(IMap))] IMiddleware mapMiddleware,
            [InjectOptional(Id = SaveMethod.Json)] IMiddleware jsonMiddleware) : base(config)
        {
            _defaultMiddleware = jsonMiddleware;
            _middlewares.Add(typeof(IInventory), inventoryMiddleware);
            _middlewares.Add(typeof(IMap), mapMiddleware);
        }

        public override bool Load<T>(out T result, string saveName = null)
        {
            if (_middlewares.TryGetValue(typeof(T), out IMiddleware middleware))
            {
                result = (T) middleware.Load(GetFilePath(saveName, typeof(T).GetCustomAttribute<SavableAttribute>()));
                return true;
            }
            else
            {
                result = (T) _defaultMiddleware.Load(GetFilePath(saveName, typeof(T).GetCustomAttribute<SavableAttribute>()));
                return true;
            }
        }

        public override bool Save<T>(object data, string saveName = null)
        {
            if (_middlewares.TryGetValue(typeof(T), out IMiddleware middleware))
            {
                middleware.Save(data, GetFilePath(saveName, typeof(T).GetCustomAttribute<SavableAttribute>()));
                return true;
            }
            else
            {
                _defaultMiddleware.Save(data, GetFilePath(saveName, typeof(T).GetCustomAttribute<SavableAttribute>()));
                return true;
            }
        }

        private string GetFilePath(string saveName, SavableAttribute savableAttribute)
        {
            string filePath;
            filePath = saveName == null ? Config.SavePath + savableAttribute.Key : Config.SavePath + savableAttribute.Key + saveName;
            return filePath;
        }
    }
}