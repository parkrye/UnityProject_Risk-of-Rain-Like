using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossSummon : MonoBehaviour
{
    public float charge, chargeTime, chargeDistance;
    int counter;
    [SerializeField] bool inArea;

    public UnityEvent<SceneInfoUI.ObjectState> ObjectStateEvent;

    public void StartCharge()
    {
        StartCoroutine(SummonCharge());
    }

    IEnumerator SummonCharge()
    {
        counter = 0;
        GetComponent<SphereCollider>().radius = chargeDistance;
        GetComponent<CircleDrawer>().Setting(60, chargeDistance);
        ObjectStateEvent?.Invoke(SceneInfoUI.ObjectState.Keep);

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
        ObjectStateEvent?.Invoke(SceneInfoUI.ObjectState.Fight);
        Debug.Log("Boss Summoned!");
    }

    public void WinByBoss()
    {
        ObjectStateEvent?.Invoke(SceneInfoUI.ObjectState.Win);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
            ObjectStateEvent?.Invoke(SceneInfoUI.ObjectState.Keep);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inArea = false;
            ObjectStateEvent?.Invoke(SceneInfoUI.ObjectState.ComeBack);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chargeDistance);
    }
}
