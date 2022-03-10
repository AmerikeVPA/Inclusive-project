using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class IPPlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    [Tooltip("Movement speed")]
    public float walkingSpeed = 3.5f;
    [Tooltip("Look speed")]
    public float rotationSpeed = 1.0f;
    [Tooltip("Time before remembering again")]
    public float recallTimeout = 2.5f;
    [Tooltip("Check if the player has an interactable in range")]
    public bool interInRange = false;


    // Private player
    private float _speed, _rotateSpeed;
    // Private timeout deltatime
    private float _recallTO;

    private CharacterController playerController;
    private IPPlayerInputs ipInputs;

    // Character Narrator Source and Elements
    private AudioSource narratorVoice;
    private GameObject _intObjInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.GetComponent<AudioSource>().Play();
            interInRange = true;
            _intObjInRange = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        _intObjInRange = null;
        interInRange = false;
    }

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        ipInputs = GetComponent<IPPlayerInputs>();

        _recallTO = recallTimeout;
    }
    private void Update()
    {
        
    }
    private void Interact()
    {
        if (interInRange && _intObjInRange!=null)
        {
            if (ipInputs.interact)
            {
                narratorVoice.PlayOneShot(_intObjInRange.GetComponent<ObjectController>().recollectionDialogue);
                Destroy(_intObjInRange);
            }
        }
    }
}
