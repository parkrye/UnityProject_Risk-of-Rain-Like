using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyCount, enemyLimit;
    [SerializeField] AudioSource summonAudio;
    Transform player;
    ParticleSystem summonParticle;
    IEnumerator Spawn;

    public void Initialize(float _spawnDelay, float _spawnDistance, int _enemyLimit)
    {
        spawnDelay = _spawnDelay;
        spawnDistance = _spawnDistance;
        enemyLimit = _enemyLimit;
        player = GameManager.Data.Player.transform;
        summonParticle = GameManager.Resource.Load<ParticleSystem>("Particle/_Spawn");
        Spawn = SpawnRoutine();

        StartCoroutine(Spawn);
    }

    public void EnemyDie()
    {
        enemyCount--;
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if(enemyCount < enemyLimit)
            {
                GameObject enemy = EnemySummon.RandomLocationSummon(player, spawnDistance);
                if (enemy)
                {
                    summonAudio.Play();
                    enemy.GetComponent<Enemy>().OnEnemyDieEvent.AddListener(EnemyDie);
                    ParticleSystem effect = GameManager.Resource.Instantiate(summonParticle, true);
                    effect.transform.position = enemy.transform.position;
                    effect.transform.LookAt(GameManager.Data.Player.playerTransform.position);

                    enemyCount++;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void StopSpawn(LevelScene.LevelState levelState)
    {
        if(levelState == LevelScene.LevelState.Keep)
            StopCoroutine(Spawn);
    }
}
