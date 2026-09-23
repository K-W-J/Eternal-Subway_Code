using System.Collections.Generic;
using UnityEngine;

namespace _01_Scripts.BuildObjects
{
    public class BuildObject : MonoBehaviour
    {
        [field:SerializeField] public List<Transform> BuildPoints = new List<Transform>();
        
        
    }
}