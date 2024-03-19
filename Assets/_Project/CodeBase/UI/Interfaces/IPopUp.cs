using UnityEngine;

namespace _Project.CodeBase.UI.Interfaces
{
    public interface IPopUp : IUIEntity
    {
        public void Show(Vector3 worldPos);
    }
}