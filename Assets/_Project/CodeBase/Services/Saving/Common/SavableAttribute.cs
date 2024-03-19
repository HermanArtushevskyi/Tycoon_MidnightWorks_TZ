using System;

namespace _Project.CodeBase.Services.Saving.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SavableAttribute : Attribute
    {
        public readonly string Key;
        public readonly SaveMethod SaveMethod;
        
        public SavableAttribute(string key, SaveMethod saveMethod = SaveMethod.Json)
        {
            Key = key;
            SaveMethod = saveMethod;
        }
    }
}