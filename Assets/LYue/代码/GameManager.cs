using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    public Text coinText;
    public Text scoreText;

    private int coinsCollected = 0;
    private int totalScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int value)
    {
        coinsCollected += value;
        totalScore += value * 10; // 假设每个金币10分
        UpdateUI();

        Debug.Log($"收集金币! 总数: {coinsCollected} 分数: {totalScore}");
    }

    void UpdateUI()
    {
        if (coinText != null) coinText.text = $"金币: {coinsCollected}";
        if (scoreText != null) scoreText.text = $"分数: {totalScore}";
    }
}