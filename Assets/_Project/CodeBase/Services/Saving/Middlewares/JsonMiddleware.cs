using System;
using System.IO;
using _Project.CodeBase.Services.Saving.Interfaces;
using Newtonsoft.Json;

namespace _Project.CodeBase.Services.Saving.Middlewares
{
    public class JsonMiddleware : IMiddleware
    {
        public bool IsLoadApplicable(Type data)
        {
            return true;
        }

        public object Load(string saveName)
        {
            if (File.Exists(saveName))
            {
                var json = File.ReadAllText(saveName);
                return JsonConvert.DeserializeObject<object>(json);
            }

            return null;
        }

        public bool IsSaveApplicable(object data)
        {
            return true;
        }

        public void Save(object data, string saveName)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(saveName, json);
        }
    }
}