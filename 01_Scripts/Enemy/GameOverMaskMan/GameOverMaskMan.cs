using System;
using _01_Scripts.Level;
using UnityEngine;

namespace _01_Scripts.Enemy.GameOverMaskMan
{
    public class GameOverMaskMan : MonoBehaviour
    {
        private Transform _playerPos;
    
        private float _time;
        private float _speed;

        private void Start()
        {
            _playerPos = GameManager.Instance.Player.transform;
            
            SoundManager.Instance.PlaySFX("Noise1", transform, 10, 5,true);
        }

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelCleared += Destroy;
        }
        
        private void OnDisable()
        {
            LevelManager.Instance.OnLevelCleared -= Destroy;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time > 120)
                _speed = 30f;
            else if(_time > 90)
                _speed = 15f;
            else if(_time > 60)
                _speed = 9f;
            else if(_time > 30)
                _speed = 3f;
            else
                _speed = 1f;
            
            transform.position = Vector3.MoveTowards(transform.position, _playerPos.position, _speed * Time.deltaTime);
            transform.LookAt(_playerPos);
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponentInParent<Player.Player>() != null)
                Destroy(GameManager.Instance.Player);
        }
    }
}
