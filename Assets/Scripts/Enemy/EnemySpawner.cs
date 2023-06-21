using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;

    public void Initialize(float _spawnDelay)
    {
        spawnDelay = _spawnDelay;

        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            EnemyData enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Bat");
            GameManager.Resource.Instantiate(enemyData.enemy, transform.position, transform.rotation, true);
        }
    }
}
