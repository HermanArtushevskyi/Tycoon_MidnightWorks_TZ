namespace _Project.CodeBase.GameFlow.Map.Interfaces
{
    public interface IMap
    {
        public IHex[,] Hexes { get; }
        public IHex GetHex(int x, int y);
        public void Copy(IMap map);
    }
}