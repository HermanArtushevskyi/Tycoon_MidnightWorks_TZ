using System.Reflection;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;

namespace _Project.CodeBase.Services.Saving
{
    public abstract class SaverBase : ILoader, ISaver
    {
        protected SavingConfig Config;
        
        protected SaverBase(SavingConfig config)
        {
            Config = config;
        }

        public abstract bool Load<T>(out T result, string saveName = null) where T : class;

        public abstract bool Save<T>(object data, string saveName = null);
    }
}