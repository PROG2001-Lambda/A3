using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    [Header("基础设置")]
    public Transform player;               // 绑定的角色对象
    public Vector3 baseOffset = new Vector3(0, 2f, -5f); // 基础偏移量 (高度Y，前后Z)

    [Header("平滑跟随")]
    [Tooltip("值越小跟随越平滑 (0.01-0.2)")]
    public float smoothFactor = 0.1f;      // 平滑系数

    [Header("鼠标控制")]
    public float rotateSpeed = 100f;       // 视角旋转速度
    public float zoomSpeed = 10f;          // 滚轮缩放速度
    public float minZoom = 2f;             // 最小缩放距离
    public float maxZoom = 15f;            // 最大缩放距离

    [Header("防穿墙设置")]
    public LayerMask collisionMask;        // 碰撞检测层 (建议选择环境层)
    public float wallBuffer = 0.5f;        // 防穿墙缓冲距离

    private Vector3 currentOffset;         // 动态偏移量
    private float currentZoom;             // 当前缩放值

    void Start()
    {
        // 初始化偏移和缩放
        currentOffset = baseOffset;
        currentZoom = baseOffset.magnitude;
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标到窗口中心
    }

    void LateUpdate()
    {
        HandleCameraRotation();
        HandleCameraZoom();
        FollowPlayerWithCollision();
    }

    // 处理鼠标旋转
    void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1)) // 按住右键旋转
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            // 垂直角度限制（避免翻转）
            float newYAngle = Mathf.Clamp(transform.eulerAngles.x + mouseY, 10f, 80f);
            
            // 绕角色旋转
            transform.RotateAround(player.position, Vector3.up, mouseX);
            transform.rotation = Quaternion.Euler(newYAngle, transform.eulerAngles.y, 0);
            
            // 更新动态偏移
            currentOffset = transform.position - player.position;
        }
    }

    // 处理滚轮缩放
    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll * zoomSpeed, minZoom, maxZoom);
            currentOffset = currentOffset.normalized * currentZoom;
        }
    }

    // 带碰撞检测的跟随
    void FollowPlayerWithCollision()
    {
        Vector3 desiredPosition = player.position + currentOffset;
        RaycastHit hit;

        // 从角色向相机方向发射射线
        if (Physics.Linecast(player.position, desiredPosition, out hit, collisionMask))
        {
            // 遇到障碍物时调整位置
            float safeDistance = Vector3.Distance(player.position, hit.point) - wallBuffer;
            desiredPosition = player.position + currentOffset.normalized * safeDistance;
        }

        // 平滑移动
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor);
        
        // 始终面向角色
        transform.LookAt(player);
    }
}