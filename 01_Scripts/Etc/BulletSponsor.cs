using UnityEngine;

namespace _01_Scripts.Enemy
{
    public class BulletSponsor : MonoBehaviour
    {
        [SerializeField] Enemy _entity;
        /*public void BulletSpawn()
        {
            GameObject bullet = Instantiate(_entity.MinerStatsSO.Bullet, 
                _entity.AttackChecker.long);
            
            bullet.transform.SetParent(null);
            bullet.transform.LookAt(_entity.transform.position);
        }*/
    }
}