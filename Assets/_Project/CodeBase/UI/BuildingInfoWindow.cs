using System.Collections.Generic;
using _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.UI.Common;
using _Project.CodeBase.UI.Interfaces;
using TMPro;
using TNRD;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Reactions
{
    public class BuildingInfoWindow : MonoWindow
    {
        [SerializeField] private TextMeshProUGUI _buildingNameText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private SerializableInterface<IButton> _upgradeButton;
        
        private IFactory<IBuilding, string, Vector3, Quaternion> _buildingFactory;
        private Dictionary<string, IResource> _resources;
        private IInventory _inventory;
        private IWindowsManager _windowsManager;
        private WindowId _windowId;
        private IMap _map;

        private const string USD_ID = "cash_usd";

        private (IBuilding, Vector2Int) Data => ((IBuilding, Vector2Int)) EntityData;

        [Inject]
        private void GetDependencies(
            IFactory<IBuilding, string, Vector3, Quaternion> buildingFactory,
            Dictionary<string, IResource> resources,
            IInventory inventory,
            IWindowsManager windowsManager,
            WindowId windowId,
            IMap map)
        {
            _buildingFactory = buildingFactory;
            _resources = resources;
            _inventory = inventory;
            _windowsManager = windowsManager;
            _windowId = windowId;
            _map = map;
        }

        public override void Show()
        {
            IBuilding building = Data.Item1;
            base.Show();
            _buildingNameText.text = building.Name;
            if (building.CanBeUpgraded)
            {
                _costText.text = building.UpgradeCost[_resources[USD_ID]].ToString();
                _upgradeButton.Value.OnClick += UpgradeBuilding;
                _upgradeButton.Value.GameObject.SetActive(true);
            }

            else
            {
                _costText.text = "Can not be upgraded";
                _upgradeButton.Value.GameObject.SetActive(false);
            }
        }

        public override void Hide()
        {
            _upgradeButton.Value.OnClick -= UpgradeBuilding;
            GameObject.SetActive(false);
        }

        private void UpgradeBuilding(IButton _)
        {
            int cost = Data.Item1.UpgradeCost[_resources[USD_ID]];
            if (_inventory.GetAmount(USD_ID) < cost) return;
            _inventory.RemoveResource(USD_ID, cost);
            Vector3 buildingPos = new Vector3(Data.Item2.x, 0f, Data.Item2.y);
            IBuilding newBuilding = _buildingFactory.Create(Data.Item1.NextBuilding.Id, buildingPos, Quaternion.identity);
            IHex hex = _map.GetHex((int) buildingPos.x, (int) buildingPos.z);
            hex.SetBuilding(newBuilding);
            Close();
        }

        private void Close()
        {
            _windowsManager.HideWindow(_windowId.BuildingInfo);
        }
    }
}