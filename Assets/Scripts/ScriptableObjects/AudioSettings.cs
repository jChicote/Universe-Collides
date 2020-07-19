using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    public List<Sound> soundEffects;
    public List<BackgroundTrack> backgroundTracks;
    public List<EngineSound> engineSounds;
}

[System.Serializable]
public class Sound {
    public SoundType type;
    public AudioClip[] soundClips;

    [Range(0,1)]
    public float volume;

    [HideInInspector]
    public AudioClip clip {
        get {
            int size = soundClips.Length;
            if (size == 1) return soundClips[0];

            int randomIndex = Random.Range(0, size);
            return soundClips[randomIndex];
        }
    }
}

[System.Serializable]
public class EngineSound {
    public VesselType type;
    public AudioClip clip;

    [Range(0,1)]
    public float defaultVolume;

    [Range(0,1.5f)]
    public float defaultPitch;
}

[System.Serializable]
public class BackgroundTrack {
    public BackgroundOstType type;
    public AudioClip[] soundClips;

    [Range(0,1)]
    public float volume;

    [HideInInspector]
    public AudioClip clip {
        get {
            int size = soundClips.Length;
            if (size == 1) return soundClips[0];

            int randomIndex = Random.Range(0, size);
            return soundClips[randomIndex];
        }
    }
}

public enum SoundType {
    LaserCannon_1,
    LaserCannon_2,
    BoltImpact,
    EngineThrust
}

public enum BackgroundOstType {
    StarWars,
    StarTrek
}