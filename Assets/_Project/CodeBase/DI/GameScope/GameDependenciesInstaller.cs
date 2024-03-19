using _Project.CodeBase.GameFlow.Inventory;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using Zenject;

namespace _Project.CodeBase.DI.GameScope
{
    public class GameDependenciesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMap();
            BindInventory();
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