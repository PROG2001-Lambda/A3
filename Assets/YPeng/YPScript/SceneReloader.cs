using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        // 获取当前活动场景的索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneIndex);

        // 可选：重置时间缩放（如果游戏曾被暂停）
        Time.timeScale = 1f;
    }
}