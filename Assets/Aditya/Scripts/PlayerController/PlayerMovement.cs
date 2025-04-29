using UnityEngine;

namespace Aditya.Scripts.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float groundCheckDistance = 2f;
        [SerializeField] private float mouseSensitivity = 2f;

        private float _yaw;
        private Rigidbody _rb;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            if (GetComponent<Rigidbody>() == null)
            {
                _rb = gameObject.AddComponent<Rigidbody>();
                print("Rigidbody component added to the player.");
            }
            else
            {
                _rb = GetComponent<Rigidbody>();
                print("rb");
            }
        }
        void Update()
        {
            // movement
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = transform.forward * v + transform.right * h;

            float inputMagnitude = dir.magnitude;

            if (inputMagnitude > 0.1f)
            {
                Vector3 moveDirection = dir.normalized;

                Vector3 newPosition = transform.position + moveDirection * (movementSpeed * Time.deltaTime);
                transform.position = newPosition;
            }
            // jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 origin = transform.position + Vector3.up * 0.1f;
                if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, groundCheckDistance))
                {
                    _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
            // mouse look
            float mouseX = Input.GetAxis("Mouse X");
            _yaw += mouseX * mouseSensitivity;
            transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
        }
    }
}
