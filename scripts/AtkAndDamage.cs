using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//公有父类，伤害机制，应用于各个GameObject
public class AtkAndDemage : MonoBehaviour
{
    public float HP = 1000;
    public float normalAttack = 50;
    public float attackDistance = 1;
    public GameObject awardItem = null;
    protected Animator animator;
    public AudioClip PlayerTakeDamageAudio;
    public AudioClip MonsterTakeDamageAudio;

    protected void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    //虚函数，万一继承后角色需要复写
    public virtual void TakeDamage(float damage)
    {
        //player闪避状态下不会受到伤害
        if(tag == Tags.player)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dodge_Front"))
            {
                return;
            }
        }

        GetComponent<Animator>().ResetTrigger("Attack1");
        GetComponent<Animator>().ResetTrigger("Attack2");
        GetComponent<Animator>().ResetTrigger("Attack3");
        GetComponent<Animator>().ResetTrigger("Attack4");
        GetComponent<Animator>().ResetTrigger("Skill");
        GetComponent<Animator>().ResetTrigger("StrongAtk");

        //hp > 0,收到伤害减少血量
        if (HP > 0)
        {
            HP -= damage;
            if (tag == Tags.player)
            {
                AudioSource.PlayClipAtPoint(PlayerTakeDamageAudio, transform.position);
            }
            AudioSource.PlayClipAtPoint(MonsterTakeDamageAudio, transform.position);
        }

        //根据不同的血量播放不同的动画
        if (HP > 0)
        {
            if (tag == Tags.soulBoss)
            {
                float damageFlag = Random.Range(1, 10);
                if (damageFlag < 2)
                {
                    animator.SetTrigger("Damage");
                }
            }
            else
            {
                animator.SetTrigger("Damage");
            }
        }
        else
        {
            animator.SetTrigger("Death");
            if (this.tag == Tags.soulBoss || tag == Tags.soulMonster)
            {
                if (tag == Tags.soulBoss)
                {
                    spawnAward();   //掉落物品
                }
                SpawnManager._instance.enemyList.Remove(this.gameObject);
                Destroy(this.gameObject, 1);
                this.GetComponent<CharacterController>().enabled = false;       //销毁物体后character controller可能还在，会影响player行走
            }
        }
    }

    //生成物品
    void spawnAward()
    {
        Instantiate(awardItem, transform.position + Vector3.up, Quaternion.identity);
    }
}
