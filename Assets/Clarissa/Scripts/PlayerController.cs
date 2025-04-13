using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player
    private Rigidbody rb;

    // Movement input values
    private float movementX;
    private float movementY;

    // Speed at which the player moves
    public float speed = 5f;

    // Rotation speed
    public float rotationSpeed = 10f;

    // UI text to display pick-up count
    public TextMeshProUGUI countText;

    // GameObject to display win message
    public GameObject winTextObject;

    // Number of items picked up
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Optional: tuning physics
        rb.drag = 1f;
        rb.angularDrag = 0.05f;
        rb.freezeRotation = true;

        count = 0;
        SetCountText();
        if (winTextObject != null)
            winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Set direct velocity
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        // Face the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    // Handle collision with pick-up items
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        if (countText != null)
            countText.text = "Count: " + count.ToString();

        if (count >= 5 && winTextObject != null)
            winTextObject.SetActive(true);
    }
}
