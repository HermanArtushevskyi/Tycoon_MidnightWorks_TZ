using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.UI.Common;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.CodeBase.GameFlow.Buildings
{
    public class FactoryBuilding : Building, IPointerClickHandler
    {
        private IWindowsManager _windowsManager;
        private WindowId _windowId;
        private IMap _map;

        [Inject]
        private void GetDependencies(IWindowsManager windowsManager, WindowId windowId, IMap map)
        {
            _windowsManager = windowsManager;
            _windowId = windowId;
            _map = map;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            IBuilding building = null;
            Vector2Int hexPos = new();

            for (int x = 0; x < _map.Hexes.GetLength(0); x++)
            {
                for (int y = 0; y < _map.Hexes.GetLength(1); y++)
                {
                    if (ReferenceEquals(_map.GetHex(x, y).Building, this))
                    {
                        building = _map.GetHex(x, y).Building;
                        hexPos = new Vector2Int(x, y);
                        break;
                    }
                }
            }
            
            _windowsManager.ShowWindow(_windowId.BuildingInfo, (building, hexPos));
        }
    }
}