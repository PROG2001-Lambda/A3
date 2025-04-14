using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    public Image foodIcon;          // 食物图标（拖入UI中的Image组件）
    public TMP_Text scoreText;      // 分数文本（格式：3/10）
    public Sprite[] foodSprites;    // 不同状态的食物图标（可选）

    [Header("Score Settings")]
    public int targetScore = 10;    // 目标分数
    private int _currentScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 增加分数
    public void AddScore(int points)
    {
        _currentScore = Mathf.Min(_currentScore + points, targetScore);
        UpdateScoreUI();

        // 触发音效/特效
        Debug.Log($"获得 {points} 分，当前: {_currentScore}/{targetScore}");
    }

    // 更新UI显示
    private void UpdateScoreUI()
    {
        // 更新分数文本
        scoreText.text = $"{_currentScore}/{targetScore}";

        // 动态切换图标（可选）
        if (foodSprites.Length > 0)
        {
            int progress = Mathf.FloorToInt((float)_currentScore / targetScore * foodSprites.Length);
            foodIcon.sprite = foodSprites[Mathf.Clamp(progress, 0, foodSprites.Length - 1)];
        }
    }
}