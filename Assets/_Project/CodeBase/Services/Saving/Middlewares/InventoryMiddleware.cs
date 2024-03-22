using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.CodeBase.GameFlow.Inventory;
using _Project.CodeBase.GameFlow.Inventory.Interfaces;
using _Project.CodeBase.Services.Saving.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.CodeBase.Services.Saving.Middlewares
{
    public class InventoryMiddleware : IMiddleware
    {
        public bool IsLoadApplicable(Type data)
        {
            if (data == typeof(Inventory))
                return true;
            if (data == typeof(IInventory))
                return true;
            return false;
        }

        public void Save(object data, string saveName)
        {
            if (data is Inventory inventory)
            {
                InventoryData inventoryData = new();

                foreach (var (key, value) in inventory.GetInventory())
                {
                    inventoryData.Resources.Add(key, value);
                }

                var json = JsonConvert.SerializeObject(inventoryData);
                File.WriteAllText(saveName, json);
            }
            else
            {
                throw new ArgumentException("Data is not an Inventory");
            }
        }

        public object Load(string saveName)
        {
            string json = File.ReadAllText(saveName);
            InventoryData inventoryData = JsonConvert.DeserializeObject<InventoryData>(json);

            Inventory inventory = new();
            foreach (KeyValuePair<string,int> resource in inventoryData.Resources)
            {
                inventory.AddResource(resource.Key, resource.Value);
            }

            return inventory;
        }

        public bool IsSaveApplicable(object data) => CheckApplicability(data);

        private bool CheckApplicability(object data)
        {
            if (data is Inventory)
                return true;
            if (data is IInventory)
                return true;
            
            return false;
        }

        [Serializable]
        public class InventoryData
        {
            public Dictionary<string, int> Resources = new();
        }
    }
}