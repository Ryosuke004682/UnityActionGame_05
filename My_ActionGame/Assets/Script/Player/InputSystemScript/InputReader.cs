using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

//���͂�ǂݎ��Ƃ���B
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


    //�W�����v�ɂ���
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    //����ɂ���
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    //�ړ��ɂ���
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    //�J�����̎��_����
    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    //���b�N�I���ɂ���
    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    //���b�N�I�������ɂ���
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
