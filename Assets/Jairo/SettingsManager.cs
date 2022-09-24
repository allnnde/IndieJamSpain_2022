using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SoundSlider;

    private float _masterVolume;
    private float _musicVolume;
    private float _soundVolume;

    private bool _isInitialized = false;

    private void Awake()
    {
        _masterVolume = PlayerPrefs.GetFloat(AudioManager.KEY_MASTER_VOLUME, 1f);
        _musicVolume = PlayerPrefs.GetFloat(AudioManager.KEY_MUSIC_VOLUME, 1f);
        _soundVolume = PlayerPrefs.GetFloat(AudioManager.KEY_SOUND_VOLUME, 1f);

        InitializeSliders();
    }

    private void InitializeSliders()
    {
        MasterSlider.value = _masterVolume;
        MusicSlider.value = _musicVolume;
        SoundSlider.value = _soundVolume;

        MasterSlider.minValue = 0.0001f;
        MusicSlider.minValue = 0.0001f;
        SoundSlider.minValue = 0.0001f;

        _isInitialized = true;
    }


    // Called from OnValueChanged
    public void ChangeMasterVolume()
    {
        if (_isInitialized)
        {
            AudioManager.Instance.ChangeMasterVolume(MasterSlider.value);
            PlayerPrefs.SetFloat(AudioManager.KEY_MASTER_VOLUME, MasterSlider.value);
        }
    }

    public void ChangeMusicVolume()
    {
        if (_isInitialized)
        {
            PlayerPrefs.SetFloat(AudioManager.KEY_MUSIC_VOLUME, MusicSlider.value);
            AudioManager.Instance.ChangeMusicVolume(MusicSlider.value);
        }
    }

    public void ChangeSoundVolume()
    {
        if (_isInitialized)
        {
            PlayerPrefs.SetFloat(AudioManager.KEY_SOUND_VOLUME, SoundSlider.value);
            AudioManager.Instance.ChangeSoundVolume(SoundSlider.value);
        }
    }
}
