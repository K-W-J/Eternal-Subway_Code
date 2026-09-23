using System;
using _01_Scripts.BuildObjects;
using UnityEngine;

namespace _01_Scripts.Test
{
    public class Pos : MonoBehaviour
    {
        [SerializeField] private BuildObject _transform;
        [SerializeField] private GameObject _transform2;

        private void Start()
        {
            Vector3 asda = _transform.BuildPoints[0].position;
            Vector3 asdsa = _transform2.transform.position;
            print(asdsa - asda);
            //_transform.transform.rotation = _transform2.transform.rotation * _transform.BuildPoints[0].rotation;
            _transform2.transform.position = asda - asdsa;

            
        }
    }
}