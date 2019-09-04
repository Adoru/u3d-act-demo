using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerTransform;

    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = playerTransform.position + new Vector3(-0.02f, 1.93f, -3.82f);
        //Time.deltaTime：时间增量，等于帧率的倒数，表示单位时间内所有帧组成的动画对于每一帧的切割量。每帧都在变化
        transform.position = Vector3.Lerp(transform.position, targetPos, speed*Time.deltaTime); //两个向量的线性插值，第三个值0~1，实现镜头跟随

        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);//实现镜头转向
    }
}
