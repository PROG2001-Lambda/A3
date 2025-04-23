using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("开始重新加载场景");

        // 重置所有可能影响角色移动的状态
        Time.timeScale = 1;
        Input.ResetInputAxes();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 添加场景加载完成后的回调
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("场景加载完成");

        // 查找玩家对象并确保其控制脚本启用
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }

            Debug.Log("已启用玩家所有脚本");
        }

        // 移除回调避免重复添加
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}