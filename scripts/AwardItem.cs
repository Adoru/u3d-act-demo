using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//定义一个可选标签，可以在检查器中下拉框选择
public enum AwardType
{
    PraxisMegalance
}

public class AwardItem : MonoBehaviour
{
    public AwardType type;
    public float getSpeed = 10;
    public AudioClip PickedAudio;
    private bool startMove = false;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)); //随机运动 
    }

    //碰撞检测
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == Tags.ground)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            //当物品掉落后设置为触发器，碰撞到地面就不动
            SphereCollider col = GetComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 2f;
        }
    }

    //检测是否被player拾取
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.player)
        {
            startMove = true;
            player = other.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startMove)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + Vector3.up, getSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, player.position + Vector3.up)<0.3f)
            {
                player.GetComponent<playerGetAward>().getAward(type);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(PickedAudio, transform.position);
            }

        }
    }
}
