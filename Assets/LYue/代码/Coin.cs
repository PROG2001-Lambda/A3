using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public float rotateSpeed = 100f;
    public ParticleSystem collectEffect; // 收集特效(可选)

    void Update()
    {
        // 让金币旋转
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // 确保只有玩家能收集
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        // 播放收集特效
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // 通知游戏管理器
        GameManager.Instance.AddCoin(coinValue);

        // 销毁金币对象
        Destroy(gameObject);
    }
}