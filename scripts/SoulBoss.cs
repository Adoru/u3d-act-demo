using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBoss : MonoBehaviour
{
    private Transform player;
    private CharacterController cc;
    private Animator animator;
    private PlayerAtkAndDamage playerAtkAndDamage;
    
    public float attackDistance = 1.5f; //攻击距离
    public float speed = 3;
    public float attackTime = 3;        //攻击频率
    private float timer = 0;            //计时器

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        cc = this.GetComponent<CharacterController>();
        playerAtkAndDamage = player.GetComponent<PlayerAtkAndDamage>();
        animator = this.GetComponent<Animator>();
        timer = attackTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAtkAndDamage.HP <= 0)
        {
            animator.SetBool("Walk", false);
            return;
        }

        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);

        float distance = Vector3.Distance(targetPos, transform.position);
        if(distance <= attackDistance)  
        {   //在攻击距离之内
            timer += Time.deltaTime;
            if(timer>attackTime)
            {   //达到攻击时间,随即执行攻击指令
                int num = Random.Range(0, 20);
                if(num<=10)
                {
                    animator.SetTrigger("Skill1");
                }else if(num <= 15)
                {
                    animator.SetTrigger("Skill2");
                }
                else
                {
                    animator.SetTrigger("Skill3");
                }
                timer = 0;
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else   
        {   //跑向目标
            timer = attackTime;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                cc.SimpleMove(transform.forward*speed);
            }
            animator.SetBool("Walk", true);
        }
    }
}
