using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{  
    public void Start()
    {
        if (PlayerPrefs.GetFloat("MouseSensitivity") != 0) sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        else
        {
            // The above is used to determine if it is the player's first play. These are enabled on the first play:
            PlayerPrefs.SetInt("OcclusionCullingEnabled", 1);
        }
        if (PlayerPrefs.GetInt("FPSCountEnabled") == 1) FPSCountToggle.isOn = true;
        else FPSCountToggle.isOn = false;
        if (PlayerPrefs.GetInt("OcclusionCullingEnabled") == 1) OcclusionCullingToggle.isOn = true;
        else OcclusionCullingToggle.isOn = false;
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }
    public void Update()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
        ytpsText.text = PlayerPrefs.GetInt("YTPs").ToString() + " Total YTPs";
        if (FPSCountToggle.isOn) PlayerPrefs.SetInt("FPSCountEnabled", 1);
        else PlayerPrefs.SetInt("FPSCountEnabled", 0);
        if (OcclusionCullingToggle.isOn) PlayerPrefs.SetInt("OcclusionCullingEnabled", 1);
        else PlayerPrefs.SetInt("OcclusionCullingEnabled", 0);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        musicMixer.SetFloat("GlobalMusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        PlayerPrefs.SetFloat("SoundVolume", soundVolumeSlider.value);
        soundMixer.SetFloat("GlobalSFXVolume", PlayerPrefs.GetFloat("SoundVolume"));
    }
    public Slider sensitivitySlider;
    public TMP_Text ytpsText;
    public Toggle FPSCountToggle;
    public Toggle OcclusionCullingToggle;
    public AudioMixer musicMixer;
    public Slider musicVolumeSlider;
    public AudioMixer soundMixer;
    public Slider soundVolumeSlider;
}
