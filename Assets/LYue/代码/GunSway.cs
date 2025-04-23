using UnityEngine;

public class GunSway : MonoBehaviour
{
    [Header("基础设置")]
    public ThirdPersonMove playerMovement;
    public Transform gunTransform; // 枪的Transform（如果脚本不直接挂在枪上）

    [Header("前后摆动")]
    public float forwardSwayAmount = 3f; // 前后摆动幅度
    public float forwardSwaySpeed = 2f;  // 前后摆动速度

    [Header("左右倾斜")]
    public float sideTiltAmount = 2f;   // 左右倾斜幅度
    public float tiltResponseSpeed = 5f; // 倾斜响应速度

    [Header("跑步效果")]
    public float runSwayMultiplier = 1.5f; // 跑步时的摆动幅度倍增
    public float runSpeedThreshold = 4f;   // 视为跑步的速度阈值

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    private float currentSwayPhase;

    void Start()
    {
        // 如果没有指定gunTransform，默认使用脚本所在对象的Transform
        if (gunTransform == null) gunTransform = transform;

        initialLocalPosition = gunTransform.localPosition;
        initialLocalRotation = gunTransform.localRotation;
    }

    void Update()
    {
        if (playerMovement == null) return;

        // 计算摆动因子（基于移动状态和速度）
        float swayFactor = CalculateSwayFactor();

        // 前后摆动（基于正弦波）
        HandleForwardBackSway(swayFactor);

        // 左右倾斜（基于移动方向）
        HandleSideTilt();

        // 重置位置（防止累积误差）
        gunTransform.localPosition = Vector3.Lerp(
            gunTransform.localPosition,
            initialLocalPosition,
            Time.deltaTime * 5f
        );
    }

    float CalculateSwayFactor()
    {
        float baseFactor = playerMovement.IsMoving ? 1f : 0f;

        // 跑步时增加摆动幅度
        if (playerMovement.CurrentSpeed > runSpeedThreshold)
        {
            baseFactor *= runSwayMultiplier;
        }

        return baseFactor * (playerMovement.CurrentSpeed / playerMovement.speed);
    }

    void HandleForwardBackSway(float swayFactor)
    {
        if (playerMovement.IsMoving)
        {
            // 更新摆动相位
            currentSwayPhase += Time.deltaTime * forwardSwaySpeed;

            // 计算前后摆动角度（使用缓动函数使更自然）
            float swayAngle = Mathf.Sin(currentSwayPhase) * forwardSwayAmount * swayFactor;

            // 应用旋转（X轴旋转）
            gunTransform.localRotation = initialLocalRotation * Quaternion.Euler(swayAngle, 0, 0);
        }
        else
        {
            // 不移动时平滑复位
            gunTransform.localRotation = Quaternion.Lerp(
                gunTransform.localRotation,
                initialLocalRotation,
                Time.deltaTime * tiltResponseSpeed
            );
            currentSwayPhase = 0f;
        }
    }

    void HandleSideTilt()
    {
        if (playerMovement.IsMoving)
        {
            // 基于移动方向计算倾斜角度（左右移动时更明显）
            float tiltAngle = -playerMovement.MoveDirection.x * sideTiltAmount;

            // 应用Z轴旋转（左右倾斜）
            Quaternion targetTilt = Quaternion.Euler(0, 0, tiltAngle);
            gunTransform.localRotation = Quaternion.Lerp(
                gunTransform.localRotation,
                gunTransform.localRotation * targetTilt,
                Time.deltaTime * tiltResponseSpeed
            );
        }
    }
}