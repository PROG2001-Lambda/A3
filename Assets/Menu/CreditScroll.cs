using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed = 50f;             // 滚动速度
    public float maxScrollDistance = 800f;      // 最大滚动距离（根据文本长度调整）

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;

        ResetPosition(); // 确保进入页面时从头开始
    }

    void Update()
    {
        // 使用 unscaledDeltaTime 解决 Time.timeScale = 0 导致卡住的问题
        if (rectTransform.anchoredPosition.y - startPos.y < maxScrollDistance)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.unscaledDeltaTime;
        }
    }

    // 重置滚动位置（可供外部调用）
    public void ResetPosition()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = startPos;
        }
    }
}

