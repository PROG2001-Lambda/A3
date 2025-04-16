using UnityEngine;

public class CameraF2 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float mouseSensitivity = 2f;
    public float verticalLimit = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isControllingCamera = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        // 默认显示鼠标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1)) // 右键按下时进入控制
        {
            isControllingCamera = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonUp(1)) // 右键松开时退出控制
        {
            isControllingCamera = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isControllingCamera)
        {
            rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY = Mathf.Clamp(rotationY, -verticalLimit, verticalLimit);
        }

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
