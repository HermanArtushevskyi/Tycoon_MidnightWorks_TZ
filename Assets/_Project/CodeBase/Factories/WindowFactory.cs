using System.Collections.Generic;
using IFactories = _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.UI;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Factories
{
    public class WindowFactory : IFactories.IFactory<IWindow, string>
    {
        private readonly Dictionary<string, GameObject> _windowPrefabs;
        
        private Dictionary<string, IWindow> _spawnedWindows = new();

        public WindowFactory(
            [InjectOptional(Id = typeof(IWindow))] Dictionary<string, GameObject> windowPrefabs)
        {
            _windowPrefabs = windowPrefabs;
        }
        
        public IWindow Create(string windowId)
        {
            if (_spawnedWindows.ContainsKey(windowId))
                return _spawnedWindows[windowId];

            IWindow window = GameObject.Instantiate(_windowPrefabs[windowId]).GetComponent<MonoWindow>();
            
            _spawnedWindows.Add(windowId, window);
            _spawnedWindows[windowId].OnGameObjectDestroy += OnGameObjectWindowDestroyed;
            return window;
        }

        private void OnGameObjectWindowDestroyed(IUIEntity entity)
        {
            IWindow window = entity as IWindow;
            window.OnGameObjectDestroy -= OnGameObjectWindowDestroyed;
            _spawnedWindows.Remove(window.GetId());
        }
    }
}