using _Project.CodeBase.UI.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.UI.Reactions
{
    public class DebugTextReaction : MonoBehaviour, IReaction
    {
        public void React(IUIEntity entity)
        {
            Debug.Log($"{gameObject.name} on debug reaction");
        }
    }
}