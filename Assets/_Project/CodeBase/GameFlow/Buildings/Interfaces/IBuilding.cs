using System;
using System.Collections.Generic;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;

namespace _Project.CodeBase.GameFlow.Buildings.Interfaces
{
    public interface IBuilding : IUpgradable
    {
        public string Id { get; }
        public IResource[] Cost { get; }
        public IResource ProducingResource { get; }
        public IResource[] ProducingCost { get; }
        public float ProductionRate { get; }
        public void Remove();
        public void TurnOn();
        public void TurnOff();
    }

    public interface IUpgradable
    {
        public Dictionary<IResource, int> UpgradeCost { get; }
        
        /// <summary>
        /// (Building that was upgraded, New building)
        /// </summary>
        public event Action<IBuilding, IBuilding> Upgraded;
        
        public bool CanBeUpgraded { get; }
        public void Upgrade();
    }
}