using UnityEngine;

namespace YourGame.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 3f;
        
        [Header("Raycast Settings")]
        [SerializeField] private float playerDetectionRange = 10f;
        [SerializeField] private float wallCheckDistance    = 2f;
        [SerializeField] private LayerMask wallMask;
        [SerializeField] private LayerMask playerMask;

        private Rigidbody _rb;
        private Vector3   _moveDir;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _moveDir = transform.forward;
        }

        void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, _moveDir, out RaycastHit wallHit, wallCheckDistance))
            {
                if (wallHit.transform.gameObject.CompareTag("Wall") || wallHit.transform.gameObject.CompareTag("Enemy"))
                {
                    transform.Rotate(0f, 180f, 0f);
                    _moveDir = -_moveDir;
                } 
            }

            if (Physics.Raycast(transform.position, _moveDir, out RaycastHit playerHit, playerDetectionRange, playerMask))
            {
                Debug.Log($"Enemy spotted player at {playerHit.distance:F1} units!");
            }

            Vector3 vel = _moveDir.normalized * speed;
            vel.y = _rb.velocity.y;
            _rb.velocity = vel;
        }

    }
}