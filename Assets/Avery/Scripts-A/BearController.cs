
using UnityEngine;

public class BearController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // 北极熊的移动速度
    public float turnSpeed = 200f; // 北极熊的旋转速度

    [Header("Camera Settings")]
    public Transform cameraTransform; // 用于控制摄像机的位置

    private Rigidbody rb; // 用于物理计算
    private Animator animator; // 用于播放动画

    void Start()
    {
        // 获取北极熊的刚体和动画组件
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // 确保 Rigidbody 的旋转被冻结
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 获取玩家的输入（WASD）
        float horizontal = Input.GetAxis("Horizontal"); // A/D 或者 左/右箭头
        float vertical = Input.GetAxis("Vertical"); // W/S 或者 上/下箭头

        // 计算角色的移动方向（前后移动 + 左右旋转）
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        // 当角色正在移动时
        if (moveDirection.magnitude > 0)
        {
            // 更新动画参数，控制行走动画
            animator.SetFloat("Speed", moveDirection.magnitude);

            // 移动北极熊
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

            // 旋转北极熊：调整为平滑旋转
            // 计算目标旋转角度
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            // 使用 Slerp 平滑旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            // 如果不移动，保持静止动画
            animator.SetFloat("Speed", 0);
        }

        // 更新摄像机位置
        FollowCamera();
    }

    // 摄像机跟随北极熊
    void FollowCamera()
    {
        if (cameraTransform != null)
        {
            // 摄像机在北极熊的后方，并保持一定的高度
            cameraTransform.position = transform.position + new Vector3(0, 5, -10); 
            cameraTransform.LookAt(transform); // 摄像机始终朝向北极熊
        }
    }
}