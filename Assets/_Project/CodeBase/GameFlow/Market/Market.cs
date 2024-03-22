using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Market.Common;
using _Project.CodeBase.GameFlow.Market.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameFlow.Market
{
    public class Market : IMarket, ITickable
    {
        private readonly MarketConfig _marketConfig;
        private readonly Dictionary<string, IResource> _resources;
        private readonly IInventory _inventory;

        private float _timeFromLastDeviation = 0f;
        private float _currentDeviation = 0f;

        public Market(MarketConfig marketConfig, Dictionary<string, IResource> resources, IInventory inventory)
        {
            _marketConfig = marketConfig;
            _resources = resources;
            _inventory = inventory;
        }

        public string[] GetSellableResources()
        {
            List<string> sellableIds = new();

            foreach (IResource resource in _marketConfig.Prices.Keys)
            {
                sellableIds.Add(resource.Id);
            }

            return sellableIds.ToArray();
        }

        public bool IsSellable(string resourceId)
        {
            return _marketConfig.Prices.ContainsKey(_resources[resourceId]);
        }

        public int GetPrice(string resourceId)
        {
            int price = Mathf.RoundToInt(_marketConfig.Prices[_resources[resourceId]] * (1 + _currentDeviation));
            if (price <= 0) price = 1;
            return price;
        }

        public bool TrySell(string resourceId, int amount)
        {
            if (_inventory.GetAmount(resourceId) < amount)
            {
                return false;
            }
            
            _inventory.RemoveResource(resourceId, amount);
            _inventory.AddResource(_marketConfig.cashResourceId, GetPrice(resourceId) * amount);
            
            return true;
        }

        public void Tick()
        {
            _timeFromLastDeviation += Time.deltaTime;
            if (_timeFromLastDeviation >= _marketConfig.DeviationTime)
            {
                _timeFromLastDeviation = 0f;
                SetNewDeviation();
            }
        }

        private void SetNewDeviation()
        {
            _currentDeviation = Random.Range(-_marketConfig.MaxPercentDeviation, _marketConfig.MaxPercentDeviation);
        }
    }
}