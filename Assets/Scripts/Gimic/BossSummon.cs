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
    EnemyData bossData;

    public UnityEvent<LevelScene.LevelState> ObjectStateEvent;

    void OnEnable()
    {
        EnemyData[] bossList = GameManager.Resource.LoadAll<EnemyData>("Boss");
        bossData = bossList[Random.Range(0, bossList.Length)];
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
        counter = 0;
        GetComponent<SphereCollider>().radius = chargeDistance;
        GetComponent<CircleDrawer>().Setting(transform.position + Vector3.up, 60, chargeDistance * 0.5f);
        ObjectStateEvent?.Invoke(LevelScene.LevelState.Keep);

        while (charge < chargeTime)
        {
            if (inArea)
            {
                charge += Time.deltaTime;
            }

            if(charge > counter * (chargeTime * 0.2f))
            {
                counter++;
                SummonGuardians();
            }
            yield return null;
        }
        SummonBoss();
    }

    void SummonGuardians()
    {
        for(int i = 0; i < counter * 5; i++)
        {
            EnemySummon.RandomLocationSummon(transform, 30f);
        }
    }

    void SummonBoss()
    {
        zone.SetActive(false);
        ObjectStateEvent?.Invoke(LevelScene.LevelState.Fight);
        GameObject boss = EnemySummon.TargetLocationSummon(bossTransform, bossData);
        boss.GetComponent<Boss>().OnEnemyDieEvent.AddListener(BeatBoss);
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
