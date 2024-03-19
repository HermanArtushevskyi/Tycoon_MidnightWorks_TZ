using System.Collections.Generic;

namespace _Project.CodeBase.GameFlow.Inventory.Interfaces
{
    public interface IInventory
    {
        public int GetAmount(string resourceId);
        public void AddResource(string resourceId, int amount);
        public void RemoveResource(string resourceId, int amount);
        public void Copy(IInventory inventory);
        public Dictionary<string, int> GetInventory();
    }
}