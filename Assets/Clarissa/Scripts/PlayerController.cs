using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    private float movementX;
    private float movementY;

    public float speed = 5f;
    public float rotationSpeed = 10f;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // 控制动画播放或停止
        if (animator != null)
        {
            float movementMagnitude = new Vector2(movementX, movementY).magnitude;
            animator.speed = movementMagnitude > 0.01f ? 1f : 0f;
        }
    }

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
            countText.text = count.ToString() + "/10";

        if (count >= 10 && winTextObject != null)
            winTextObject.SetActive(true);
    }
}
