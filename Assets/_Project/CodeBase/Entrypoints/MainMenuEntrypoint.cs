using System;
using _Project.CodeBase.UI.Common;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Entrypoints
{
    public class MainMenuEntrypoint : MonoBehaviour
    {
        private IWindowsManager _windowsManager;
        private WindowId _windowId;

        [Inject]
        private void GetDependencies(IWindowsManager windowsManager, WindowId windowId)
        {
            _windowsManager = windowsManager;
            _windowId = windowId;
        }
        
        private void Start()
        {
            _windowsManager.ShowWindow(_windowId.MainMenu);
        }
    }
}