using _Project.CodeBase.Factories;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.DI.GameScope
{
    public class GameFactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Factories.Interfaces.IFactory<IBuilding, string, Vector3, Quaternion>>()
                .To<BuildingFactory>().AsSingle();
            Container.Bind<Factories.Interfaces.IFactory<IMap, string>>().To<MapFactory>().AsSingle();
            Container.Bind<Factories.Interfaces.IFactory<IInventory, string>>().To<InventoryFactory>().AsSingle();

        }
    }
}