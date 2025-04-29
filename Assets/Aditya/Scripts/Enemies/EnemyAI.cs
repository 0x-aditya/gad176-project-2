using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aditya.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 3f;
        
        [Header("Raycast Settings")]
        [SerializeField] private float playerDetectionRange = 10f;
        [SerializeField] private float wallCheckDistance    = 2f;

        private Rigidbody _rb;
        private Vector3   _moveDir;
        private bool _flag;
        private GameObject _player;
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _moveDir = transform.forward;
            _flag = false;
        }

        void FixedUpdate()
        {
            // raycast for player detection
            if (Physics.Raycast(transform.position, _moveDir, out RaycastHit playerHit, playerDetectionRange))
            {
                if (playerHit.transform.gameObject.CompareTag("Player"))
                {
                    _flag = true;
                    _player = playerHit.transform.gameObject;
                    // once the flag is set to true, the enemy will move towards the player
                }
            }
            if (Physics.Raycast(transform.position, _moveDir, out RaycastHit wallHit, wallCheckDistance))
            {
                if (wallHit.transform.gameObject.CompareTag("Wall") || wallHit.transform.gameObject.CompareTag("Enemy"))
                {
                    transform.Rotate(0f, 180f, 0f);
                    _moveDir = Vector3.zero - _moveDir;
                } 
            }
            if (_flag)
            {
                _moveDir = (_player.transform.position - transform.position).normalized;// movement to player
                _flag = false;
            }

            Vector3 vel = _moveDir.normalized * speed;
            vel.y = _rb.velocity.y; // maintain the y velocity for jumping
            _rb.velocity = vel; // adding velocity to the rigidbody
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("Aditya/Scenes/End");
                // edns game if enemy collides with player
            }
        }
    }
}