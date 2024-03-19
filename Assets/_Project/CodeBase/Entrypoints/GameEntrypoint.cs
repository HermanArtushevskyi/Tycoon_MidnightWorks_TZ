using _Project.CodeBase.GameFlow.Map.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Entrypoints
{
    public class GameEntrypoint : MonoBehaviour
    {
        private Factories.Interfaces.IFactory<IMap, string> _mapFactory;

        [Inject]
        private void GetDependencies(
            Factories.Interfaces.IFactory<IMap, string> mapFactory)
        {
            _mapFactory = mapFactory;
        }
        
        private void Start()
        {
            IMap map = _mapFactory.Create(null);
        }
    }
}