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

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (stealthStats == null)
                Debug.LogWarning("StealthStats not assigned to NoiseEmitter!");
        }

        public float GetNoiseLevel()
        {
            // Noise increases with movement speed
            float speed = rb.velocity.magnitude;
            return stealthStats.baseNoiseLevel * speed * movementNoiseMultiplier;
        }

        public float GetVisibilityLevel()
        {
            // Use this to simulate light/shadow in future
            return stealthStats.baseVisibility;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
