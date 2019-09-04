using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtkAndDamage : AtkAndDemage
{
    private Transform player;
    public GameObject BossSkill1Fx;
    public GameObject BossSkill2Fx;
    public GameObject BossSkill3Fx;
    public AudioClip BossSkill1Audio;
    public AudioClip BossSkill2Audio;
    public AudioClip BossSkill3Audio;

    private void Awake()
    {
        base.Awake();   //防止父类awake被覆盖后导致父类awake不被调用
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;

    }

    public void BossSkill1Event()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            player.GetComponent<AtkAndDemage>().TakeDamage(normalAttack);
            Instantiate(BossSkill1Fx, player.transform.position + Vector3.up, player.transform.rotation);
            AudioSource.PlayClipAtPoint(BossSkill1Audio, transform.position);
            AudioSource.PlayClipAtPoint(BossSkill3Audio, transform.position);
        }
    }
    public void BossSkill2Event()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            player.GetComponent<AtkAndDemage>().TakeDamage(normalAttack);
            Instantiate(BossSkill2Fx, player.transform.position + Vector3.up, player.transform.rotation);
            AudioSource.PlayClipAtPoint(BossSkill2Audio, transform.position);
            AudioSource.PlayClipAtPoint(BossSkill3Audio, transform.position);
        }
    }
    public void BossSkill3Event()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            player.GetComponent<AtkAndDemage>().TakeDamage(normalAttack);
            Instantiate(BossSkill3Fx, player.transform.position + Vector3.up, player.transform.rotation);
            AudioSource.PlayClipAtPoint(BossSkill3Audio, transform.position);
        }
    }
}
