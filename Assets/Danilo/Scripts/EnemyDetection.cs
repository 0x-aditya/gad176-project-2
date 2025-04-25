using StealthGame.Stealth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.AI
{
    public class EnemyDetection : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float hearingRadius = 10f;
        [SerializeField] private float viewDistance = 15f;
        [SerializeField] private float fieldOfView = 90f;

        private void Update()
        {
            DetectTargets();
        }

        private void DetectTargets()
        {
            // Overlap sphere around the enemy
            Collider[] hits = Physics.OverlapSphere(transform.position, hearingRadius);
            foreach (var hit in hits)
            {
                IDetectable detectable = hit.GetComponent<IDetectable>();
                if (detectable == null) continue;

                float distance = Vector3.Distance(transform.position, detectable.GetTransform().position);

                // --- NOISE DETECTION ---
                float noise = detectable.GetNoiseLevel();
                if (distance <= noise)
                {
                    Debug.Log($"[Enemy] Heard {hit.name} from {distance:F1} units.");
                }

                // --- VISUAL DETECTION ---
                Vector3 dirToTarget = (detectable.GetTransform().position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, dirToTarget);

                if (angle <= fieldOfView / 2f)
                {
                    // Raycast to ensure no obstacles block the view
                    if (Physics.Raycast(transform.position, dirToTarget, out RaycastHit rayHit, viewDistance))
                    {
                        if (rayHit.collider.GetComponent<IDetectable>() != null)
                        {
                            Debug.Log($"[Enemy] Saw {hit.name} at {distance:F1} units.");
                        }
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, hearingRadius);
        }
    }
}
