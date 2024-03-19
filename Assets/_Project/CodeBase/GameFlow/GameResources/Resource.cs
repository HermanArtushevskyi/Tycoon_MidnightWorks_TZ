using _Project.CodeBase.GameFlow.GameResources.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.GameFlow.GameResources
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Resource")]
    public class Resource : ScriptableObject, IResource
    {
        public string Id => _id;
        public string Name => _name;

        [SerializeField] private string _id;
        [SerializeField] private string _name;
    }
}