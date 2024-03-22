using IFactories = _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.Map;
using _Project.CodeBase.GameFlow.Map.Common;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Factories
{
    public class MapFactory : IFactories.IFactory<IMap, string>
    {
        private readonly IMap _map;
        private readonly MapConfig _mapConfig;
        private readonly ILoader _loader;
        private readonly IFactories.IFactory<IBuilding, string, Vector3, Quaternion> _buildingFactory;

        public MapFactory(IMap map, MapConfig mapConfig,
            [InjectOptional(Id = SaveMethod.Json)] ILoader loader,
            IFactories.IFactory<IBuilding, string, Vector3, Quaternion> buildingFactory)
        {
            _map = map;
            _mapConfig = mapConfig;
            _loader = loader;
            _buildingFactory = buildingFactory;
        }

        public IMap Create(string saveName = null)
        {
            if (saveName == null) return CreateNewMap();
            else return LoadExistingMap(saveName);
        }

        private IMap CreateNewMap()
        {
            for (int x = 0; x < _map.Hexes.GetLength(0); x++)
            {
                for (int y = 0; y < _map.Hexes.GetLength(1); y++)
                {
                    _map.Hexes[x, y].SetBuilding(_buildingFactory.Create(_mapConfig.defaultBuildingId, new Vector3(x, 0, y), Quaternion.identity));
                }
            }
            
            return _map;
        }

        private IMap LoadExistingMap(string saveName)
        {
            _loader.Load<IMap>(out IMap map, saveName);
            _map.Copy(map);

            for (int x = 0; x < _map.Hexes.GetLength(0); x++)
            {
                for (int y = 0; y < _map.Hexes.GetLength(1); y++)
                {
                    _map.GetHex(x, y).Building = _buildingFactory.Create(_map.Hexes[x,y].Building.Id, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
            
            return _map;
        }
    }
}