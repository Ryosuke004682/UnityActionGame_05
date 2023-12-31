using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*重力の設定*/
public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent             agent;



    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;

    private Vector3 dampingVelocity;
    private Vector3 impact;

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

        if(agent != null)
        {
            if (impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void Reset()
    {
        impact = Vector3.up;
        verticalVelocity = 0.0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        
        if(agent != null)
        {
            agent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

}
