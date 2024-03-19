using System.IO;
using System.Reflection;
using _Project.CodeBase.Services.Saving.Common;
using UnityEngine;

namespace _Project.CodeBase.Services.Saving
{
    public class JsonSaver : SaverBase
    {
        public JsonSaver(SavingConfig config) : base(config)
        {
        }

        public override bool Load<T>(out T result, string saveName = null)
        {
            result = default;
            
            SavableAttribute savableAttribute = typeof(T).GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;
            
            string filePath;
            filePath = saveName == null ? Config.savePath + savableAttribute.Key : Config.savePath + saveName;
            
            if (!File.Exists(filePath))
                return false;
            
            string json = File.ReadAllText(filePath);
            result = JsonUtility.FromJson<T>(json);
            return true;
        }

        public override bool Save(object data)
        {
            SavableAttribute savableAttribute = data.GetType().GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Config.SavePath + savableAttribute.Key, json);
            return true;
        }
    }
}