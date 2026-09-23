using UnityEngine;

namespace _01_Scripts.Interactables.Trap
{
    public class WhoopeeCushionTrap : Trap
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponentInParent<Player.Player>() != null)
            {
                SoundManager.Instance.PlaySFX("Fart", transform);
                
                if(Agent == null || Agent.EnemyMovement.Target != null) return;
                
                Agent.EnemyMovement.Target = gameObject;
            }
        }
    }
}