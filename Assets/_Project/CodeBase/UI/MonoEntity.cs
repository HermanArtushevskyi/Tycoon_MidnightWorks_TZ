using System;
using _Project.CodeBase.UI.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public abstract class MonoEntity : MonoBehaviour, IUIEntity
    {
        public event Action<IUIEntity> OnGameObjectDestroy;
        public GameObject GameObject => gameObject;
        public object EntityData { get; protected set; }
        public void SetData(object data)
        {
            EntityData = data;
        }

        public void DestroyEntity()
        {
            OnGameObjectDestroy?.Invoke(this);
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            OnGameObjectDestroy?.Invoke(this);
        }
    }
}