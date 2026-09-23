using _01_Scripts.Level;
using UnityEngine;

namespace _01_Scripts
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Player.Player>() != null)
                Destroy(GameManager.Instance.Player.gameObject);
            else
                Destroy(other);
        }
    }
}
