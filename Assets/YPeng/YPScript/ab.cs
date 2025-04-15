using UnityEngine;

public class ab : MonoBehaviour
{
    [Header("基础设置")]
    public GameObject player;          // 角色对象
    public Vector3 baseOffset = new Vector3(0, 2f, -5f); // 基础偏移量

    [Header("视角控制")]
    public float rotateSpeed = 100f;   // 视角旋转速度
    public float minVerticalAngle = -20f; // 垂直旋转最小角度
    public float maxVerticalAngle = 80f;  // 垂直旋转最大角度

    private Vector3 offset;           // 动态偏移量
    private float currentHorizontalAngle = 0f; // 当前水平旋转角度
    private float currentVerticalAngle = 20f;  // 当前垂直旋转角度

    void Start()
    {
        // 初始化偏移量和角度
        offset = baseOffset;
        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        HandleCameraRotation();
        UpdateCameraPosition();
    }

    // 处理鼠标旋转
    void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1)) // 按住右键旋转视角
        {
            // 获取鼠标输入
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            // 更新旋转角度（限制垂直角度）
            currentHorizontalAngle += mouseX;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle - mouseY, minVerticalAngle, maxVerticalAngle);

            // 计算新偏移量
            Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0);
            offset = rotation * baseOffset;
        }
    }

    // 更新相机位置
    void UpdateCameraPosition()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
    }
}