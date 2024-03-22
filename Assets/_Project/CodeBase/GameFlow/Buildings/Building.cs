using System;
using System.Collections.Generic;
using _Project.CodeBase.GameFlow.Buildings.Interfaces;
using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using TNRD;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.GameFlow.Buildings
{
    public class Building : MonoBehaviour, IBuilding
    {
        public string Id => _id;
        public string Name => _name;
        public GameObject GameObject => gameObject;

        public Dictionary<IResource, int> Cost
        {
            get
            {
                Dictionary<IResource, int> resources = new();
                foreach (var resource in _buildingCost)
                {
                    resources.Add(resource.Key.Value, resource.Value);
                }

                return resources;
            }
        }
        public Dictionary<IResource, int> UpgradeCost
        {
            get
            {
                Dictionary<IResource, int> resources = new();
                foreach (var resource in _upgradeCost)
                {
                    resources.Add(resource.Key.Value, resource.Value);
                }

                return resources;
            }
        }
        public event Action<IBuilding, IBuilding> Upgraded;
        public IBuilding NextBuilding => _upgradeBuilding.GetComponent<IBuilding>();
        public bool CanBeUpgraded => _upgradeBuilding != null;
        public IResource ProducingResource => _product.Value;
        public IResource[] ProducingCost
        {
            get
            {
                List<IResource> resources = new();
                foreach (SerializableInterface<IResource> resource in _productCost)
                {
                    resources.Add(resource.Value);
                }

                return resources.ToArray();
            }
        }
        public float ProductionRate => _productionRate;

        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private GenericDictionary<SerializableInterface<IResource>, int> _buildingCost;
        [SerializeField] private SerializableInterface<IResource> _product;
        [SerializeField] private SerializableInterface<IResource>[] _productCost;
        [SerializeField] private float _productionRate;
        [SerializeField] private GameObject _upgradeBuilding;
        [SerializeField] private GenericDictionary<SerializableInterface<IResource>, int> _upgradeCost;
        
        private bool _canProduce = true;
        private float _timeSinceLastProduct;
        protected IInventory Inventory;

        [Inject]
        private void GetDependencies(IInventory inventory)
        {
            Inventory = inventory;
        }
        
        public void Upgrade()
        {
            Upgraded?.Invoke(this, _upgradeBuilding.GetComponent<IBuilding>());
        }

        public void Remove()
        {
            _canProduce = false;
            Destroy(gameObject);
        }

        public void TurnOn() => _canProduce = true;

        public void TurnOff() => _canProduce = false;

        private void Update()
        {
            if (_product.Value == null) return;
            if (!_canProduce) return;
            
            _timeSinceLastProduct += Time.deltaTime;
            if (_timeSinceLastProduct >= _productionRate)
            {
                foreach (SerializableInterface<IResource> resource in _productCost)
                {
                    if (Inventory.GetAmount(resource.Value.Id) <= 0)
                    {
                        return;
                    }
                }
                
                _timeSinceLastProduct = 0;
                Produce();
            }
        }

        private void Produce()
        {
            Inventory.AddResource(_product.Value.Id, 1);

            foreach (SerializableInterface<IResource> resource in _productCost)
            {
                Inventory.RemoveResource(resource.Value.Id, 1);
            }
        }
    }
}