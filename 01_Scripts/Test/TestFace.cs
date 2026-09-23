using System;
using UnityEngine;

namespace _01_Scripts.Test
{
    public class TestFace : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Vector3 backForce = transform.forward * 30;
            Vector3 upForce = Vector3.up * 3;

            Rigidbody player = other.GetComponentInParent<Rigidbody>();
                
            player.linearVelocity = Vector3.zero;
            player.AddForce(backForce, ForceMode.Impulse);
            player.AddForce(upForce, ForceMode.Impulse);
        }
    }
}