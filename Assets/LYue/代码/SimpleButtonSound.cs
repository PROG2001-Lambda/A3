using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleButtonSound : MonoBehaviour, IPointerClickHandler
{
    public AudioSource clickSound; // 拖入你的 AudioSource

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            clickSound.Play();

        Debug.Log("按钮点击音效播放"); // 可选：测试用
    }
}