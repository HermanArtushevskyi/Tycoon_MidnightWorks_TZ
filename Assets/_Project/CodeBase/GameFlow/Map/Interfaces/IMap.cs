using _Project.CodeBase.Services.Saving.Common;

namespace _Project.CodeBase.GameFlow.Map.Interfaces
{
    [Savable(nameof(IMap))]
    public interface IMap
    {
        public IHex[,] Hexes { get; }
        public IHex GetHex(int x, int y);
        public void Copy(IMap map);
    }
}