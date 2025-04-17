using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [Header("UI 引用")]
    public TMP_Text scoreText;
    public GameObject winPanel;

    [Header("分数设置")]
    private int score = 0;
    public int maxScore = 10;

    [Header("胜利音效")]
    public AudioClip winSound;         // ✅ 胜利音效
    private AudioSource audioSource;   // 用于播放音效

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (scoreText == null)
            Debug.LogError("ScoreText 没有设置！");
        if (winPanel == null)
            Debug.LogError("WinPanel 没有设置！");

        winPanel.SetActive(false);
        UpdateScoreText();

        // 获取 AudioSource，如果没有则添加一个
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score >= maxScore)
        {
            ShowWinMenu();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = score + "/" + maxScore;
    }

    void ShowWinMenu()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;

        // ✅ 播放胜利音效（确保先暂停游戏，再播放音效）
        if (winSound != null && audioSource != null)
        {
            // 暂停游戏后播放声音需要使用 PlayOneShot 不受 Time.timeScale 影响
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        winPanel.SetActive(false);
    }

    public int GetScore()
    {
        return score;
    }
}
