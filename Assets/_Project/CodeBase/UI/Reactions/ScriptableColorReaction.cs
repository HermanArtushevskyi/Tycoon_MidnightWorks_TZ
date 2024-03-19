using _Project.CodeBase.UI.Interfaces;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Reactions
{
    [CreateAssetMenu(fileName = "ScriptableColorReaction", menuName = "UI/Reactions/ColorReaction", order = 0)]
    public class ScriptableColorReaction : ScriptableObject, IReaction
    {
        [ValidateInput(nameof(ValidateRefBehaviour))] [SerializeField] private ReferenceBehaviour _referenceBehaviour;
        private Image _image;
        [SerializeField] private Color _color;
        
        public void React(IUIEntity entity)
        {
            GameObject go = entity.GameObject;
            
            Image image = _referenceBehaviour switch
            {
                ReferenceBehaviour.Get => GetReference(go),
                ReferenceBehaviour.Cache => CacheReference(go),
                _ => null
            };

            if (image != null) image.color = _color;
        }
        
        private Image GetReference(GameObject go) => go.GetComponent<Image>();

        private Image CacheReference(GameObject go) => _image ??= go.GetComponent<Image>();

        private TriValidationResult ValidateRefBehaviour()
        {
            if (_referenceBehaviour == ReferenceBehaviour.Serialize) return TriValidationResult.Error("ReferenceBehaviour cannot be Serialize in scriptable object");
            if (_referenceBehaviour == ReferenceBehaviour.Cache) return TriValidationResult.Warning("ReferenceBehaviour.Cache may behave unexpectedly in scriptable object. Consider using Get instead.");
            return TriValidationResult.Valid;
        }
    }
}