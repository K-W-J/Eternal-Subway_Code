using System.Collections;
using UnityEngine;

namespace _01_Scripts.Enemy.Looker
{
    public class LookChecker : MonoBehaviour
    {
        [SerializeField] private Player.Player _agent;
        
        private Camera _camera;

        private bool _isLook;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            LookCheck();
        }

        public void LookCheck()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _agent.PlayerStatsSo.InteractionRange + 10))
            {
                if (hit.transform.GetComponentInParent<Looker>() is { } looker)
                {
                    if (!_isLook)
                        StartCoroutine(LookCheckCoroutine(looker));
                }
                else
                {
                    _isLook = false;
                }
            }
        }

        private IEnumerator LookCheckCoroutine(Looker looker)
        {
            _isLook = true;
            
            float remainingTime = looker.LookerStatsSo.LookTime;

            while (remainingTime > 0)
            {
                if (!_isLook)
                {
                    _isLook = false;
                    yield break;
                }
                
                remainingTime -= Time.deltaTime;
                    
                yield return null;
            }
            
            looker.LookCounter.SetLookCount();
            
            _isLook = false;
        }
    }
}