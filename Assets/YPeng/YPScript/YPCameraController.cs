using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPCameraController : MonoBehaviour
{
    public GameObject player; // 角色对象
    private Vector3 offset;   // 相机与角色之间的偏移量

    void Start()
    {
        // 计算相机与玩家的初始偏移量
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // 让相机跟随玩家
        transform.position = player.transform.position + offset;
    }
}
