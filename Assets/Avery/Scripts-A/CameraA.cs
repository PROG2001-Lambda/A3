using UnityEngine;

public class CameraA : MonoBehaviour
{
    public Transform target;                         // 熊
    public Vector3 offset = new Vector3(0, 8, -10);   // 世界空间偏移
    public float smoothSpeed = 0.125f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // 跟随熊的世界位置，不考虑熊的朝向
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

        // 摄像机朝向角色
        transform.LookAt(target.position + Vector3.up * 1.5f); // 视线稍微看高一点
    }
}
