using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IPausableAudio
{
    void PauseAllAudio();
    void UnpauseAllAudio();
}

public class VesselAudioSystem : MonoBehaviour, IPausableAudio
{
    [HideInInspector] public AudioSettings audioSettings;

    [Header("Attached Audio Sources")]
    public AudioSource vesselAudioSource;
    public AudioSource flightAudioSource;
    public AudioSource thrustAudioSource;

    float defaultFlightPitch = 0;

    public void Init(EntityType entityType)
    {
        audioSettings = GameManager.Instance.gameSettings.audioSettings;

        if(entityType == EntityType.Player) {
            vesselAudioSource.spatialBlend = 0;
            flightAudioSource.spatialBlend = 0;
            thrustAudioSource.spatialBlend = 0;
        } else {
            vesselAudioSource.spatialBlend = 1;
            flightAudioSource.spatialBlend = 1;
            thrustAudioSource.spatialBlend = 1;

            vesselAudioSource.maxDistance = 70f;
            flightAudioSource.maxDistance = 200f;
            thrustAudioSource.maxDistance = 70f;

            vesselAudioSource.minDistance = 15f;
            flightAudioSource.minDistance = 45f;
            thrustAudioSource.minDistance = 15f;
        }
    }

    public void SetVolume(float value)
    {
        vesselAudioSource.volume = vesselAudioSource.volume = value;
    }

    public void PlayFlightAudio(VesselType type) {

        //Set engine sound
        EngineSound engineSound = audioSettings.engineSounds.Where(x => x.type == type).First();
        flightAudioSource.clip = engineSound.clip;
        flightAudioSource.volume = engineSound.defaultVolume;
        flightAudioSource.loop = true;
        flightAudioSource.Play();

        defaultFlightPitch = engineSound.defaultPitch;

        //Set thrust sound
        Sound thrustSound = audioSettings.soundEffects.Where(x => x.type == SoundType.EngineThrust).First();
        thrustAudioSource.clip = thrustSound.clip;
        thrustAudioSource.volume = 0;
        thrustAudioSource.loop = true;
    }

    public void CalculateThrustSound(float inputY) {
        if(inputY <= 0) {
            thrustAudioSource.volume = Mathf.Lerp(thrustAudioSource.volume, 0, 0.05f);
            if(thrustAudioSource.volume == 0) thrustAudioSource.Pause();
        } else if (inputY > 0) {
            if(!thrustAudioSource.isPlaying) thrustAudioSource.Play();
            thrustAudioSource.volume = Mathf.Lerp(thrustAudioSource.volume, 0.9f, 0.05f);
        }
    }

    public void CalculateEngineTempo(float inputY) {
        if(inputY == 0) {
            flightAudioSource.pitch = Mathf.Lerp(flightAudioSource.pitch, defaultFlightPitch, 0.05f);
            return;
        } else if (inputY > 0) {
            flightAudioSource.pitch = Mathf.Lerp(flightAudioSource.pitch, 1.1f, 0.05f);
        } else {
            flightAudioSource.pitch = Mathf.Lerp(flightAudioSource.pitch, 0.85f, 0.05f);
        }
    }

    public void PlaySoundEffect(SoundType soundType) {
        Sound soundSelection = audioSettings.soundEffects.Where(x => x.type == soundType).First();
        AudioClip sound = soundSelection.clip;
        vesselAudioSource.PlayOneShot(sound, soundSelection.volume);
    }

    public void PauseAllAudio()
    {
        vesselAudioSource.Pause();
        flightAudioSource.Pause();
        thrustAudioSource.Pause();
    }

    public void UnpauseAllAudio()
    {
        vesselAudioSource.Play();
        flightAudioSource.Play();
        thrustAudioSource.Play();
    }
}

