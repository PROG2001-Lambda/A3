using UnityEngine;

public class HelpUIController : MonoBehaviour
{
    public GameObject helpPanel;

    // 打开 Help 面板
    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    // 关闭 Help 面板
    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}
