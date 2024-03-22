using _Project.CodeBase.Services.SceneLoading.Interfaces;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Reactions
{
    public class SwitchSceneReaction : MonoBehaviour, IReaction
    {
        [SerializeField] private string _sceneName;
        
        private ISceneLoader _sceneLoader;

        [Inject]
        private void GetDependencies(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void React(IUIEntity entity)
        {
            _sceneLoader.LoadScene(_sceneName);
        }
    }
}