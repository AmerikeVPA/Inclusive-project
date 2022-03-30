using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAstdio.InclusiveProject
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SoundRendererController : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private AudioSource _source;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _source = GetComponent<AudioSource>();
        }
    }
}
