using UnityEngine;


/*�̂̕��ʂ�*/
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


    /*���ꗎ����悤�Ɏ��S�����邽�߂ɁA���O�h�[�����A�N�e�B�u�ɂ���*/
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

        Debug.Log($"���O�h�[�����A{isRagdoll}�ł�");

    }

}
