using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordLight : MonoBehaviour
{
    public float speed = 10;
    public float demage = 500;
    public GameObject swordLightFX;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(swordLightFX, transform.position, transform.rotation);    //特效
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //因为是局部坐标，使用forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //swordLightFX.GetComponent<Transform>().transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.soulBoss||other.tag == Tags.soulMonster)
        {
            other.GetComponent<AtkAndDemage>().TakeDamage(demage);
        }
    }
}
