using StealthGame.Stealth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.AI
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        [Header("General Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float waitTimeAtPoint = 2f;

        [Header("Detection Settings")]
        [SerializeField] private float viewDistance = 12f;
        [SerializeField] private float fieldOfView = 90f;
        [SerializeField] private float hearingRadius = 10f;

        [Header("Chase Settings")]
        [SerializeField] private float chaseTime = 5f;

        private Rigidbody rb;
        private int currentPoint = 0;
        private float waitTimer = 0f;
        private float chaseTimer = 0f;
        private bool isChasing = false;
        private Transform target;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (isChasing && target != null)
            {
                ChaseTarget();
                chaseTimer -= Time.fixedDeltaTime;
                if (chaseTimer <= 0f)
                {
                    isChasing = false;
                    target = null;
                }
            }
            else
            {
                Patrol();
            }
        }

        private void Update()
        {
            DetectPlayer();
        }

        private void Patrol()
        {
            if (patrolPoints.Length == 0) return;

            Transform destination = patrolPoints[currentPoint];
            Vector3 dir = (destination.position - transform.position).normalized;
            Vector3 move = dir * moveSpeed;

            // Apply movement
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            // Rotate smoothly
            if (dir.magnitude > 0.1f)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);
            }

            // Check if reached destination
            if (Vector3.Distance(transform.position, destination.position) < 1f)
            {
                rb.velocity = Vector3.zero;

                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTimeAtPoint)
                {
                    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                    waitTimer = 0f;
                }
            }
        }

        private void ChaseTarget()
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Vector3 move = dir * moveSpeed * 1.5f;

            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            if (dir.magnitude > 0.1f)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);
            }
        }

        private void DetectPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, hearingRadius);
            foreach (Collider hit in hits)
            {
                IDetectable detectable = hit.GetComponent<IDetectable>();
                if (detectable == null) continue;

                Vector3 toTarget = (detectable.GetTransform().position - transform.position);
                float distance = toTarget.magnitude;
                float angle = Vector3.Angle(transform.forward, toTarget.normalized);

                // HEARING
                if (distance <= detectable.GetNoiseLevel())
                {
                    StartChase(detectable.GetTransform());
                    return;
                }

                // VISION
                if (distance <= viewDistance && angle <= fieldOfView / 2f)
                {
                    if (Physics.Raycast(transform.position, toTarget.normalized, out RaycastHit ray, viewDistance))
                    {
                        if (ray.collider.GetComponent<IDetectable>() != null)
                        {
                            StartChase(detectable.GetTransform());
                            return;
                        }
                    }
                }
            }
        }

        private void StartChase(Transform player)
        {
            target = player;
            isChasing = true;
            chaseTimer = chaseTime;
            Debug.Log($"Enemy started chasing {player.name}");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, hearingRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);
        }
    }
}
