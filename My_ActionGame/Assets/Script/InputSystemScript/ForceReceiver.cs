using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*重力の設定*/
public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;

    private float verticalVelocity;

    public Vector3 movement => impact +  Vector3.up * verticalVelocity;

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

        impact = Vector3.SmoothDamp(impact , Vector3.zero , ref dampingVelocity , drag);

    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

}
