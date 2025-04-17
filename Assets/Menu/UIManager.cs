using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject creditPanel;       // 拖入包含整个 Credit 的 UI 面板
    public CreditScroll creditScroll;    // 拖入挂着 CreditScroll 的对象（一般是 CreditContent）

    public void ShowCredits()
    {
        creditPanel.SetActive(true);          // 显示面板
        creditScroll.ResetPosition();         // 重置滚动位置
    }

    public void HideCredits()
    {
        creditPanel.SetActive(false);         // 隐藏面板
    }
}
