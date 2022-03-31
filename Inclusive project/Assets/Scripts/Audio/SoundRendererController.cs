using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAstdio.InclusiveProject
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(SphereCollider))]
    public class SoundRendererController : MonoBehaviour
    {
        //private MeshRenderer _meshRenderer;
        private AudioSource _source;
        private SphereCollider _sphereColl;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _sphereColl = GetComponent<SphereCollider>();

            _sphereColl.isTrigger = true;
            _sphereColl.radius = _source.maxDistance;
        }
    }
}


/*
        private AudioDb _db;

        //private float loudness;
        //private float[] _clipData;

        private void Start() { StartCoroutine(CheckVolume()); }
        IEnumerator CheckVolume()
        {
            yield return new WaitForSeconds(0.1f);
            
            
            
            
            _source.clip.GetData(_clipData, _source.timeSamples);
            loudness = 0f;
            
            foreach(var sample in _clipData) { loudness += Mathf.Abs(sample); }

            loudness /= 1024;
            
            print(loudness);
            StartCoroutine(CheckVolume());
        }
 */