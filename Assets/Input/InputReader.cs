using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Action OnJumpPerformed;
    public Action OnInteractionPerformed;
    public Vector2 Look;
    public Vector2 Move;
    private Controls controls;

    private void OnEnable()
    {
        if (controls != null)
            return;
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDisable() {
        controls.Player.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        OnInteractionPerformed?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        OnJumpPerformed?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

}