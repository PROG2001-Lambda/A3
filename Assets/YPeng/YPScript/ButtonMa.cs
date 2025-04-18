using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMa : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioSource clickSound;
    [Tooltip("悬停时的缩放比例（相对于原始大小）")]
    public Vector3 hoverScale = new Vector3(0.9f, 0.9f, 1f); // 缩小到90%
    public float scaleSpeed = 8f;

    private Vector3 originalScale;
    private RectTransform rectTransform;
    private bool isHovered = false; // 添加缺失的变量声明

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        Debug.Log("Initial Scale: " + originalScale);
    }

    void Update()
    {
        Vector3 targetScale = isHovered ? 
            Vector3.Scale(originalScale, hoverScale) : 
            originalScale;

        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );
    }

    // 鼠标悬停时触发
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true; // 设置悬停状态为 true
    }

    // 鼠标离开时触发
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false; // 设置悬停状态为 false
    }

    // 鼠标点击时触发
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            clickSound.Play();
    }
}