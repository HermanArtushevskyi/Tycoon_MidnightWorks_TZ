using System;
using System.Collections.Generic;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.Services.Saving.Common;

namespace _Project.CodeBase.GameFlow.Inventory
{
    [Serializable]
    [Savable(nameof(Inventory))]
    public class Inventory : IInventory
    {
        private Dictionary<string, int> _inventory = new();
        
        public int GetAmount(string resourceId) => _inventory.ContainsKey(resourceId) ? _inventory[resourceId] : 0;

        public void AddResource(string resourceId, int amount)
        {
            if (_inventory.ContainsKey(resourceId)) _inventory[resourceId] += amount;
            else _inventory.Add(resourceId, amount);
        }

        public void RemoveResource(string resourceId, int amount)
        {
            if (_inventory.ContainsKey(resourceId) && _inventory[resourceId] >= amount)
                _inventory[resourceId] -= amount;
            else if (_inventory.ContainsKey(resourceId)) _inventory.Remove(resourceId);
        }

        public void Copy(IInventory inventory)
        {

            _inventory = inventory.GetInventory();
        }

        public Dictionary<string, int> GetInventory() => _inventory;
    }
}