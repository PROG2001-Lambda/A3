using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 目标（角色）
    public Vector3 offset = new Vector3(0, 5, -10);  // 相机与目标的偏移量
    public float followSpeed = 2f;  // 跟随速度
    public float rotationSpeed = 5f;  // 相机旋转速度（鼠标控制）

    private float currentRotationX = 0f;  // 当前水平旋转角度
    private float currentRotationY = 0f;  // 当前垂直旋转角度

    void Update()
    {
        // 获取鼠标的输入
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // 更新水平和垂直旋转角度
        currentRotationX += mouseX;
        currentRotationY -= mouseY;

        // 限制垂直旋转角度，避免相机翻转
        currentRotationY = Mathf.Clamp(currentRotationY, -40f, 80f);

        // 旋转相机
        transform.eulerAngles = new Vector3(currentRotationY, currentRotationX, 0);

        // 平滑地跟随角色
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}

