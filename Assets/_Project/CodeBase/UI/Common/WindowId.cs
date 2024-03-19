﻿using UnityEngine;

namespace _Project.CodeBase.UI.Common
{
    [UnityEngine.CreateAssetMenu(fileName = "WindowIds", menuName = "StaticData/UI/WindowIds", order = 0)]
    public class WindowId : ScriptableObject
    {
        public string MainMenu;
        public string Settings;
        public string NewGame;
        public string LoadGame;
    }
}