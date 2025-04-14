using UnityEngine;

public class YpRotator : MonoBehaviour
{
    [SerializeField] float x = 0f;
    [SerializeField] float y = 100f; // 绕Y轴旋转
    [SerializeField] float z = 0f;

    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}