using _Project.CodeBase.UI.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.UI.Reactions
{
    public class QuitGameReaction : MonoBehaviour, IReaction
    {
        public void React(IUIEntity entity)
        {
            Application.Quit();
        }
    }
}