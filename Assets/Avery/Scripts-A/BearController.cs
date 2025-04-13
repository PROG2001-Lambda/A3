using UnityEngine;

public class BearController : MonoBehaviour
{
    public float moveSpeed = 5f; // 北极熊的移动速度
    public float turnSpeed = 200f; // 北极熊转向速度
    private Rigidbody rb; // 用于控制物理

    void Start()
    {
        // 获取北极熊的刚体组件
        rb = GetComponent<Rigidbody>();

        // 确保北极熊的位置在地面之上（Y轴设置在3以确保在地面上）
        if (transform.position.y < 3f)
        {
            transform.position = new Vector3(transform.position.x, 3f, transform.position.z); // 这里将Y设为3
        }

        // 确保刚体没有勾选 isKinematic
        rb.isKinematic = false;
        rb.useGravity = true; // 允许重力
    }

    void Update()
    {
        // 获取玩家输入
        float horizontal = Input.GetAxis("Horizontal"); // A/D 或者 左/右箭头
        float vertical = Input.GetAxis("Vertical"); // W/S 或者 上/下箭头

        // 移动方向（前后和左右）
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        // 控制北极熊的旋转
        if (horizontal != 0)
        {
            transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        }

        // 移动北极熊（不直接设置 transform.position，使用 Rigidbody 的 MovePosition）
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(targetPosition);

        // 限制北极熊活动范围
        LimitMovement();
    }

    // 限制北极熊在地面上的活动范围
    void LimitMovement()
    {
        // 获取地面（Cube）的范围
        float groundSizeX = 150f; // Cube 尺寸设定为 150x3x150
        float groundSizeZ = 150f;

        // 获取当前北极熊的位置
        Vector3 currentPosition = transform.position;

        // 限制 X 和 Z 坐标不超过 Cube 边界
        float clampedX = Mathf.Clamp(currentPosition.x, -groundSizeX / 2f, groundSizeX / 2f);
        float clampedZ = Mathf.Clamp(currentPosition.z, -groundSizeZ / 2f, groundSizeZ / 2f);

        // 设置新的位置
        transform.position = new Vector3(clampedX, currentPosition.y, clampedZ);
    }
}
