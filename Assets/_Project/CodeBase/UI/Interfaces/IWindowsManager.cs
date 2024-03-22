namespace _Project.CodeBase.UI.Interfaces
{
    public interface IWindowsManager
    {
        public void ShowWindow(string windowId, object data = null);
        public void HideWindow(string windowId);
    }
}