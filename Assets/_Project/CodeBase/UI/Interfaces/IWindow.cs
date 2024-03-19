using UnityEditor;

namespace _Project.CodeBase.UI.Interfaces
{
    public interface IWindow : IUIEntity
    {
        public void Show();
        public void Hide();
        
        public string GetId();
    }
}