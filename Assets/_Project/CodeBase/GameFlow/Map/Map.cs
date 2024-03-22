using System;
using _Project.CodeBase.GameFlow.Map.Common;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Common;

namespace _Project.CodeBase.GameFlow.Map
{
    [Serializable]
    [Savable(nameof(Map))]
    public class Map : IMap
    {
        public Map(MapConfig config)
        {
            int xSize = 10;
            int ySize = 10;
            if (config != null)
            {
                xSize = config.xSize;
                ySize = config.ySize;
            }
            
            Hexes = new IHex[xSize, ySize];
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Hexes[x, y] = new Hex();
                }
            }
        }

        public IHex[,] Hexes { get; set; }
        public IHex GetHex(int x, int y) => Hexes[x, y];
        
        public void Copy(IMap map)
        {
            this.Hexes = map.Hexes;
        }
    }
}