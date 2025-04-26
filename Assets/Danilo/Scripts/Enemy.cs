using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Enemies
{
    public class Enemy : BaseEnemy
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private float detectionRange = 8f; // How far enemy can detect the player
        [SerializeField] private float chaseSpeedMultiplier = 1.5f; // Move faster when chasing

        private Transform playerTransform;
        private bool isChasing = false;

        protected override void Start()
        {
            base.Start();

            // find the player by player tag
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("there is no player on the scene");
            }
        }

        protected override void Update()
        {
            if (playerTransform == null)
                return;

            // calculate distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // if the player gets detected by the enemy, chase them
            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
                targetPosition = playerTransform.position;
            }
            else
            {
                // otherwise dont chase
                isChasing = false;
            }

            base.Update();
        }

        protected override void MoveTowardsTarget()
        {
            if (isChasing)
            {
                // while chasing the player, move towards them
                Vector3 chaseDirection = (targetPosition - transform.position).normalized;
                transform.position += chaseDirection * moveSpeed * chaseSpeedMultiplier * Time.deltaTime;
            }
            else
            {
                // otherwise just keep moving to the next target
                base.MoveTowardsTarget();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // if the enemy collides with something that doesnt have the player tag display debug log warning
            if (collision == null)
            {
                Debug.LogWarning("enemy didn't collide with anything");
                return;
            }

            // if the enemy collides with the player tag then do handle player caught
            if (collision.gameObject.CompareTag(playerTag))
            {
                Debug.Log("looks like you got caught");
                HandlePlayerCaught();
            }
        }

        private void HandlePlayerCaught()
        {
            //when the player is caught time is frozen
            Debug.Log("you lost, better luck next time");
            Time.timeScale = 0f;
        }
    }
}