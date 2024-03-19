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

        [Inject]
        private void GetDependencies(
            Factories.Interfaces.IFactory<IMap, string> mapFactory,
            Factories.Interfaces.IFactory<IInventory, string> inventoryFactory,
            IWindowsManager windowsManager,
            WindowId windowId)
        {
            _mapFactory = mapFactory;
            _inventoryFactory = inventoryFactory;
            _windowsManager = windowsManager;
            _windowId = windowId;
        }
        
        private void Start()
        {
            _inventoryFactory.Create(null);
            _windowsManager.ShowWindow(_windowId.GameUI);
            IMap map = _mapFactory.Create(null);
        }
    }
}