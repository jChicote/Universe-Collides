using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This Audio Manager is designed for general use and should be not destroyed during gameplay.
/// </summary>
public class GeneralAudioManager : MonoBehaviour
{
    public static GeneralAudioManager Instance = null;

    [HideInInspector] public AudioSettings audioSettings;
    public SoundEvent onSoundEvent;
    public TrackEvent onTrackEvent;

    AudioSource backgroundAudioSource;
    AudioSource generalAudioSource;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this);
        }

        audioSettings = GameManager.Instance.gameSettings.audioSettings;
        onSoundEvent.AddListener(PlaySoundEffect);
        onTrackEvent.AddListener(PlayBackgroundTrack);
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        generalAudioSource = gameObject.AddComponent<AudioSource>();

    }

    public void SetVolume(float value)
    {
        generalAudioSource.volume = backgroundAudioSource.volume = value;
    }

    public void PlaySoundEffect(SoundType soundType)
    {
        Sound soundSelection = audioSettings.soundEffects.Where(x => x.type == soundType).First();
        AudioClip sound = soundSelection.clip;
        generalAudioSource.PlayOneShot(sound, soundSelection.volume);
    }

    public void PlayBackgroundTrack(BackgroundOstType track) {
        BackgroundTrack trackSelection = audioSettings.backgroundTracks.Where(x => x.type == track).First();
        AudioClip music = trackSelection.clip;
        backgroundAudioSource.clip = music;
        backgroundAudioSource.Play();
    }
}

[System.Serializable]
public class SoundEvent : UnityEvent<SoundType> { }

[System.Serializable]
public class TrackEvent : UnityEvent<BackgroundOstType> { }