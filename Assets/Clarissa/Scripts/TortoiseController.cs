using UnityEngine;
using UnityEngine.InputSystem;

public class TortoiseController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    private Vector2 moveInput;
    private Vector2 mousePosition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    void Update()
    {
        Move();
        RotateTowardsMouse();
    }

    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - transform.position).normalized;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
