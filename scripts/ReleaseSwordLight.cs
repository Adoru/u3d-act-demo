using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSwordLight : MonoBehaviour
{
    public Transform swordLightPosition;
    public GameObject swordLightPrefab;
    public float attack = 100;  //剑光伤害

    public void SwordLightAtk()
    {
        GameObject go = Instantiate(swordLightPrefab, swordLightPosition.position + Vector3.up, transform.root.rotation) as GameObject;
        go.GetComponent<swordLight>().demage = attack;
    }
    
}
