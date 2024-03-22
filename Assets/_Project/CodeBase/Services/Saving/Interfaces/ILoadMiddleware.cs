using System;

namespace _Project.CodeBase.Services.Saving.Interfaces
{
    public interface ILoadMiddleware
    {
        public object Load(string saveName);
    }
}