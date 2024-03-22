using System.Collections.Generic;
using _Project.CodeBase.Factories.Interfaces;
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
        private IFactory<IBuilding, string, Vector3, Quaternion> _buildingFactory;
        private Dictionary<string, IResource> _resources;
        private IMap _map;
        private IInventory _inventory;
        private IWindowsManager _windowsManager;

        private Vector2Int HexCoords => (Vector2Int) EntityData;
        private List<GameObject> _spawnedFields = new();

        [Inject]
        private void GetDependencies(
            [InjectOptional(Id = typeof(IBuilding))] Dictionary<string, GameObject> buildingPrefabs,
            IFactory<IBuilding, string, Vector3, Quaternion> buildingFactory,
            Dictionary<string, IResource> resources,
            IMap map,
            IInventory inventory,
            IWindowsManager windowsManager)
        {
            _buildingPrefabs = buildingPrefabs;
            _buildingFactory = buildingFactory;
            _resources = resources;
            _map = map;
            _inventory = inventory;
            _windowsManager = windowsManager;
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
                    field.SetActive(true);
                    _spawnedFields.Add(field);
                    var textFields = field.GetComponentsInChildren<TextMeshProUGUI>();
                    textFields[0].text = building.Name;
                    textFields[1].text = $"USD: {building.Cost[_resources["cash_usd"]]}";
                    field.GetComponentInChildren<IButton>().OnClick += (_) => Build(buildingPrefab.Key);
                }
            }
        }

        public override void Hide()
        {
            base.Hide();

            foreach (GameObject field in _spawnedFields) 
            {
                GameObject.Destroy(field);
            }
            
            _spawnedFields.Clear();
        }
        private void Build(string id)
        {
            IBuilding building = _buildingPrefabs[id].GetComponent<IBuilding>();

            foreach (KeyValuePair<IResource,int> resource in building.Cost)
            {
                if (_inventory.GetAmount(resource.Key.Id) < resource.Value)
                {
                    Debug.Log($"Not enough {resource.Key.Id}");
                    return;
                }
                else
                {
                    _inventory.RemoveResource(resource.Key.Id, resource.Value);
                }
            }
            
            IHex hex = _map.GetHex(HexCoords.x, HexCoords.y);
            hex.SetBuilding(_buildingFactory.Create(id, new Vector3(HexCoords.x, 0f, HexCoords.y), Quaternion.identity));
            _windowsManager.HideWindow(GetId());
        }
    }
}