using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonsterAtkAndDamage : AtkAndDemage
{
    private Transform player;
    public GameObject MonsterHitFx;
    public AudioClip MonsterHitAudio;

    private void Awake()
    {
        base.Awake();   //防止父类awake被覆盖后导致父类awake不被调用
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;

    }

    public void MonsterAtkEvent()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            player.GetComponent<AtkAndDemage>().TakeDamage(normalAttack);
            Instantiate(MonsterHitFx, player.transform.position + Vector3.up, player.transform.rotation);
            AudioSource.PlayClipAtPoint(MonsterHitAudio, transform.position);
        }
    }
}
