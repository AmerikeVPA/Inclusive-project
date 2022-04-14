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
        //private Color ogColour;
        private float distanceToRender = 0f, materialAlpha = 0f;

        //public Material _affectedMat;
        private MeshRenderer _affectedMesh;
        public bool onRange;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _sphereColl = GetComponent<SphereCollider>();

            _sphereColl.isTrigger = true;
            _sphereColl.radius = _source.maxDistance;

            _affectedMesh = GetComponentInChildren<MeshRenderer>();
            //_affectedMat = GetComponentInChildren<MeshRenderer>().material;
            //ogColour = new Color(_affectedMat.color.r, _affectedMat.color.g, _affectedMat.color.b, 0);
            //_affectedMat.color = ogColour;
            foreach (Material mat in _affectedMesh.materials)
            {
                Color ogMatColour = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
                mat.color = ogMatColour;
            }
        }
        private void Start()
        {
            //_affectedMat.SetColor("_setClear", ogColour);
            StartCoroutine(SetTransparecny(null));
        }
        public void CallTransparencyChange(Transform player)
        {
            StartCoroutine(SetTransparecny(player));
        }
        IEnumerator SetTransparecny(Transform player)
        {
            yield return new WaitForEndOfFrame();
            if (onRange)
            {
                distanceToRender = Vector3.Distance(transform.position, player.transform.position);
                materialAlpha = (distanceToRender - _sphereColl.radius) / (0 - _sphereColl.radius) * (1 - 0) + 0;
                print(materialAlpha);
                foreach(Material mat in _affectedMesh.materials)
                {
                    Color newColour = new Color(mat.color.r, mat.color.g, mat.color.b, materialAlpha);
                    mat.color = newColour;
                }
                StartCoroutine(SetTransparecny(player));
            }
            else {
                foreach (Material mat in _affectedMesh.materials)
                {
                    Color ogColour = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
                    mat.color = ogColour;
                }
            }
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