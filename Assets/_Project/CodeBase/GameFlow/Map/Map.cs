using _Project.CodeBase.GameFlow.Map.Common;
using _Project.CodeBase.GameFlow.Map.Interfaces;

namespace _Project.CodeBase.GameFlow.Map
{
    public class Map : IMap
    {
        public Map(MapConfig config)
        {
            Hexes = new IHex[config.xSize, config.ySize];
            for (int x = 0; x < config.xSize; x++)
            {
                for (int y = 0; y < config.ySize; y++)
                {
                    Hexes[x, y] = new Hex();
                }
            }
        }

        public IHex[,] Hexes { get; private set; }
        public IHex GetHex(int x, int y) => Hexes[x, y];
        
        public void Copy(IMap map)
        {
            this.Hexes = map.Hexes;
        }
    }
}