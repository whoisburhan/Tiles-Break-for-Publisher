using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCanvasScript : MonoBehaviour
{
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        soundVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.SOUND_VOLUME_KEY, 0.5f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_VOLUME_KEY, 0.5f);

        this.gameObject.SetActive(false);
    }
}
