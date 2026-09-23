using System.Linq;
using UnityEngine;

namespace _01_Scripts.Interactables
{
    public class InteractableOutline : MonoBehaviour
    {
        [SerializeField] private Material _outlineMaterial;
        
        private bool _isFirstFocus = true;
        
        public void OnFocus(GameObject interactableObject)
        {
            if(!_isFirstFocus) return;
            
            MeshRenderer meshRenderer = interactableObject.GetComponentInChildren<MeshRenderer>();
            Material[] outlineMaterials = new Material[meshRenderer.materials.Length + 1];

            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                outlineMaterials[i] = meshRenderer.materials[i];
            }
            
            outlineMaterials[outlineMaterials.Length - 1] = _outlineMaterial;
            
            meshRenderer.materials = outlineMaterials;

            _isFirstFocus = false;
        }

        public void OnUnfocus(GameObject interactableObject)
        {
            if(_isFirstFocus) return;
            
            MeshRenderer meshRenderer = interactableObject.GetComponentInChildren<MeshRenderer>();
            
            Material[] originalMaterials = meshRenderer.materials
                .Take(meshRenderer.materials.Length - 1)
                .ToArray();
            
            meshRenderer.materials = originalMaterials;
            
            _isFirstFocus = true;
        }
    }
}