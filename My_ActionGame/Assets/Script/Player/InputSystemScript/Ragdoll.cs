using UnityEngine;


/*体の部位に*/
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[]  allColliders;
    private Rigidbody[] allRigidbodies;

    private void Start()
    {
        allColliders   = GetComponentsInChildren<Collider> (true);
        allRigidbodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(true);
    }


    /*崩れ落ちるように死亡させるために、ラグドールをアクティブにする*/
    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach(Collider collider in allColliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }

        foreach(Rigidbody rigidbody in allRigidbodies)
        {
            if(rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity  =  isRagdoll;
            }
        }

        controller.enabled = !isRagdoll;
        animator  .enabled = !isRagdoll;

        Debug.Log($"ラグドールが、{isRagdoll}です");

    }

}
