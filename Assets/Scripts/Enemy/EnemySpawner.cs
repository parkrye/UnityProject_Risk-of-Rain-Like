using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyCount, enemyLimit;
    Transform player;

    public void Initialize(float _spawnDelay, float _spawnDistance, int _enemyLimit)
    {
        spawnDelay = _spawnDelay;
        spawnDistance = _spawnDistance;
        enemyLimit = _enemyLimit;
        player = GameManager.Data.Player.transform;

        StartCoroutine(Spawner());
    }

    public void EnemyDie()
    {
        enemyCount--;
    }

    IEnumerator Spawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if(enemyCount < enemyLimit)
            {
                EnemyData[] enemyDatas = GameManager.Resource.LoadAll<EnemyData>("Enemy");

                Vector3 spawnPosition = Vector3.zero;
                float remainDistance = spawnDistance;

                spawnPosition.x = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
                remainDistance -= spawnPosition.x;

                spawnPosition.y = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
                remainDistance -= spawnPosition.y;

                spawnPosition.z = remainDistance;

                GameObject enemy = GameManager.Resource.Instantiate(enemyDatas[Random.Range(0, enemyDatas.Length)].enemy, player.position + spawnPosition, Quaternion.identity, true);
                enemy.GetComponent<Enemy>().EnemyDie.AddListener(EnemyDie);

                enemyCount++;
            }
        }
    }
}
