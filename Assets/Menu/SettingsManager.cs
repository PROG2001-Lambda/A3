using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("音频设置")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    [Header("分辨率与画面")]
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private AudioSource bgmAudioSource;
    private float lastMusicVolume = 1f;

    private Resolution[] resolutions;

    void Start()
    {
        // 1. 初始化 AudioSource
        bgmAudioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        // 2. 读取存储设置
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        // 3. 应用音量
        ApplyAudioSettings();

        // 4. 分辨率选项
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;

        // 5. UI 事件绑定
        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });
        muteToggle.onValueChanged.AddListener(delegate { OnMuteToggle(); });
    }

    public void SaveSettings()
    {
        // 音量保存
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("IsMuted", muteToggle.isOn ? 1 : 0);

        // 分辨率
        Resolution selectedRes = resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedRes.width, selectedRes.height, fullscreenToggle.isOn);

        PlayerPrefs.Save();
    }

    public void CancelSettings()
    {
        gameObject.SetActive(false);
    }

    private void OnMusicVolumeChanged()
    {
        if (bgmAudioSource != null && !muteToggle.isOn)
        {
            bgmAudioSource.volume = musicSlider.value;
        }
    }

    private void OnSFXVolumeChanged()
    {
        // 建议将音效音量作为全局控制量保存下来，供SFX播放时读取
        AudioListener.volume = muteToggle.isOn ? 0 : sfxSlider.value;
    }

    private void OnMuteToggle()
    {
        if (muteToggle.isOn)
        {
            lastMusicVolume = musicSlider.value;
            if (bgmAudioSource != null)
                bgmAudioSource.volume = 0;
            AudioListener.volume = 0;
        }
        else
        {
            if (bgmAudioSource != null)
                bgmAudioSource.volume = lastMusicVolume;
            AudioListener.volume = sfxSlider.value;
        }
    }

    private void ApplyAudioSettings()
    {
        if (muteToggle.isOn)
        {
            if (bgmAudioSource != null)
                bgmAudioSource.volume = 0;
            AudioListener.volume = 0;
        }
        else
        {
            if (bgmAudioSource != null)
                bgmAudioSource.volume = musicSlider.value;
            AudioListener.volume = sfxSlider.value;
        }
    }
}
