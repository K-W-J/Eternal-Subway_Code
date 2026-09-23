using TMPro;
using UnityEngine;

namespace _01_Scripts.UI
{
    public class LookAtPlayerUI : MonoBehaviour
    {
        [field:SerializeField] public Transform Target { get; set; }
        [field: SerializeField] public bool IsTutorial { get; private set; }

        [SerializeField] private TextMeshProUGUI textMeshPro;
        
        public void SetText(string text) => textMeshPro.text = text;
        public void SetText(float text) => textMeshPro.text = text.ToString("F2");
        public void SetText(int text) => textMeshPro.text = text.ToString();
        
        private void Update()
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }
            
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
            transform.position = Target.position + Vector3.up * 0.5f;
            
            if(Target.gameObject.activeSelf == false)
                Destroy(gameObject);

            if (IsTutorial)
            {
                float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
                
                float minScale = 0.0036f;
                float maxScale = 0.02f;
                float scaleFactor = Mathf.Clamp(distance * 0.003f, minScale, maxScale);

                transform.localScale = Vector3.one * scaleFactor;
            }
        }
    }
}
