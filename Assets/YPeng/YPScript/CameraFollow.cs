using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // CameraTarget
    public Vector3 offset = new Vector3(0, 2, -5); // 初始相机位置偏移
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

        // 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // 鼠标输入
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 限制上下视角
        rotationY = Mathf.Clamp(rotationY, -verticalLimit, verticalLimit);

        // 计算相机旋转和位置
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
