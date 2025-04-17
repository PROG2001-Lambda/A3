using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource bgmSource;
    public AudioClip menu1BGM;
    public AudioClip menupage2BGM;
    public AudioClip[] levelBGMs; // 顺序：Game Scene, Clarissa Scene, LYue Scene, YP Scene

    private Dictionary<string, AudioClip> sceneBGMMap;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupSceneBGMMap();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupSceneBGMMap()
    {
        sceneBGMMap = new Dictionary<string, AudioClip>
        {
            { "Menu1", menu1BGM },
            { "Menupage2", menupage2BGM },
            { "Game Scene", levelBGMs[0] },
            { "Clarissa Scene", levelBGMs[1] },
            { "LYue Scene", levelBGMs[2] },
            { "YP Scene", levelBGMs[3] }
        };
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneBGMMap.ContainsKey(sceneName))
        {
            AudioClip clip = sceneBGMMap[sceneName];
            if (bgmSource.clip != clip)
            {
                bgmSource.clip = clip;
                bgmSource.Play();
            }
        }
    }

    public void SetVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void Mute(bool isMute)
    {
        bgmSource.mute = isMute;
    }
}
