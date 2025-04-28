using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed = 2f;
        [SerializeField] protected float reachDistance = 0.5f;
        [SerializeField] protected float roamRadius = 10f;
        [SerializeField] protected float idleTime = 2f;
        [SerializeField] protected float minimumMoveDistance = 2f;
        [SerializeField] protected float maxChaseTime = 5f;

        protected Vector3 roamCenter;
        protected Vector3 targetPosition;
        protected bool isWaiting = false;
        protected float waitTimer = 0f;
        protected float chaseTimer = 0f;

        protected virtual void Start()
        {
            // the enemy starts roaming
            roamCenter = transform.position;
            SetNewRoamTarget();
        }

        protected virtual void Update()
        {
            if (isWaiting)
            {
                // idle counter counts down
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    isWaiting = false;
                    SetNewRoamTarget();
                }
                return;
            }

            MoveTowardsTarget();
        }

        protected virtual void MoveTowardsTarget()
        {
            // enemy moves towards the current target position
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            chaseTimer += Time.deltaTime;
            
            // if the enemy has reached the target, wait a bit
            if (Vector3.Distance(transform.position, targetPosition) <= reachDistance)
            {
                StartWaiting();
            }
            // if the enemy is taking too long to reach the target, find a new one
            else if (chaseTimer >= maxChaseTime)
            {
                Debug.LogWarning("enemy procrastinated too long, picking a new roam target");
                SetNewRoamTarget();
            }
        }

        protected virtual void SetNewRoamTarget()
        {
            // pick a new target randomly within the roam radius
            Vector2 randomCircle = Random.insideUnitCircle * roamRadius;
            Vector3 targetOffset = new Vector3(randomCircle.x, 0f, randomCircle.y);

            // make sure the enemy's movement isnt small
            if (targetOffset.magnitude < minimumMoveDistance)
            {
                targetOffset = targetOffset.normalized * minimumMoveDistance;
            }

            targetPosition = roamCenter + targetOffset;
            chaseTimer = 0f;
            Debug.Log($"enemy found new target position {targetPosition}");
        }

        protected virtual void StartWaiting()
        {
            // the enemy starts waiting at their current position
            isWaiting = true;
            waitTimer = idleTime;
            Debug.Log("enemy is procrastinating");
        }

        protected virtual void OnDrawGizmosSelected()
        {
            // draw a gizmo in editor mode to show the roam radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, roamRadius);
        }
    }
}
