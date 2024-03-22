using _Project.CodeBase.Common;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.GameFlow.Map.Interfaces;
using _Project.CodeBase.Services.Saving.Common;
using _Project.CodeBase.Services.Saving.Interfaces;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Reactions
{
    public class SaveGameReaction : MonoBehaviour, IReaction
    {
        private ISaver _saver;
        private IInventory _inventory;
        private IMap _map;
        private GameInfo _gameInfo;

        [Inject]
        private void GetDependencies(
            [InjectOptional(Id = SaveMethod.Json)] ISaver saver,
            IInventory inventory,
            IMap map,
            GameInfo gameInfo)
        {
            _saver = saver;
            _inventory = inventory;
            _map = map;
            _gameInfo = gameInfo;
        }
        
        public void React(IUIEntity entity)
        {
            _saver.Save<IInventory>(_inventory, _gameInfo.GameName);
            _saver.Save<IMap>(_map, _gameInfo.GameName);
        }
    }
}