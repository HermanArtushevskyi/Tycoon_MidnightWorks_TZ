using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Reactions
{
    public class OpenWindowReaction : MonoBehaviour, IReaction
    {
        [SerializeField] private string _windowId;
        
        private IWindowsManager _windowManager;

        [Inject]
        private void GetDependencies(IWindowsManager windowManager)
        {
            _windowManager = windowManager;
        }

        public void React(IUIEntity entity)
        {
            _windowManager.ShowWindow(_windowId);
        }
    }
}