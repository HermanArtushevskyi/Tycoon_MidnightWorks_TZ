using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Services.SceneLoading.Interfaces
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        public UniTask<AsyncOperation> LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single,
            IProgress<float> progress = null, Action<string, LoadSceneMode> onLoaded = null);
        public UniTask<AsyncOperation> UnloadSceneAsync(string sceneName, Action<string> onUnloaded = null);
    }
}