using System;
using System.Collections.Generic;
using System.IO;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.Map;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Services.Saving.Middlewares
{
    public class MapMiddleware : IMiddleware
    {
        private readonly Dictionary<string, GameObject> _buildingPrefabs;

        public MapMiddleware(
            [InjectOptional(Id = typeof(IBuilding))] Dictionary<string, GameObject> buildingPrefabs)
        {
            _buildingPrefabs = buildingPrefabs;
        }

        public object Load(string saveName)
        {
            if (File.Exists(saveName))
            {
                var json = File.ReadAllText(saveName);
                var mapData = JsonConvert.DeserializeObject<MapData>(json);
                
                Map map = new Map(null);

                foreach (KeyValuePair<string, string> hex in mapData.Hexes)
                {
                    SerializableCoords coords = SerializableCoords.FromString(hex.Key);
                    map.GetHex(coords.X, coords.Y).Building = _buildingPrefabs[hex.Value].GetComponent<IBuilding>();
                }
                
                return map;
            }
            else
            {
                throw new ArgumentException("File with this name doesn't exist");
            }

            return null;
        }


        public void Save(object data, string saveName)
        {
            if (data is Map map)
            {
                MapData mapData = new MapData();

                for(int x = 0; x < map.Hexes.GetLength(0); x++)
                {
                    for(int y = 0; y < map.Hexes.GetLength(1); y++)
                    {
                        IHex hex = map.GetHex(x, y);
                        if (hex.Building != null)
                        {
                            mapData.Hexes.Add(new SerializableCoords(x, y).ConvertToString(), hex.Building.Id);
                        }
                    }
                }
                
                var json = JsonConvert.SerializeObject(mapData);
                File.WriteAllText(saveName, json);
            }
            else
            {
                throw new ArgumentException("Data is not a Map");
            }
        }
        
        [Serializable]
        public class MapData
        {
            public Dictionary<string, string> Hexes = new();
        }

        [Serializable]
        public class SerializableCoords
        {
            public int X;
            public int Y;

            public SerializableCoords(int x, int y)
            {
                X = x;
                Y = y;
            }

            public string ConvertToString()
            {
                return $"{X};{Y}";
            }

            public static SerializableCoords FromString(string data)
            {
                string[] coords = data.Split(';');
                return new SerializableCoords(int.Parse(coords[0]), int.Parse(coords[1]));
            }
        }
    }
}