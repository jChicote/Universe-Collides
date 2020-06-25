using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    public List<Sound> soundEffects;
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

public enum SoundType {
    LaserCannon
}