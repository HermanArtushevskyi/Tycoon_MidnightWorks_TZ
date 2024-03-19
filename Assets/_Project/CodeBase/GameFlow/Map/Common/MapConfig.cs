using UnityEngine;

namespace _Project.CodeBase.GameFlow.Map.Common
{
    [UnityEngine.CreateAssetMenu(fileName = "MapConfig", menuName = "Configuration/Services/MapConfig", order = 0)]
    public class MapConfig : ScriptableObject
    {
        public int xSize;
        public int ySize;
        public string defaultBuildingId;
    }
}