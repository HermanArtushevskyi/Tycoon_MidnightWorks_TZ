using _Project.CodeBase.UI.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class MonoWindow : MonoEntity, IWindow
    {

        [SerializeField] private string _id;
        public string GetId() => _id;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}