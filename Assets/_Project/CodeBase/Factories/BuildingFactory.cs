using System.Collections.Generic;
using _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Factories
{
    public class BuildingFactory : IFactory<IBuilding, string, Vector3, Quaternion>
    {
        private readonly Dictionary<string, GameObject> _buildingPrefabs;

        public BuildingFactory(
            [InjectOptional(Id = typeof(IBuilding))]Dictionary<string, GameObject> buildingPrefabs)
        {
            _buildingPrefabs = buildingPrefabs;
        }

        public IBuilding Create(string buildingName, Vector3 position, Quaternion rotation)
        {
            var prefab = _buildingPrefabs[buildingName];
            var building = GameObject.Instantiate(prefab,position, rotation, null).GetComponent<IBuilding>();
            return building;
        }
    }
}