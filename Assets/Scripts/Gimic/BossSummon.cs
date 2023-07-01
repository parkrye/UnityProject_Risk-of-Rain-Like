using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossSummon : MonoBehaviour
{
    public float charge, chargeTime, chargeDistance;
    int counter;
    [SerializeField] bool inArea, startSummon, onGizmo;
    [SerializeField] Transform bossTransform;
    [SerializeField] GameObject zone, gate;
    [SerializeField] EnemyData bossData;
    ParticleSystem summonParticle, bossSummonParticle;

    public UnityEvent<LevelScene.LevelState> ObjectStateEvent;

    void Awake()
    {
        EnemyData[] bossList = GameManager.Resource.LoadAll<EnemyData>("Boss");
        bossData = bossList[Random.Range(0, bossList.Length)];
        summonParticle = GameManager.Resource.Load<ParticleSystem>("Particle/_Spawn");
        bossSummonParticle = GameManager.Resource.Load<ParticleSystem>("Particle/_Boss");
    }

    public void StartCharge()
    {
        if (!startSummon)
        {
            StartCoroutine(SummonCharge());
        }
    }

    IEnumerator SummonCharge()
    {
        startSummon = true;
        counter = 1;
        GetComponent<SphereCollider>().radius = chargeDistance;
        GetComponent<CircleDrawer>().Setting(transform.position + Vector3.up, 60, chargeDistance * 0.5f);
        ObjectStateEvent?.Invoke(LevelScene.LevelState.Keep);

        while (charge < chargeTime)
        {
            if (inArea)
            {
                charge += 0.1f;
            }

            if(charge > counter * (chargeTime * 0.3f))
            {
                counter++;
                SummonGuardians();
            }
            yield return new WaitForSeconds(0.1f);
        }
        SummonBoss();
    }

    void SummonGuardians()
    {
        for(int i = 0; i < counter + GameManager.Data.Records["Difficulty"]; i++)
        {
            GameObject enemy = EnemySummon.RandomLocationSummon(transform, 30f);
            if (enemy)
            {
                ParticleSystem effect = GameManager.Resource.Instantiate(summonParticle, true);
                effect.transform.position = enemy.transform.position;
                effect.transform.LookAt(GameManager.Data.Player.playerTransform.position);
                GameManager.Resource.Destroy(effect.gameObject, 2f);
            }
            else
            {
                break;
            }
        }
    }

    void SummonBoss()
    {
        zone.SetActive(false);
        ObjectStateEvent?.Invoke(LevelScene.LevelState.Fight);
        ParticleSystem effect = GameManager.Resource.Instantiate(bossSummonParticle, true);
        effect.transform.position = bossTransform.position;
        effect.transform.LookAt(GameManager.Data.Player.playerTransform.position);
        GameManager.Resource.Destroy(effect.gameObject, 3.5f);
        StartCoroutine(BossSummonRoutine());
    }

    IEnumerator BossSummonRoutine()
    {
        yield return new WaitForSeconds(3f);
        GameObject boss = EnemySummon.TargetLocationSummon(bossTransform, bossData);
        if (boss)
        {
            boss.GetComponent<Boss>().OnEnemyDieEvent.AddListener(BeatBoss);
        }
    }

    public void BeatBoss()
    {
        ObjectStateEvent?.Invoke(LevelScene.LevelState.Win);
        gate.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (charge < chargeTime)
            {
                inArea = true;
            ObjectStateEvent?.Invoke(LevelScene.LevelState.Keep);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(charge < chargeTime)
            {
                inArea = false;
                ObjectStateEvent?.Invoke(LevelScene.LevelState.ComeBack);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (onGizmo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chargeDistance);
        }
    }
}
