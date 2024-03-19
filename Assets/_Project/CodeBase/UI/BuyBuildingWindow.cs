using System.Collections.Generic;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.UI.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class BuyBuildingWindow : MonoWindow
    {
        [SerializeField] private GameObject _fieldPrefab;
        [SerializeField] private GameObject _context;
        
        private Dictionary<string, GameObject> _buildingPrefabs;
        private Dictionary<string, IResource> _resources;
        private IMap _map;
        private IInventory _inventory;

        private int _x;
        private int _y;

        [Inject]
        private void GetDependencies(
            [InjectOptional(Id = typeof(IBuilding))] Dictionary<string, GameObject> buildingPrefabs,
            Dictionary<string, IResource> resources,
            IMap map,
            IInventory inventory)
        {
            _buildingPrefabs = buildingPrefabs;
            _resources = resources;
            _map = map;
            _inventory = inventory;
        }

        public override void Show()
        {
            base.Show();

            foreach (KeyValuePair<string,GameObject> buildingPrefab in _buildingPrefabs)
            {
                IBuilding building = buildingPrefab.Value.GetComponent<IBuilding>();

                if (building.Cost.Keys.Count > 0)
                {
                    GameObject field = GameObject.Instantiate(_fieldPrefab, _context.transform);
                    var textFields = field.GetComponentsInChildren<TextMeshProUGUI>();
                    textFields[0].text = building.Name;
                    textFields[1].text = $"USD: {building.Cost[_resources["cash_usd"]]}";
                    field.GetComponent<IButton>().OnClick += (_) => Build(buildingPrefab.Key);
                }
            }
        }

        public override void Hide()
        {
            base.Hide();
        }
        private void Build(string id)
        {
            
        }
    }
}