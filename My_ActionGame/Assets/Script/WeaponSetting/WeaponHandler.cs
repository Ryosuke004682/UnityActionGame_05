using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField,Header("����̃R���C�_�[�̐���")]             private GameObject[] weaponLogic;
    [SerializeField,Header("������A�N�e�B�u�ɂ��邩�A���Ȃ���")] private GameObject[] weaponState;


    private void Awake()
    {
        weaponState[0].SetActive(true);
        weaponState[1].SetActive(false);
    }


    /*����̓����蔻��*/
    public void EnableWeapon()
    { weaponLogic[1].SetActive(true); }

    public void DisableWeapon()
    { weaponLogic[1].SetActive(false); }



    //TODO : �R���[�`�����쐬���āA�w�肵���b��ExitWeapon()���Ă΂��悤�ɂ���B
    //NEXT : ������ۂɁA�f�B�]���u�ŏ�����悤�ɂ���B
    //NEXT : �o��������ۂ��f�B�]���u�ŕ\���悤�ɂ���B


    /*������f�B�]���u�ŕ\���E��\����������*/
    public void EnterWeapon()
    {
        /*����̏�Ԑݒ�*/
        weaponState[0].SetActive(false);
        weaponState[1].SetActive(true);

        //���łɐ�������Ă�����ĂԂ������ʂȂ̂ŁA�Ă΂Ȃ��悤�ɂ���
        if (weaponState[0].active == false && weaponState[1].active == true) { return; }
    }


    /*�R���[�`���ŏ����鎞�Ԃ��Ǘ�������
      �܂��AList���ɓG���c���Ă��Ȃ������ꍇ�A�ɏ�L�̃R���[�`������������悤�Ɏd���������B*/
    public void ExitWeapon()
    {
        /*����̏�Ԑݒ�*/
        weaponState[0].SetActive(true);
        weaponState[1].SetActive(false);
    }


    /*�L�b�N�̓����蔻��*/
    public void EnableFootHitPoint()
    { weaponLogic[2].SetActive(true); }

    public void DisableFootHitPoint()
    { weaponLogic[2].SetActive(false); }


    

}
