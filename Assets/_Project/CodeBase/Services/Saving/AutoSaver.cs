using _Project.CodeBase.Common;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Services.Saving
{
    public class AutoSaver : ITickable
    {
        private readonly AutoSaverConfig _config;
        private readonly ISaver _saver;
        private readonly IInventory _inventory;
        private readonly IMap _map;
        private readonly GameInfo _gameInfo;

        private float _secondsSinceLastSave = 0f;
        
        public AutoSaver(
            AutoSaverConfig config, 
            [InjectOptional(Id = SaveMethod.Json)] ISaver saver,
            IInventory inventory,
            IMap map,
            GameInfo gameInfo)
        {
            _config = config;
            _saver = saver;
            _inventory = inventory;
            _map = map;
            _gameInfo = gameInfo;
        }

        public void Tick()
        {
            _secondsSinceLastSave += Time.deltaTime;
            
            if (_secondsSinceLastSave < _config.SecondsBetweenSaves) return;
            
            _secondsSinceLastSave = 0f;
            Save();
        }

        private void Save()
        {
            _saver.Save<IInventory>(_inventory, _gameInfo.GameName);
            _saver.Save<IMap>(_map, _gameInfo.GameName);
        }
    }
}