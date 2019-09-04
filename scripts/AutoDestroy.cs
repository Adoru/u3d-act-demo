using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * 
 * 自动销毁特效
 * * * * * * * * * * * * * * * * */ 
public class AutoDestroy : MonoBehaviour
{
    public float existTime = 1;

    private void Start()
    {
        Destroy(this.gameObject, existTime);
    }
}
