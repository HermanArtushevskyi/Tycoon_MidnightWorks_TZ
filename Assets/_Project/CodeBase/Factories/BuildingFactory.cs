using System.Collections.Generic;
using IFactories = _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Factories
{
    public class BuildingFactory : IFactories.IFactory<IBuilding, string, Vector3, Quaternion>
    {
        private readonly DiContainer _container;
        private readonly Dictionary<string, GameObject> _buildingPrefabs;

        public BuildingFactory(
            DiContainer container,
            [InjectOptional(Id = typeof(IBuilding))]Dictionary<string, GameObject> buildingPrefabs)
        {
            _container = container;
            _buildingPrefabs = buildingPrefabs;
        }

        public IBuilding Create(string buildingName, Vector3 position, Quaternion rotation)
        {
            var prefab = _buildingPrefabs[buildingName];
            var buildingGO = _container.InstantiatePrefab(prefab, position, rotation, null);
            var building = buildingGO.GetComponent<IBuilding>();
            
            return building;
        }
    }
}