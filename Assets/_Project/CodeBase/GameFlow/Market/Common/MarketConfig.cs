using System.Collections.Generic;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using TNRD;
using UnityEngine;

namespace _Project.CodeBase.GameFlow.Market.Common
{
    [UnityEngine.CreateAssetMenu(fileName = "MarketConfig", menuName = "Configuration/Services/MarketConfig", order = 0)]
    public class MarketConfig : ScriptableObject
    {
        public float MaxPercentDeviation;
        public float DeviationTime;
        public string cashResourceId;
        
        [SerializeField] private GenericDictionary<SerializableInterface<IResource>, int> _prices;

        public Dictionary<IResource, int> Prices
        {
            get
            {
                Dictionary<IResource, int> prices = new();
                foreach (KeyValuePair<SerializableInterface<IResource>, int> pair in _prices)
                {
                    prices.Add(pair.Key.Value, pair.Value);
                }

                return prices;
            }
        }
    }
}