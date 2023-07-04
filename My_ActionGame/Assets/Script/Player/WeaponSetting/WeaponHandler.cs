using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField,Header("武器のコライダーの制御")]             private GameObject[] weaponLogic;
    [SerializeField,Header("武器をアクティブにするか、しないか")] private GameObject[] weaponState;


    private void Awake()
    {
        weaponState[0].SetActive(true);
        weaponState[1].SetActive(false);
    }


    /*武器の当たり判定*/
    public void EnableWeapon()
    { weaponLogic[1].SetActive(true); }

    public void DisableWeapon()
    { weaponLogic[1].SetActive(false); }



    //TODO : コルーチンを作成して、指定した秒数ExitWeapon()が呼ばれるようにする。
    //NEXT : 消える際に、ディゾルブで消えるようにする。
    //NEXT : 出現させる際もディゾルブで表すようにする。


    /*武器をディゾルブで表示・非表示させたい*/
    public void EnterWeapon()
    {
        /*武器の状態設定*/
        weaponState[0].SetActive(false);
        weaponState[1].SetActive(true);

        //すでに生成されていたら呼ぶだけ無駄なので、呼ばないようにする
        if (weaponState[0].active == false && weaponState[1].active == true) { return; }
    }


    /*コルーチンで消える時間を管理したい
      また、List内に敵が残っていなかった場合、に上記のコルーチンが発動するように仕向けたい。*/
    public void ExitWeapon()
    {
        /*武器の状態設定*/
        weaponState[0].SetActive(true);
        weaponState[1].SetActive(false);
    }


    /*キックの当たり判定*/
    public void EnableFootHitPoint()
    { weaponLogic[2].SetActive(true); }

    public void DisableFootHitPoint()
    { weaponLogic[2].SetActive(false); }


    

}
