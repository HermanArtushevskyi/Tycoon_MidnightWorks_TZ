using System;
using _Project.CodeBase.Services.SceneLoading.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader, ISceneEvents
    {
        public event Action<string, LoadSceneMode> OnLoadingStarted;
        public event Action<string, LoadSceneMode> OnSceneLoaded;

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            OnLoadingStarted?.Invoke(sceneName, mode);
            SceneManager.LoadScene(sceneName, mode);
            OnSceneLoaded?.Invoke(sceneName, mode);
        }

        public async UniTask<AsyncOperation> LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, IProgress<float> progress = null,
            Action<string, LoadSceneMode> onLoaded = null)
        {
            OnLoadingStarted?.Invoke(sceneName, mode);
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                progress?.Report(asyncOperation.progress);
                
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                
                await UniTask.Yield();
            }

            onLoaded?.Invoke(sceneName, mode);
            OnSceneLoaded?.Invoke(sceneName, mode);
            
            return asyncOperation;
        }

        public async UniTask<AsyncOperation> UnloadSceneAsync(string sceneName, Action<string> onUnloaded = null)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }

            onUnloaded?.Invoke(sceneName);
            return asyncOperation;
        }
    }
}