using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay, spawnDistance;
    Transform player;

    public void Initialize(float _spawnDelay, float _spawnDistance)
    {
        spawnDelay = _spawnDelay;
        spawnDistance = _spawnDistance;
        player = GameManager.Data.Player.transform;

        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnDelay);
            EnemyData[] enemyDatas = GameManager.Resource.LoadAll<EnemyData>("Enemy");

            Vector3 spawnPosition = Vector3.zero;
            float remainDistance = spawnDistance;

            spawnPosition.x = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
            remainDistance -= spawnPosition.x;

            spawnPosition.y = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
            remainDistance -= spawnPosition.y;

            spawnPosition.z = remainDistance;

            GameManager.Resource.Instantiate(enemyDatas[Random.Range(0, enemyDatas.Length)].enemy, player.position + spawnPosition, Quaternion.identity, true);
        }
    }
}
