using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed = 50f; // 滚动速度
    public float maxScrollDistance = 800f; // 最大滚动距离（你可以根据需要调整）
    
    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // 如果没超过最大滚动距离就继续滚
        if (rectTransform.anchoredPosition.y - startPos.y < maxScrollDistance)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        }
    }

    // 让 Credit 从头开始滚动
    public void ResetPosition()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = startPos;
        }
    }
}
