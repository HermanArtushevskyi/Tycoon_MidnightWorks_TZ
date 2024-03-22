using System;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Common;
using UnityEditor;

namespace _Project.CodeBase.GameFlow.Map
{
    [Serializable]
    [Savable(nameof(Hex))]
    public class Hex : IHex
    {
        public bool IsEmpty => Building == null;
        public IBuilding Building { get; set; }
        
        public Hex(IBuilding building = null)
        {
            if (building != null)
                SetBuilding(building);
        }

        public void SetBuilding(IBuilding building)
        {
            if (Building != null && PrefabUtility.GetPrefabInstanceStatus(Building.GameObject) == PrefabInstanceStatus.NotAPrefab)
                Building?.Remove();
            Building = building;
        }

        public void RemoveBuilding()
        {
            Building.Remove();
            Building = null;
        }
    }
}