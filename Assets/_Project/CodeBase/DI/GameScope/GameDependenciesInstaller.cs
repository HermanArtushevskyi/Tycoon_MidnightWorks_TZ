using _Project.CodeBase.GameFlow.Inventory;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.GameFlow.Market;
using _Project.CodeBase.Services.Saving;
using Zenject;

namespace _Project.CodeBase.DI.GameScope
{
    public class GameDependenciesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMap();
            BindInventory();
            BindMarket();
            BindAutoSaver();
        }

        private void BindAutoSaver()
        {
            Container.BindInterfacesAndSelfTo<AutoSaver>().AsSingle();
        }

        private void BindMarket()
        {
            Container.BindInterfacesTo<Market>().AsSingle();
        }

        private void BindInventory()
        {
            Container.Bind<IInventory>().To<Inventory>().AsSingle();
        }

        private void BindMap()
        {
            Container.Bind<IMap>().To<Map>().AsSingle();
        }
    }
}