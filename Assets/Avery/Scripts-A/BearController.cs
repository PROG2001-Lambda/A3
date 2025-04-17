using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class BearController : MonoBehaviour
{
    public float moveSpeed = 3f;       // 走路速度
    public float turnSpeed = 100f;     // 旋转速度
    public Transform cameraTransform;  // 摄像机的Transform
    public Vector3 offset = new Vector3(0, 8, -10); // 摄像机相对位置偏移
    public float smoothSpeed = 0.125f; // 摄像机平滑速度

    private Rigidbody rb;
    private Animator animator;

    // 用于平滑跟随的速度变量
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameObject.tag = "Player"; // 确保熊有正确的 Tag

        // 设置 Rigidbody 插值，使物理更新更加平滑
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // 调整 Rigidbody 的阻力值来确保移动平稳
        rb.drag = 0.3f; // 设置适当的线性阻力
        rb.angularDrag = 0.1f; // 设置适当的角阻力
    }

    void FixedUpdate()
    {
        // 获取输入
        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // 动画控制：只看前后移动
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // 旋转（A/D 左右转）: 使用物理方式旋转
        Quaternion turnRotation = Quaternion.Euler(0f, turnInput * turnSpeed * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // 反转W/S的方向：
        Vector3 move = Vector3.zero;

        // 处理 W 键（按下 W 时角色应该后退）
        if (moveInput > 0)
        {
            move = -transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime; // W 键后退
        }
        // 处理 S 键（按下 S 时角色应该前进）
        else if (moveInput < 0)
        {
            move = transform.forward * Mathf.Abs(moveInput) * moveSpeed * Time.fixedDeltaTime; // S 键前进
        }

        // 使用物理引擎更新位置
        rb.MovePosition(rb.position + move);
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // 使用 SmoothDamp 来平滑地跟随摄像机
            Vector3 desiredPosition = transform.position + offset;
            cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref velocity, smoothSpeed);

            // 保持摄像机看向目标（角色）
            cameraTransform.LookAt(transform);
        }
    }
}