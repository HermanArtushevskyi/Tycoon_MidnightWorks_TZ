using UnityEngine;

namespace _Project.CodeBase.UI.Common
{
    [UnityEngine.CreateAssetMenu(fileName = "WindowIds", menuName = "StaticData/UI/WindowIds", order = 0)]
    public class WindowId : ScriptableObject
    {
        public string MainMenu;
        public string NewGame;
        public string LoadGame;
        public string GameUI;
        public string Inventory;
        public string BuyBuilding;
        public string BuildingInfo;
    }
}