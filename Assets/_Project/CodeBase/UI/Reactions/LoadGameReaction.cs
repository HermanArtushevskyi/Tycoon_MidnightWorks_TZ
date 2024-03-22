using _Project.CodeBase.Common;
using _Project.CodeBase.Services.SceneLoading.Common;
using _Project.CodeBase.Services.SceneLoading.Interfaces;
using _Project.CodeBase.UI.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Reactions
{
    public class LoadGameReaction : MonoBehaviour, IReaction
    {
        private ISceneLoader _sceneLoader;
        private GameInfo _info;

        [SerializeField] private TMP_InputField _inputField;

        [Inject]
        private void GetDependencies(ISceneLoader sceneLoader, GameInfo info)
        {
            _sceneLoader = sceneLoader;
            _info = info;
        }


        public void React(IUIEntity entity)
        {
            _info.GameName = _inputField.text;
            _info.IsLoadedGame = true;
            _sceneLoader.LoadSceneAsync(SceneId.GameScene.ToSceneName());
        }
    }
}