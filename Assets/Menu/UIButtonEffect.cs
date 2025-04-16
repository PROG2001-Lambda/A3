using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioSource clickSound; // 拖入 AudioManager 上的 AudioSource
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    public float scaleSpeed = 10f;

    private Vector3 originalScale;
    private bool isHovered = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 平滑缩放
        if (isHovered)
            transform.localScale = Vector3.Lerp(transform.localScale, hoverScale, Time.deltaTime * scaleSpeed);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            clickSound.Play();

        // 可以自定义点击后执行的行为，比如切换场景、播放动画等
        Debug.Log($"{gameObject.name} Button Clicked");
    }
}
