using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class ShiftMaterial_TEST : MonoBehaviour
{
    /*
     Material配列の一番目は、納刀してる方
     Material配列の二番目は、抜刀してる方
     */

    [SerializeField] private GameObject[] weapons;

    private void Start()
    {
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);

            //ActiveWeapon();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);

            //UnActiveWeapon();
        }
    }

    private void ActiveWeapon()
    {
        Renderer rend     = weapons[1].GetComponent<Renderer>();
        Material material = rend.material;
        material.SetFloat("_Dissolve", 0.0f);
        rend.material     = material;
    }

    private void UnActiveWeapon()
    {
        Renderer rend     = weapons[0].GetComponent<Renderer>();
        Material material = rend.material;
        material.SetFloat("_Dissolve", 1.0f);
        rend.material     = material;
    }



}
