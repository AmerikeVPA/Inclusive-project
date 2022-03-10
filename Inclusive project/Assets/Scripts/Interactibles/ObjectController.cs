using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class ObjectController : MonoBehaviour
{
    [Header("Narration")]
    [Tooltip("Audio dialogue played when object is recollected")]
    public AudioClip recollectionDialogue;
}
