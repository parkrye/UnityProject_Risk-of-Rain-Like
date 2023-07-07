using System.Collections;
using UnityEngine;

/// <summary>
/// 패턴 1-1: 흡수
/// 패턴 1-2: 마법탄 발사
/// 패턴 2-1: 순간이동
/// 패턴 1-2: 에너미 소환
/// </summary>
public class EvilMage : Boss
{
    EnemyDrain enemyDrain;
    EnemyBolt enemyBolt;
    ParticleSystem teleportParticle;

    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Boss/Mage");
        enemyDrain = GameManager.Resource.Load<EnemyDrain>("EnemyAttack/EnemyDrain");
        enemyBolt = GameManager.Resource.Load<EnemyBolt>("EnemyAttack/EnemyBolt");
        teleportParticle = GameManager.Resource.Load<ParticleSystem>("Particle/FireworkBlueLarge");
        base.Awake();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                switch (lifeMode)
                {
                    case LifeMode.Full:
                        switch (rangeMode)
                        {
                            case RangeMode.NearRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(DrainRoutine());
                                break;
                            case RangeMode.FarRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(BoltRoutine());
                                break;
                        }
                        break;
                    case LifeMode.Half:
                        switch (rangeMode)
                        {
                            case RangeMode.NearRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(TeleportRoutine());
                                break;
                            case RangeMode.FarRange:
                                if (!patternCoroutineIsRunning)
                                    StartCoroutine(SummonRoutine());
                                break;
                        }
                        break;
                }
                yield return new WaitForSeconds(enemyData.AttackSpeed);
            }
            yield return null;
        }
    }

    IEnumerator DrainRoutine()
    {
        patternCoroutineIsRunning = true;
        transform.LookAt(playerTransform);
        animator.SetBool("Drain", true);
        GameManager.Resource.Instantiate(enemyDrain, attackTransform.position, Quaternion.identity, true).StartDrain(this, enemyData.floatdatas[0]);
        yield return new WaitForSeconds(enemyData.floatdatas[0]);
        animator.SetBool("Drain", false);
        transform.LookAt(playerTransform);
        patternCoroutineIsRunning = false;
    }

    IEnumerator BoltRoutine()
    {
        patternCoroutineIsRunning = true;
        transform.LookAt(playerTransform);
        animator.SetTrigger("Bolt");
        for (int i = 0; i < 5; i++)
        {
            EnemyBolt boltAttack = GameManager.Resource.Instantiate(enemyBolt, attackTransform.position, Quaternion.identity, true);
            boltAttack.Shot(GameManager.Data.Player.transform.position, damage);
            yield return new WaitForSeconds(enemyData.AttackSpeed * 0.2f);
        }
        transform.LookAt(playerTransform);
        patternCoroutineIsRunning = false;
    }

    IEnumerator TeleportRoutine()
    {
        patternCoroutineIsRunning = true;
        transform.LookAt(playerTransform);
        animator.SetTrigger("Teleport");
        RaycastHit hit;
        for (int x = -1; x <= 1; x++)
        {
            for(int z = -1; z <= 1; z++)
            {
                if (x == z)
                    continue;
                if(Physics.Raycast(transform.position + 0.75f * enemyData.Range * Vector3.up, Vector3.down + Vector3.forward * z + Vector3.right * x , out hit, enemyData.Range, LayerMask.GetMask("Ground")))
                {
                    GameManager.Resource.Instantiate(teleportParticle, transform.position, Quaternion.identity);
                    transform.position = hit.point;
                    x = 2; z = 2;
                }
            }
        }
        yield return null;
        transform.LookAt(playerTransform);
        patternCoroutineIsRunning = false;
    }

    IEnumerator SummonRoutine()
    {
        patternCoroutineIsRunning = true;
        transform.LookAt(playerTransform);
        animator.SetTrigger("Summon");
        for(int i = 0; i < GameManager.Data.Records["Difficulty"]; i++)
            EnemySummon.RandomLocationSummon(transform, enemyData.Range);
        yield return null;
        transform.LookAt(playerTransform);
        patternCoroutineIsRunning = false;
    }
}
