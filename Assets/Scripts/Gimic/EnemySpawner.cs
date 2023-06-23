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
                GameObject enemy = EnemySummon.RandomLocationSummon(player, spawnDistance);
                enemy.GetComponent<Enemy>().EnemyDie.AddListener(EnemyDie);

                enemyCount++;
            }
        }
    }
}
