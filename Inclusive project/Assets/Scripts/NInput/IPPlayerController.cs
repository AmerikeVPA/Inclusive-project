using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AAAstdio.InclusiveProject
{
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
        private float _speed, _rotateSpeed, _cameraPitch;
        // Private timeout deltatime
        private float _recallTO;

        private const float treshold = 0.1f;

        private CharacterController playerController;
        private IPPlayerInputs ipInputs;
        private Camera mainCam;

        // Character Narrator Source and Elements
        private AudioSource narratorVoice;
        private ObjectController _intObjInRange;
        private NarratorManager narrator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Interactable")
            {
                if (!other.GetComponent<AudioSource>().isPlaying) { other.GetComponent<AudioSource>().Play(); } 
                interInRange = true;
                _intObjInRange = other.GetComponent<ObjectController>();
            }
            if (other.tag == "AudSrc")
            {
                other.GetComponent<SoundRendererController>().onRange = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Interactable")
            {
                _intObjInRange = null;
                interInRange = false;
            }
            if (other.tag == "AudSrc")
            {
                other.GetComponent<SoundRendererController>().onRange = false;
            }
        }
        private void Awake()
        {
            mainCam = GetComponentInChildren<Camera>();
            narrator = FindObjectOfType<NarratorManager>();
        }
        private void Start()
        {
            playerController = GetComponent<CharacterController>();
            narratorVoice = GetComponentInChildren<AudioSource>();
            ipInputs = GetComponent<IPPlayerInputs>();

            _recallTO = recallTimeout;
        }
        private void Update() { Interact(); Remember(); Walk(); }
        private void LateUpdate() { Rotation(); }
        private void Interact()
        {
            if (interInRange && _intObjInRange != null)
            {
                if (ipInputs.interact)
                {
                    narratorVoice.PlayOneShot(_intObjInRange.recollectionDialogue);
                    _intObjInRange.DestroyObjs();
                }
            }
        }
        private void Remember()
        {
            if (ipInputs.remember)
            {
                print("Enter remember function");
                if (!narratorVoice.isPlaying && _recallTO <= 0)
                {
                    Debug.Log("Should play objective clue");
                    narratorVoice.PlayOneShot(narrator.ObjectiveClues[0]);
                }
            }
        }
        private void Rotation()
        {
            if (ipInputs.rotation.sqrMagnitude >= treshold)
            {
                _cameraPitch += ipInputs.rotation.y * rotationSpeed * Time.deltaTime;
                _rotateSpeed = ipInputs.rotation.x * rotationSpeed * Time.deltaTime;

                _cameraPitch = ClampCam(_cameraPitch, -90f, 90f);

                // rotate the camera up and down
                mainCam.transform.localRotation = Quaternion.Euler(_cameraPitch, 0.0f, 0.0f);
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
            playerController.Move(inputDirection.normalized * Time.deltaTime);
        }
        private static float ClampCam(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
