using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Used to play sounds not specific to a character
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    public const string KEY_MASTER_VOLUME = "MasterVolume";
    public const string KEY_MUSIC_VOLUME = "MusicVolume";
    public const string KEY_SOUND_VOLUME = "SoundVolume";

    [Header("Volume control")]
    public AudioMixer MainAudioMixer;
    public AudioMixerSnapshot DefaultSnapshot;
    public AudioMixerSnapshot NoMusicSnapshot;

    [Header("Music")]
    public AudioSource MusicAS;
    public AudioClip MenuMusic;
    public AudioClip GameMusicLayer1;
    public AudioClip GameMusicLayer2;
    public AudioClip GameMusicLayer3;
    public AudioClip GameMusicLayer4;
    public AudioClip GameMusicLayer5;

    [Header("Global sounds")]
    public AudioSource GlobalSoundsAS;
    public AudioClip Shooting;
    public AudioClip SwordAttack;
    public AudioClip WaveCompleteSound;
    public AudioClip WaveComingSound;
    public AudioClip EndGame;
    public AudioClip ButtonHoverSound;
    

    [Header("Object sounds")]
    public AudioSource ObjectSoundsAS;
    public AudioClip ChangeWeapon;
    public AudioClip ButtonClickSound;
    public AudioClip PotionSound;


    private bool _MenuMusicPlaying = false;
    
   

    private void Start()
    {
        ChangeMasterVolume(PlayerPrefs.GetFloat(KEY_MASTER_VOLUME, 1f));
        ChangeMusicVolume(PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 1f));
        ChangeSoundVolume(PlayerPrefs.GetFloat(KEY_SOUND_VOLUME, 1f));
    }


    public void PlaySound(AudioSource audioSource, AudioClip sound, float minPitch, float maxPitch)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(sound);
    }

    // Global sounds
    public void PlayShootSound()
    {
        PlaySound(GlobalSoundsAS, Shooting, 0.8f, 1.2f);
    }

    public void PlaySwordAttackSound()
    {
        PlaySound(GlobalSoundsAS, SwordAttack, 0.95f, 1.05f);
    }

    public void PlayButtonHoverSound()
    {
        PlaySound(GlobalSoundsAS, ButtonHoverSound, 0.98f, 1.02f);
    }

    public void PlayButtonClickSound()
    {
        PlaySound(GlobalSoundsAS, ButtonClickSound, 0.98f, 1.02f);
    }

    public void PlayWaveCompleteSound()
    {
        PlaySound(GlobalSoundsAS, WaveCompleteSound, 0.99f, 1.01f);
    }

    public void PlayWaveComingSound()
    {
        PlaySound(GlobalSoundsAS, WaveComingSound, 0.99f, 1.01f);
    }

    public void PlayEndGameSound()
    {
        PlaySound(GlobalSoundsAS, EndGame, 0.99f, 1.01f);
    }

    

    // Objects sounds
    public void PlayPotionSound()
    {
        PlaySound(ObjectSoundsAS, PotionSound, 0.98f, 1.02f);
    }

    
    // Music
    public void StopMusic()
    {
        NoMusicSnapshot.TransitionTo(0.5f);
    }

    public void StartMusic()
    {
        DefaultSnapshot.TransitionTo(0f);
        MusicAS.Play();
    }

    public void StopMenuMusic()
    {
        MusicAS.Stop();
    }

    public void PlayMenuMusic()
    {

        
        if (!_MenuMusicPlaying)
        {
            _MenuMusicPlaying = true;
            MusicAS.clip = MenuMusic;
            StartMusic();
        }
        else
        {
            DefaultSnapshot.TransitionTo(0.25f);
        }
    }


    public void ChangeMasterVolume(float value)
    {
        MainAudioMixer.SetFloat(KEY_MASTER_VOLUME, Mathf.Log10(value) * 20);
    }

    public void ChangeMusicVolume(float value)
    {
        MainAudioMixer.SetFloat(KEY_MUSIC_VOLUME, Mathf.Log10(value) * 20);
    }

    public void ChangeSoundVolume(float value)
    {
        MainAudioMixer.SetFloat(KEY_SOUND_VOLUME, Mathf.Log10(value) * 20);
    }
}
