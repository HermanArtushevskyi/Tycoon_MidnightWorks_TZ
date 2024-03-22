using UnityEngine;

namespace _Project.CodeBase.Services.Saving.Common
{
    [CreateAssetMenu(fileName = "AutoSaverConfig", menuName = "Configuration/Services/AutoSaver", order = 0)]
    public class AutoSaverConfig : ScriptableObject
    {
        public float SecondsBetweenSaves = 120f;
    }
}