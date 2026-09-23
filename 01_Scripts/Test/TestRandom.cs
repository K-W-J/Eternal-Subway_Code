using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Test
{
    public class TestRandom : MonoBehaviour
    {
        private void Awake()
        {
            Random.InitState(1);
        }

        private void Start()
        {
            
            
            
            print(Random.Range(1, 6));
        }
    }
}