using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sfx, interactablesObjs, music;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sfx)
        {
            //s.source = s.source;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.spatialBlend = s._3D;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }
}
