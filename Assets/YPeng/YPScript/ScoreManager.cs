using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; // 用于协程

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    public Image foodIcon;                  // 食物图标
    public TMP_Text scoreText;              // 分数文本
    public Sprite[] foodSprites;            // 不同状态的食物图标
    public GameObject levelCompletePanel;    // 通关提示面板（需拖入预制体）
    public TMP_Text completionText;         // 通关提示文字（可选）

    [Header("Score Settings")]
    public int targetScore = 10;            // 目标分数
    public AudioClip levelCompleteSound;    // 通关音效
    public float completionDelay = 1.5f;    // 通关提示延迟时间

    private int _currentScore = 0;
    private bool _isLevelComplete = false;

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

    // 增加分数（公开方法）
    public void AddScore(int points)
    {
        if (_isLevelComplete) return; // 如果已通关则不再计分

        _currentScore = Mathf.Min(_currentScore + points, targetScore);
        UpdateScoreUI();

        // 检查是否通关
        if (_currentScore >= targetScore && !_isLevelComplete)
        {
            _isLevelComplete = true;
            StartCoroutine(ShowLevelComplete());
        }
    }

    // 更新UI显示
    private void UpdateScoreUI()
    {
        scoreText.text = $"{_currentScore}/{targetScore}";

        // 动态图标切换
        if (foodSprites.Length > 0)
        {
            int progress = Mathf.FloorToInt((float)_currentScore / targetScore * foodSprites.Length);
            foodIcon.sprite = foodSprites[Mathf.Clamp(progress, 0, foodSprites.Length - 1)];
        }

        // 分数达标时变色
        if (_currentScore >= targetScore)
        {
            scoreText.color = Color.green;
        }
    }

    // 显示通关提示（协程实现延迟）
    private IEnumerator ShowLevelComplete()
    {
        yield return new WaitForSeconds(completionDelay);

        // 显示通关UI
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            if (completionText != null)
            {
                completionText.text = $"收集完成！\n{_currentScore}/{targetScore}";
            }
        }

        // 播放音效
        if (levelCompleteSound != null)
        {
            AudioSource.PlayClipAtPoint(levelCompleteSound, Camera.main.transform.position);
        }

        // 暂停游戏逻辑（可选）
        Time.timeScale = 0f;
    }

    // === UI按钮调用方法 ===
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        // 示例：加载下一关（需根据实际场景编号修改）
        int nextLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
    }
}