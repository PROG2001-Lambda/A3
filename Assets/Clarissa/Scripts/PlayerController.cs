using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player
    private Rigidbody rb;

    // Movement input values
    private float movementX;
    private float movementY;

    // Speed at which the player moves
    public float speed = 5f;

    // Rotation speed (optional)
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Optional: Add drag to reduce sliding
        rb.drag = 1f;
        rb.angularDrag = 0.05f;

        // Optional: Lock XZ rotation to prevent turtle flipping
        rb.freezeRotation = true;
    }

    // Called by the Input System when movement input is detected
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Called at fixed intervals for physics updates
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply direct velocity movement
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        // Optional: make turtle face movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
