using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

//入力を読み取るところ。
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool       IsBlocking { get; private set; }
    public bool      IsAttacking { get; private set; }
    public Vector2 MovementValue { get; private set; }

    
    public event Action   JumpEvent;
    public event Action  DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;

    private    Controls    controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }


    //ジャンプについて
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    //回避について
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    //移動について
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    //カメラの視点操作
    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    //ロックオンについて
    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    //ロックオン解除について
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CancelEvent?.Invoke();

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsAttacking = true;
        }
        else if(context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }
}
