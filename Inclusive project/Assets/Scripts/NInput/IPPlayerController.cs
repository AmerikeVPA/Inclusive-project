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

    private const float treshold = 0.1f;

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
        Interact();
        Walk(); 
    }
    private void LateUpdate() { Rotation(); }
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
    private void Rotation()
    {
        if(ipInputs.rotation.sqrMagnitude >= treshold)
        {
            _rotateSpeed = ipInputs.rotation.x * rotationSpeed * Time.deltaTime;
            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotateSpeed);
        }
    }
    private void Walk()
    {
        // normalise input direction
        Vector3 inputDirection = new Vector3(ipInputs.movement.x, 0.0f, ipInputs.movement.y).normalized;
        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (ipInputs.movement != Vector2.zero)
        {
            // move
            inputDirection = transform.right * ipInputs.movement.x + transform.forward * ipInputs.movement.y;
        }

        // move the player
        playerController.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime);
    }
}
