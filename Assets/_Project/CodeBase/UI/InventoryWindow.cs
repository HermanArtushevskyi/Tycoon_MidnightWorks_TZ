using System.Collections.Generic;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class InventoryWindow : MonoWindow
    {
        [SerializeField] private GameObject _inventoryFieldPrefab;
        [SerializeField] private GameObject _context;
        private IInventory _inventory;
        private List<GameObject> _spawnedFields = new();
        private Dictionary<string, IResource> _resourcePrefabs;

        [Inject]
        private void GetDependencies(IInventory inventory, Dictionary<string, IResource> resourcePrefabs)
        {
            _inventory = inventory;
            _resourcePrefabs = resourcePrefabs;
        }

        public override void Show()
        {
            base.Show();
            foreach (var resource in _inventory.GetInventory())
            {
                GameObject spawnedField = GameObject.Instantiate(_inventoryFieldPrefab, Vector3.zero,
                    Quaternion.identity, _context.transform);
                spawnedField.SetActive(true);
                _spawnedFields.Add(spawnedField);
                spawnedField.GetComponentInChildren<TextMeshProUGUI>().text = $"{_resourcePrefabs[resource.Key].Name} = {resource.Value}";
            }
        }

        public override void Hide()
        {
            base.Hide();
            foreach (GameObject field in _spawnedFields)
            {
                GameObject.Destroy(field);
            }
        }
    }
}