using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间

public class Score : MonoBehaviour
{
    public static Score Instance;

    public TMP_Text scoreText;      // UI 文本组件（TextMeshPro）
    public GameObject winPanel;     // 胜利菜单面板

    private int score = 0;
    public int maxScore = 10;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
        winPanel.SetActive(false); // 游戏开始时隐藏胜利菜单
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
        scoreText.text = score + "/" + maxScore;
    }

    void ShowWinMenu()
    {
        winPanel.SetActive(true);
       // Time.timeScale = 0f; // 暂停游戏
    }
}
