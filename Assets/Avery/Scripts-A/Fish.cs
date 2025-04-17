using UnityEngine;

public class Fish : MonoBehaviour
{
    public AudioClip eatSound; // 拖入吃鱼音效
    private AudioSource audioSource;

    void Start()
    {
        // 如果没有 AudioSource，就自动添加
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保熊的Tag是 "Player"
        {
            if (Score.Instance != null)
            {
                Score.Instance.AddScore(1);
            }

            if (eatSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(eatSound);
                Destroy(gameObject, eatSound.length); // 延迟销毁，确保音效播放
            }
            else
            {
                Destroy(gameObject); // 没有音效时立即销毁
            }
        }
    }
}
