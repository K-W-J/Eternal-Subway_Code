using UnityEngine;

namespace _01_Scripts.Player
{
    public class PlayerAmmo : MonoBehaviour
    {
        public int AmmoCount { get; set; }

        public void AddAmmo(int ammo) => AmmoCount += ammo;
        public int TakeAmmo() => AmmoCount;
    }
}