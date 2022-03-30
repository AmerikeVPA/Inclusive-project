using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioMixerGroup mixerGroup;

    public bool loop;

    public string fileName;
    
    public AudioSource source;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float _3D;
    //public float pitch;
} 