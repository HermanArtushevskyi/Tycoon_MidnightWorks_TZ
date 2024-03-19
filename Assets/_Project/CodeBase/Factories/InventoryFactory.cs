using _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.GameFlow.Inventory;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using Zenject;

namespace _Project.CodeBase.Factories
{
    public class InventoryFactory : IFactory<IInventory, string>
    {
        private readonly ILoader _loader;
        private readonly IInventory _inventory;

        public InventoryFactory([InjectOptional( Id = SaveMethod.Json)] ILoader loader,
            IInventory inventory)
        {
            _loader = loader;
            _inventory = inventory;
        }

        public IInventory Create(string saveName)
        {
            if (saveName == null) return CreateNewInventory();
            else return LoadExistingInventory(saveName);
        }

        private IInventory LoadExistingInventory(string saveName)
        {
            _loader.Load(out Inventory inventory, saveName);
            _inventory.Copy(inventory);
            return _inventory;
        }

        private IInventory CreateNewInventory()
        {
            _inventory.Copy(new Inventory());
            return _inventory;
        }
    }
}