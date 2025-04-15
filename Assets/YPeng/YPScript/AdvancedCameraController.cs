using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    [Header("基础设置")]
    public Transform player;               // 绑定的角色对象
    public Vector3 baseOffset = new Vector3(0, 2f, -5f); // 基础偏移量 (高度Y，前后Z)

    [Header("平滑跟随")]
    [Tooltip("值越小跟随越平滑 (0.01-0.2)")]
    public float smoothFactor = 0.1f;      // 平滑系数

    [Header("防穿墙设置")]
    public LayerMask collisionMask;        // 碰撞检测层 (建议选择环境层)
    public float wallBuffer = 0.8f;        // 防穿墙缓冲距离
    public float minDistance = 1.5f;       // 相机最小距离

    private Vector3 currentOffset;         // 动态偏移量

    void Start()
    {
        // 初始化偏移
        currentOffset = baseOffset;
    }

    void LateUpdate()
    {
        HandleCameraZoom();      // 保留缩放功能
        FollowPlayerWithCollision();
    }

    // 处理滚轮缩放（可选）
    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // 沿当前方向缩放
            Vector3 zoomDirection = currentOffset.normalized;
            float newMagnitude = Mathf.Clamp(
                currentOffset.magnitude - scroll * 5f, // 缩放速度
                minDistance, 
                baseOffset.magnitude // 最大距离保持初始值
            );
            currentOffset = zoomDirection * newMagnitude;
        }
    }

    // 带碰撞检测的跟随（优化版）
    void FollowPlayerWithCollision()
    {
        Vector3 desiredPosition = player.position + currentOffset;
        RaycastHit hit;

        // 使用球形射线检测（更准确）
        if (Physics.SphereCast(
            player.position,
            0.3f, // 检测半径
            currentOffset.normalized,
            out hit,
            currentOffset.magnitude,
            collisionMask
        ))
        {
            // 计算安全距离（确保不小于最小距离）
            float safeDistance = Mathf.Max(hit.distance - wallBuffer, minDistance);
            desiredPosition = player.position + currentOffset.normalized * safeDistance;
        }

        // 平滑移动并更新实际偏移
        transform.position = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothFactor
        );
        currentOffset = transform.position - player.position;

        // 保持注视角色（看向胸部高度）
        transform.LookAt(player.position + Vector3.up * 1.2f);
    }

    // 调试可视化
    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(player.position, transform.position);
            Gizmos.DrawWireSphere(transform.position, 0.3f);
        }
    }
}