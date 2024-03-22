using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Market.Interfaces;
using _Project.CodeBase.UI.Common;
using _Project.CodeBase.UI.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class MarketWindow : MonoWindow
    {
        [SerializeField] private GameObject _fieldPrefab;
        [SerializeField] private GameObject _context;
        
        private Dictionary<string, IResource> _resources;
        private IInventory _inventory;
        private IMarket _market;
        private IWindowsManager _windowManager;
        private WindowId _windowId;
        
        private List<GameObject> _spawnedFields = new();

        [Inject]
        private void GetDependencies(
            Dictionary<string, IResource> resources,
            IInventory inventory,
            IMarket market,
            IWindowsManager windowsManager,
            WindowId windowId)
        {
            _resources = resources;
            _inventory = inventory;
            _market = market;
            _windowManager = windowsManager;
            _windowId = windowId;
        }
        
        public override void Show()
        {
            base.Show();

            foreach (KeyValuePair<string,int> resource in _inventory.GetInventory())
            {
                if (!_market.IsSellable(resource.Key)) continue;
                
                GameObject spawnedField = GameObject.Instantiate(_fieldPrefab, Vector3.zero, Quaternion.identity, _context.transform);
                _spawnedFields.Add(spawnedField);
                spawnedField.SetActive(true);
                var textFields = spawnedField.GetComponentsInChildren<TextMeshProUGUI>();
                var buttons = spawnedField.GetComponentsInChildren<IButton>();
                textFields[0].text = _resources[resource.Key].Name;
                textFields[1].text = $"You have: {resource.Value.ToString()}";
                textFields[2].text = $"Price: {_market.GetPrice(resource.Key).ToString()}";
                buttons[0].OnClick += (_) => Sell(resource.Key, 1);
                buttons[1].OnClick += (_) => Sell(resource.Key, 10);
            }
        }
        
        public override void Hide()
        {
            foreach (GameObject field in _spawnedFields)
            {
                GameObject.Destroy(field);
            }
            base.Hide();
        }

        private void Sell(string resourceKey, int amount)
        {
            if (_market.TrySell(resourceKey, amount))
            {
                Close();
            }
            else
            {
                #if UNITY_EDITOR
                Debug.LogWarning("Not enough resources to sell.");
                #endif
            }
        }

        private void Close()
        {
            _windowManager.ShowWindow(_windowId.GameUI);
            _windowManager.HideWindow(GetId());
        }
    }
}