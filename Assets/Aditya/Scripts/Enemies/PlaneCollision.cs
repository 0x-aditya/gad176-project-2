using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aditya.Scripts.Enemies
{
    public class PlaneCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            // if player falls off the level
            Destroy(other.gameObject);
        }
    }
}
