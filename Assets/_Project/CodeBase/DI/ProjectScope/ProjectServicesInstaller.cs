using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Common;
using _Project.CodeBase.Factories;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map;
using _Project.CodeBase.GameFlow.Map.Common;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.GameFlow.Market.Common;
using _Project.CodeBase.Services.Saving;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using _Project.CodeBase.Services.Saving.Middlewares;
using _Project.CodeBase.Services.SceneLoading;
using _Project.CodeBase.Services.SceneLoading.Interfaces;
using _Project.CodeBase.UI;
using _Project.CodeBase.UI.Common;
using _Project.CodeBase.UI.Interfaces;
using TNRD;
using TriInspector;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.DI.ProjectScope
{
    [DeclareTabGroup("tabs")]
    public class ProjectServicesInstaller : MonoInstaller
    {
        [Group("tabs"), Tab("Configs"), SerializeField] private SavingConfig _saving;
        [Group("tabs"), Tab("Configs"), SerializeField] private MapConfig _mapConfig;
        [Group("tabs"), Tab("Configs"), SerializeField] private MarketConfig _marketConfig;
        [Group("tabs"), Tab("Configs"), SerializeField] private AutoSaverConfig _autoSaverConfig;
        [Group("tabs"), Tab("Static"), SerializeField] private WindowId _windowId;
        [Group("tabs"), Tab("Static"), SerializeField] private GameObject[] _windowPrefabs;
        [Group("tabs"), Tab("Static"), SerializeField] private GameObject[] _buildingPrefabs;
        [Group("tabs"), Tab("Static"), SerializeField] private SerializableInterface<IResource>[] _resources;

        private Dictionary<string, IResource> ResourcesDictionary
        {
            get
            {
                Dictionary<string, IResource> resources = new();

                foreach (SerializableInterface<IResource> resource in _resources)
                {
                    resources.Add(resource.Value.Id, resource.Value);
                }

                return resources;
            }
        }
        
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindSavingSystem();
            BindUI();
            BindBuildingPrefabs();
            BindMapConfig();
            BindMarketConfig();
            BindResources();
            BindGameInfo();
        }

        private void BindGameInfo()
        {
            Container.Bind<GameInfo>().To<GameInfo>().AsSingle();
        }

        private void BindMarketConfig()
        {
            Container.Bind<MarketConfig>().FromInstance(_marketConfig).AsSingle();
        }

        private void BindResources()
        {
            Container.Bind<Dictionary<string, IResource>>().FromInstance(ResourcesDictionary).AsSingle();
        }

        private void BindMapConfig()
        {
            Container.Bind<MapConfig>().FromInstance(_mapConfig).AsSingle();
        }

        private void BindBuildingPrefabs()
        {
            Container.Bind<Dictionary<string, GameObject>>().WithId(typeof(IBuilding)).FromInstance(_buildingPrefabs.ToDictionary(go => go.GetComponent<IBuilding>().Id));
        }

        private void BindUI()
        {
            Container.Bind<WindowId>().FromInstance(_windowId).AsSingle();
            Container.Bind<Dictionary<string, GameObject>>().WithId(typeof(IWindow)).FromInstance(_windowPrefabs.ToDictionary(go => go.GetComponent<IWindow>().GetId()));
            Container.Bind<IWindowsManager>().To<WindowsManager>().AsSingle();
        }

        private void BindSavingSystem()
        {
            Container.Bind<SavingConfig>().FromInstance(_saving).AsSingle();
            Container.Bind(typeof(ISaver), typeof(ILoader)).WithId(SaveMethod.Json).To<JsonSaver>().AsCached();
            Container.Bind<IMiddleware>().WithId(typeof(IMap)).To<MapMiddleware>().AsCached();
            Container.Bind<IMiddleware>().WithId(typeof(IInventory)).To<InventoryMiddleware>().AsCached();
            Container.Bind<IMiddleware>().WithId(SaveMethod.Json).To<JsonMiddleware>().AsCached();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<AutoSaverConfig>().FromInstance(_autoSaverConfig).AsSingle();
        }
    }
}