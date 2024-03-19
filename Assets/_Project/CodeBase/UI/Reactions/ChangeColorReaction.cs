using _Project.CodeBase.UI.Interfaces;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Reactions
{
    public class ChangeColorReaction : MonoBehaviour, IReaction
    {
        [SerializeField] private ReferenceBehaviour _referenceBehaviour;
        [ShowIf(nameof(_referenceBehaviour), ReferenceBehaviour.Serialize)] [SerializeField] private Image _image;
        [SerializeField] private Color _color;
        
        public void React(IUIEntity entity)
        {
            GameObject go = entity.GameObject;
            
            Image image = _referenceBehaviour switch
            {
                ReferenceBehaviour.Get => GetReference(go),
                ReferenceBehaviour.Cache => CacheReference(go),
                ReferenceBehaviour.Serialize => SerializeReference(),
                _ => null
            };

            if (image != null) image.color = _color;
        }
        
        private Image GetReference(GameObject go) => go.GetComponent<Image>();

        private Image CacheReference(GameObject go) => _image ??= go.GetComponent<Image>();
        
        private Image SerializeReference() => _image;
    }
}