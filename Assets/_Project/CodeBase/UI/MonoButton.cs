using System;
using _Project.CodeBase.UI.Interfaces;
using TNRD;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI
{
    public class MonoButton : MonoEntity, IButton
    {
        [SerializeField] private SerializableInterface<IReaction>[] _onClickedReactions;
        [SerializeField]private SerializableInterface<IReaction>[] _onEnterReactions;
        [SerializeField]private SerializableInterface<IReaction>[] _onExitReactions;
        [SerializeField]private SerializableInterface<IReaction>[] _onDownReactions;
        [SerializeField]private SerializableInterface<IReaction>[] _onUpReactions;
        
        public event Action<IButton> OnClick;
        public event Action<IButton> OnEnter;
        public event Action<IButton> OnExit;
        public event Action<IButton> OnDown;
        public event Action<IButton> OnUp;


        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
            
            foreach (var reaction in _onClickedReactions)
                reaction.Value.React(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter?.Invoke(this);
            
            foreach (var reaction in _onEnterReactions)
                reaction.Value.React(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit?.Invoke(this);
            
            foreach (var reaction in _onExitReactions)
                reaction.Value.React(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke(this);
            
            foreach (var reaction in _onDownReactions)
                reaction.Value.React(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp?.Invoke(this);
            
            foreach (var reaction in _onUpReactions)
                reaction.Value.React(this);
        }
    }
}