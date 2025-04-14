using UnityEngine;

public class YpCoinCollector : MonoBehaviour
{
    [Header("Settings")]
    public int coinValue = 1;       // 单个金币分值
    public AudioClip collectSound;  // 收集音效

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        // 增加分数
        ScoreManager.Instance.AddScore(coinValue);

        // 播放音效
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // 销毁金币
        Destroy(gameObject);
    }
}