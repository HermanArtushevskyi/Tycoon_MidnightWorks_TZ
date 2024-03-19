using _Project.CodeBase.Factories;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.UI.Interfaces;
using Zenject;
using IFactories = _Project.CodeBase.Factories.Interfaces;

namespace _Project.CodeBase.DI.ProjectScope
{
    public class ProjectFactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFactories.IFactory<IWindow, string>>().To<WindowFactory>().AsSingle();
        }
    }
}