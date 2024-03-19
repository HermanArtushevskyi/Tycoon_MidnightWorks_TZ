using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;

namespace _Project.CodeBase.GameFlow.Map
{
    public class Hex : IHex
    {
        public bool IsEmpty => Building == null;
        public IBuilding Building { get; private set; }

        public Hex(IBuilding building = null)
        {
            if (building != null)
                SetBuilding(building);
        }

        public void SetBuilding(IBuilding building)
        {
            Building?.Remove();
            Building = building;
            Building.Upgraded += OnUpgraded;
        }

        private void OnUpgraded(IBuilding oldBuilding, IBuilding newBuilding)
        {
            oldBuilding.Upgraded -= OnUpgraded;
            SetBuilding(newBuilding);
        }

        public void RemoveBuilding()
        {
            Building.Remove();
            Building = null;
        }
    }
}