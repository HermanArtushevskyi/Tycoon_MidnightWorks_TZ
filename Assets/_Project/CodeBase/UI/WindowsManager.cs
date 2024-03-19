using System.Collections.Generic;
using _Project.CodeBase.Factories.Interfaces;
using _Project.CodeBase.UI.Interfaces;

namespace _Project.CodeBase.UI
{
    public class WindowsManager : IWindowsManager
    {
        private readonly IFactory<IWindow, string> _windowsFactory;
        
        private Dictionary<string, IWindow> _windows = new();
        
        public WindowsManager(IFactory<IWindow, string> windowsFactory)
        {
            _windowsFactory = windowsFactory;
        }

        public void ShowWindow(string windowId)
        {
            if (!_windows.ContainsKey(windowId))
            {
                _windows[windowId] = _windowsFactory.Create(windowId);
                _windows[windowId].OnDestroy += OnWindowDestroy;
            }
            
            _windows[windowId].Show();
        }

        public void HideWindow(string windowId)
        {
            if (_windows.ContainsKey(windowId))
            {
                _windows[windowId].Hide();
            }
        }

        private void OnWindowDestroy(IUIEntity obj)
        {
            obj.OnDestroy -= OnWindowDestroy;
            _windows.Remove((obj as IWindow).GetId());
        }
    }
}