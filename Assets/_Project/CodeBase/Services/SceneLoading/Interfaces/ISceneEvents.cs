using System;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Services.SceneLoading.Interfaces
{
    public interface ISceneEvents
    {
        public event Action<string, LoadSceneMode> OnLoadingStarted;
        public event Action<string, LoadSceneMode> OnSceneLoaded;
    }
}