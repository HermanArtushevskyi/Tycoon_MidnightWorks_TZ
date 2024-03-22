using _Project.CodeBase.Common;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.UI.Common;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Entrypoints
{
    public class GameEntrypoint : MonoBehaviour
    {
        private Factories.Interfaces.IFactory<IMap, string> _mapFactory;
        private Factories.Interfaces.IFactory<IInventory, string> _inventoryFactory;
        private IWindowsManager _windowsManager;
        private WindowId _windowId;
        private GameInfo _gameInfo;

        [Inject]
        private void GetDependencies(
            Factories.Interfaces.IFactory<IMap, string> mapFactory,
            Factories.Interfaces.IFactory<IInventory, string> inventoryFactory,
            IWindowsManager windowsManager,
            WindowId windowId,
            GameInfo gameInfo)
        {
            _mapFactory = mapFactory;
            _inventoryFactory = inventoryFactory;
            _windowsManager = windowsManager;
            _windowId = windowId;
            _gameInfo = gameInfo;
        }
        
        private void Start()
        {
            if (_gameInfo.IsLoadedGame == false)
            {
                _inventoryFactory.Create(null);
                _mapFactory.Create(null);
            }
            else
            {
                _inventoryFactory.Create(_gameInfo.GameName);
                _mapFactory.Create(_gameInfo.GameName);
            }

            _windowsManager.ShowWindow(_windowId.GameUI);
        }
    }
}