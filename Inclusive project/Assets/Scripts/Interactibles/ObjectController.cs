using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AAAstdio.InclusiveProject
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class ObjectController : MonoBehaviour
    {
        [Header("Narration")]
        [Tooltip("Audio dialogue played when object is recollected")]
        public AudioClip recollectionDialogue;
        [Header("Process variables")]
        [Tooltip("object that will disappear on player interaction")]
        public List<GameObject> objsToDestroy;
    
        public void DestroyObjs()
        {
            foreach(GameObject obj in objsToDestroy) { Destroy(obj); }
        }
    }
}
