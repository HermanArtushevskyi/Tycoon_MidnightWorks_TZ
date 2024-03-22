using System.Collections.Generic;
using _Project.CodeBase.Services.Saving.Common;

namespace _Project.CodeBase.GameFlow.Inventory.Interfaces
{
    [Savable(nameof(IInventory))]
    public interface IInventory
    {
        public int GetAmount(string resourceId);
        public void AddResource(string resourceId, int amount);
        public void RemoveResource(string resourceId, int amount);
        public void Copy(IInventory inventory);
        public Dictionary<string, int> GetInventory();
    }
}