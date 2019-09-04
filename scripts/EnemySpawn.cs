using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 生成怪物
 */

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefeb;

    public GameObject Spawm()
    {
        return GameObject.Instantiate(prefeb, transform.position, transform.rotation) as GameObject;
    }
}
