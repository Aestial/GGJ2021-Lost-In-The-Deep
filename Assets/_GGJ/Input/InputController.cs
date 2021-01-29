using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent : UnityEvent<float,float> {}
[Serializable]
public class FireInputEvent : UnityEvent {}

public class InputController : MonoBehaviour
{
    [SerializeField]
    private MoveInputEvent OnMovePerformed = default;
    [SerializeField]
    private FireInputEvent OnFirePerfomed = default;
    [SerializeField]
    private FireInputEvent OnFireCanceled = default;

    Controls controls;
    
    private void Awake() 
    {
        controls = new Controls();    
    }

    private void OnEnable() 
    {
        controls.Player.Enable();
        
        controls.Player.Move.performed += OnMovePerformedHandler;
        controls.Player.Move.canceled += OnMovePerformedHandler;

        controls.Player.Fire.performed += OnFirePerformedHandler;
        controls.Player.Fire.canceled += OnFireCanceledHandler;
    }

    private void OnFirePerformedHandler (InputAction.CallbackContext context)
    {
        OnFirePerfomed.Invoke();
        Debug.Log($"Fire Input performed");
    }

    private void OnFireCanceledHandler (InputAction.CallbackContext context)
    {
        OnFireCanceled.Invoke();
        Debug.Log($"Fire Input canceled");
    }

    private void OnMovePerformedHandler (InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        OnMovePerformed.Invoke(moveInput.x, moveInput.y);
        // Debug.Log($"Move Input: {moveInput}");
    }
}
