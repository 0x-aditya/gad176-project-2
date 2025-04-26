using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Stealth
{
    [RequireComponent(typeof(Rigidbody))]
    public class NoiseEmitter : MonoBehaviour, IDetectable
    {
        [SerializeField] private StealthStats stealthStats;
        [SerializeField] private float movementNoiseMultiplier = 1.5f;

        private Rigidbody rb;

        // get the rigid body and if it not assigned display debug log warning
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (stealthStats == null)
                Debug.LogWarning("StealthStats not assigned to NoiseEmitter!");
        }

        // calculate and returns noise level based on the player's movement
        public float GetNoiseLevel()
        {
            // Noise increases with movement speed
            float speed = rb.velocity.magnitude;
            return stealthStats.baseNoiseLevel * speed * movementNoiseMultiplier;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
