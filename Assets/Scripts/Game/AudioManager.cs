using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//THIS IS FOR SOUND THAT CAN BE PLAYED GLOBALLY
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [HideInInspector] public AudioSettings audioSettings;
    public SoundEvent onSoundEvent;
    AudioSource backgroundAudioSource;
    AudioSource generalAudioSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        audioSettings = GameManager.Instance.gameSettings.audioSettings;
        onSoundEvent.AddListener(PlaySoundEffect);
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
}

[System.Serializable]
public class SoundEvent : UnityEvent<SoundType> { }