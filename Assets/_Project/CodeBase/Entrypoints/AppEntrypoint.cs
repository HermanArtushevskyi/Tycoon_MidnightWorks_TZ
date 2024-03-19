using _Project.CodeBase.Services.SceneLoading.Common;
using _Project.CodeBase.Services.SceneLoading.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Entrypoints
{
    public class AppEntrypoint : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void GetDependencies(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Start()
        {
            _sceneLoader.LoadScene(SceneId.MainMenu.ToSceneName());
        }
    }
}