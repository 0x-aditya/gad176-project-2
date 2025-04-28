using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 movementInput;
    private Vector3 moveDirection;

    // gets the rigid body and makes sure that there is a rigid body component on the player
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError("rigid body is not found on the player");
    }

    private void Update()
    {
        // gets the input from the keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // calculates movement direction
        movementInput = new Vector3(horizontal, 0f, vertical);
        moveDirection = movementInput.normalized;

        // rotate only if the player is moving
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // apply velocity
        rb.velocity = moveDirection * moveSpeed + new Vector3(0, rb.velocity.y, 0);
    }

    public Vector3 GetMoveDirection() => moveDirection;

    // check if the player is moving
    public bool IsMoving()
    {
        return moveDirection.magnitude > 0.1f;
    }
}
