using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 控制怪物的自动生成 
 */
public class SpawnManager : MonoBehaviour
{
    //单例模式
    public static SpawnManager _instance;

    public EnemySpawn[] monsterSpawnArray;
    public EnemySpawn[] bossSpawnArray;

    public List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        _instance = this;   //单例模式赋值
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        //第一波敌人生成
        foreach(EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawm());
        }

        while(enemyList.Count>0)
        {
            yield return new WaitForSeconds(0.2f);  //集合不为空等0.2秒
        }

        //第二波敌人生成
        foreach (EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawm());
        }
        yield return new WaitForSeconds(0.5f);

        foreach (EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawm());
        }

        while (enemyList.Count > 0)
        {
            yield return new WaitForSeconds(0.2f);  //集合不为空等0.2秒
        }

        //第三波敌人产生
        //foreach (EnemySpawn s in monsterSpawnArray)
        //{
        //    enemyList.Add(s.Spawm());
        //}
        //yield return new WaitForSeconds(0.5f);
        //foreach (EnemySpawn s in monsterSpawnArray)
        //{
        //    enemyList.Add(s.Spawm());
        //}
        //yield return new WaitForSeconds(0.5f);
        foreach (EnemySpawn s in bossSpawnArray)
        {
            enemyList.Add(s.Spawm());
        }
    }
}
