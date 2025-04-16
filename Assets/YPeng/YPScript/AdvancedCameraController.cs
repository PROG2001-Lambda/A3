using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    [Header("基础设置")]
    public Transform player;               // 绑定的角色对象
    public Vector3 baseOffset = new Vector3(0, 2f, -5f); // 基础偏移量

    [Header("平滑跟随")]
    [Tooltip("值越小跟随越平滑 (0.01-0.2)")]
    public float smoothFactor = 0.1f;

    [Header("鼠标控制")]
    public float rotateSpeed = 100f;       // 视角旋转速度
    public float zoomSpeed = 10f;          // 滚轮缩放速度
    public float minZoom = 2f;             // 最小缩放距离
    public float maxZoom = 15f;            // 最大缩放距离

    [Header("防穿墙设置")]
    public LayerMask collisionMask;        // 碰撞检测层
    public float wallBuffer = 0.5f;        // 防穿墙缓冲距离

    private float currentZoom;             // 当前缩放值
    private Vector3 baseOffsetNormalized; // 归一化的基础偏移
    private float yaw;                     // 水平旋转角度
    private float pitch;                   // 垂直旋转角度

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentZoom = baseOffset.magnitude;
        baseOffsetNormalized = baseOffset.normalized;
        
        // 初始化旋转角度
        Vector3 initialEuler = transform.rotation.eulerAngles;
        pitch = initialEuler.x;
        yaw = initialEuler.y;
    }

    void LateUpdate()
    {
        HandleCameraRotation();
        HandleCameraZoom();
        FollowPlayerWithCollision();
    }

    void HandleCameraRotation()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        // 更新旋转角度
        yaw += mouseX;
        pitch -= mouseY; // 反转Y轴输入
        pitch = Mathf.Clamp(pitch, 10f, 80f); // 限制垂直角度

        // 计算新的偏移方向
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 rotatedOffset = rotation * baseOffsetNormalized * currentZoom;
        transform.rotation = rotation;
    }

    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);
        }
    }

    void FollowPlayerWithCollision()
    {
        // 计算目标位置
        Vector3 targetOffset = Quaternion.Euler(pitch, yaw, 0) * baseOffsetNormalized * currentZoom;
        Vector3 desiredPosition = player.position + targetOffset;

        // 碰撞检测
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredPosition, out hit, collisionMask))
        {
            float safeDistance = hit.distance - wallBuffer;
            desiredPosition = player.position + targetOffset.normalized * safeDistance;
        }

        // 平滑移动
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor);
    }
}