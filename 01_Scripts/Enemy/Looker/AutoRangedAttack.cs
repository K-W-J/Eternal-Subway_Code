using System.Collections;
using UnityEngine;

namespace _01_Scripts.Enemy.Looker
{
    public class AutoRangedAttack : MonoBehaviour
    {
        [SerializeField] private Looker _agent;
        [SerializeField] private Transform _firePos;
        
        [SerializeField] private float _attackTime;
        [SerializeField] private int _attackPower;

        private Player.Player _player;
        
        private bool _isAttack;

        private void Start()
        {
            _player = _agent.EnemyMovement.Target.GetComponent<Player.Player>();
        }

        private void Update()
        {
            if (_agent.AttackChecker.LongRangeAttackCheck() && !_isAttack && _agent.Collider.gameObject.activeSelf)
                StartCoroutine(WaitStopFireAttackTime());
        }

        private IEnumerator WaitStopFireAttackTime()
        {
            _isAttack = true;
            
            float remainingTime = _agent.LookerStatsSo.LongRangeAttackSo.LongRangeAttackTime;

            VolumeManager.Instance.VignetteOutIn(false);
            
            while (remainingTime > 0)
            {
                if (!_agent.AttackChecker.LongRangeAttackCheck() || !_agent.Collider.gameObject.activeSelf)
                {
                    _isAttack = false;
                    VolumeManager.Instance.VignetteOutIn(true);
                    yield break;
                }
                
                remainingTime -= Time.deltaTime;
                yield return null;
            }
            
            VolumeManager.Instance.VignetteOutIn(true);
            
            StartCoroutine(WaitStopAttackTime());
        }
        
        private IEnumerator WaitStopAttackTime()
        {
            while (_agent.AttackChecker.LongRangeAttackCheck() && _isAttack && _agent.Collider.gameObject.activeSelf)
            {
                _player.Health.TakeDamage(_attackPower);
                yield return new WaitForSeconds(_attackTime);
            }
            
            VolumeManager.Instance.VignetteOutIn(true);
            _isAttack = false;
        }
    }
}