using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承自AtkAndDamage
public class PlayerAtkAndDamage : AtkAndDemage
{
    public float sATKDamage = 300;
    public float skillDamage = 500;
    public GameObject HitLineFX;
    public GameObject HitLineFX2;
    public GameObject HitLineFX3;
    public GameObject FlameFloorFX;
    public Vector3 targetDir;
    public AudioClip lightAtkAudio1;
    public AudioClip lightAtkAudio2;
    public AudioClip lightAtkAudio3;
    public AudioClip skillAudio;
    public AudioClip strongAtkAudio;
    public AudioClip chopHitAudio;
    public AudioClip chopNoHitAudio;
    public AudioClip ExblosionAudio;
    public AudioClip SwordLightAudio;


    public void lightAttack1Event()
    {
        GameObject enemy = null;    //存储离player最近的moster
        float distance = attackDistance;

        //为了遍历monsterList，需要把SpawnManager设置为单例模式
        foreach(GameObject go in SpawnManager._instance.enemyList)
        {
            float temp = Vector3.Distance(go.transform.position, transform.position);   //player和每个怪物的距离
            if (temp < distance)
            {
                enemy = go;
                distance = temp;
            }
        }

        //播放角色音效
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk2"))
        {
            AudioSource.PlayClipAtPoint(lightAtkAudio2, transform.position);
            AudioSource.PlayClipAtPoint(chopNoHitAudio, transform.position);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk4"))
        {
            AudioSource.PlayClipAtPoint(lightAtkAudio3, transform.position);
            AudioSource.PlayClipAtPoint(chopNoHitAudio, transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(lightAtkAudio1, transform.position);
            AudioSource.PlayClipAtPoint(chopNoHitAudio, transform.position);
        }

        if (enemy == null)
        {//没有敌人在攻击范围内，不处理伤害

        }

        else
        {//面向距离最近的敌人
            Vector3 targetPos = enemy.transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);

            enemy.GetComponent<AtkAndDemage>().TakeDamage(normalAttack);

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk2"))
            {
                Instantiate(HitLineFX2, enemy.transform.position + Vector3.up, enemy.transform.rotation);    //特效。vector3.up = (0, 1, 0)
                AudioSource.PlayClipAtPoint(chopHitAudio, transform.position);
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk4"))
            {
                Instantiate(HitLineFX3, enemy.transform.position + Vector3.up, enemy.transform.rotation);    //特效。vector3.up = (0, 1, 0)
                AudioSource.PlayClipAtPoint(chopHitAudio, transform.position);
            }
            else
            {
                Instantiate(HitLineFX, enemy.transform.position + Vector3.up, enemy.transform.rotation);    //特效。vector3.up = (0, 1, 0)
                AudioSource.PlayClipAtPoint(chopHitAudio, transform.position);
            }
        }
    }

    public void strongAttackEvent()
    {
        GameObject enemy = null;    //存储离player最近的moster
        float distance = attackDistance;
        Instantiate(FlameFloorFX, transform.position + transform.forward * 3, transform.rotation);    //特效。vector3.up = (0, 1, 0)
        AudioSource.PlayClipAtPoint(strongAtkAudio, transform.position);
        AudioSource.PlayClipAtPoint(ExblosionAudio, transform.position);

        //为了遍历monsterList，需要把SpawnManager设置为单例模式
        foreach (GameObject go in SpawnManager._instance.enemyList)
        {
            float temp = Vector3.Distance(go.transform.position, transform.position);   //player和每个怪物的距离
            if (temp < distance)
            {
                enemy = go;
                distance = temp;
            }
        }
        if (enemy == null)
        {//没有敌人在攻击范围内，不处理伤害

        }
        else
        {//面向距离最近的敌人
            Vector3 targetPos = enemy.transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
            enemy.GetComponent<AtkAndDemage>().TakeDamage(sATKDamage);
        }
    }

    //使用技能
    public void skillEvent()
    {
        GetComponent<ReleaseSwordLight>().SwordLightAtk(); //释放剑气
        AudioSource.PlayClipAtPoint(skillAudio, transform.position);
        AudioSource.PlayClipAtPoint(SwordLightAudio, transform.position);
    }

    public void DogdeEvent()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //以虚拟杆为优先
        if (JoyStick.h != 0 || JoyStick.v != 0)
        {
            h = JoyStick.h;
            v = JoyStick.v;
        }

        //默认向前
        if (h == 0 && v == 0)
        {
            h = 0;
            v = 1;
        }

        float dodgeDistance = 4.7f;
        float deltaX = h * dodgeDistance / Mathf.Sqrt(v * v + h * h);
        float deltaZ = v * dodgeDistance / Mathf.Sqrt(v * v + h * h);
        targetDir = new Vector3(deltaX, 0, deltaZ);

        GetComponent<PlayerCtrl>().dodgeFlag = true;
    }
}
