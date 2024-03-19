using _Project.CodeBase.GameFlow.Buildings.Interfaces;

namespace _Project.CodeBase.GameFlow.Map.Interfaces
{
    public interface IHex
    {
        public bool IsEmpty { get; }
        public IBuilding Building { get; }
        public void SetBuilding(IBuilding building);
        public void RemoveBuilding();
    }
}