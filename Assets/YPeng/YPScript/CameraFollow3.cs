using UnityEngine;

public class CameraFollow3 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float mouseSensitivity = 2f;
    public float distance = 5f;
    public float verticalLimit = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        // 不锁定鼠标，不隐藏光标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        // 鼠标输入
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY = Mathf.Clamp(rotationY, -verticalLimit, verticalLimit);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
