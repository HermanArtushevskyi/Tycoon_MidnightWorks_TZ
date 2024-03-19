using System;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Interfaces
{
    public interface IButton : IUIEntity, IPointerClickHandler, IPointerEnterHandler,
                                IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<IButton> OnClick;
        public event Action<IButton> OnEnter;
        public event Action<IButton> OnExit;
        public event Action<IButton> OnDown;
        public event Action<IButton> OnUp;
    }
}