using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VesselAudioSystem : MonoBehaviour
{
    [HideInInspector] public AudioSettings audioSettings;
    AudioSource vesselAudioSource;
    AudioSource flightAudioSource;

    void Awake()
    {
        audioSettings = GameManager.Instance.gameSettings.audioSettings;
    }

    // Start is called before the first frame update
    void Start()
    {
        vesselAudioSource = this.gameObject.AddComponent<AudioSource>();
        flightAudioSource = this.gameObject.AddComponent<AudioSource>();
    }

    public void SetVolume(float value)
    {
        vesselAudioSource.volume = vesselAudioSource.volume = value;
    }

    public void FlightAudio() {

    }

    public void PlaySoundEffect(SoundType soundType)
    {
        Sound soundSelection = audioSettings.soundEffects.Where(x => x.type == soundType).First();
        AudioClip sound = soundSelection.clip;
        vesselAudioSource.PlayOneShot(sound, soundSelection.volume);
    }
}

