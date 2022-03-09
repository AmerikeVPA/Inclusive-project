using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IPPlayerInputs : MonoBehaviour
{
    [Header("Player Input Values")]
    public Vector2 movement, rotation;
    public bool interact, remember;

    [Header("Cursor Settings")]
    public bool lockedCursor, rotateWCursor;

    public void OnMovement(InputValue value) { MovementInput(value.Get<Vector2>()); }
    public void OnRotate(InputValue value) { if (rotateWCursor) { RotationInput(value.Get<Vector2>()); } }
    public void OnInteract(InputValue value) { InteractionInput(value.isPressed); }
    public void OnRemember(InputValue value) { RememberInput(value.isPressed); }
    public void MovementInput(Vector2 moveDirection) { movement = moveDirection; }
    public void RotationInput(Vector2 lookDirection) { rotation = lookDirection; }
    public void InteractionInput(bool interactionState) { interact = interactionState; }
    public void RememberInput(bool rememberState) { remember = rememberState; }

#if !UNITY_IOS || !UNITY_ANDROID
    private void OnApplicationFocus(bool hasFocus) { SetCursorState(lockedCursor); }
    private void SetCursorState(bool newState) 
    { Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None; }
#endif
}
