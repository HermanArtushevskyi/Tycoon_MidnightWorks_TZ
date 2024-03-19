using System.IO;
using UnityEngine;

namespace _Project.CodeBase.Services.Saving.Common
{
    [CreateAssetMenu(fileName = "SavingConfig", menuName = "Configuration/Services/SavingConfig", order = 0)]
    public class SavingConfig : ScriptableObject
    {
        [SerializeField] public string savePath;
        
        public string SavePath => Path.Combine(Application.persistentDataPath, savePath);
    }
}