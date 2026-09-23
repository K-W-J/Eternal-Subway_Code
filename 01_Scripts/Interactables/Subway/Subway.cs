using UnityEngine;

namespace _01_Scripts.Interactables.Subway
{
    public class Subway : MonoBehaviour
    {
        public SubwayFuel SubwayFuel { get; private set; } = new SubwayFuel();

        private void Start()
        {
            SubwayFuel.SetMaxFuel(100);
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}