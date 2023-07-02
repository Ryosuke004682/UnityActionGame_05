using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*重力の設定*/
public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private float verticalVelocity;

    public Vector3 movement => Vector3.up * verticalVelocity;

    private void Update()
    {
        //地面を検知させる
        if(verticalVelocity  < 0.0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

}
