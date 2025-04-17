using UnityEngine;

public class Fish : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保熊的Tag是 "Player"
        {
            if (Score.Instance != null)
            {
                Score.Instance.AddScore(1);
            }
            Destroy(gameObject); // 鱼被吃掉
        }
    }
}
