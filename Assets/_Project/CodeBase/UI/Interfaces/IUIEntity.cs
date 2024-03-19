using System;

namespace _Project.CodeBase.UI.Interfaces
{
    public interface IUIEntity : IHasGameObject
    {
        public event Action<IUIEntity> OnDestroy;
        
        public object EntityData { get; }
        public void SetData(object data);
        public void DestroyEntity();
    }
}