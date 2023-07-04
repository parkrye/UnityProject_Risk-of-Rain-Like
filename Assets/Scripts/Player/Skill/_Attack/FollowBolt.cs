using System.Collections;
using UnityEngine;

public class FollowBolt : BoltType
{
    [SerializeField] float range;
    [SerializeField] GameObject target;

    protected override IEnumerator ReadyToShot(float delay)
    {
        yield return new WaitForSeconds(delay);
        for(int i = 0; i < trails.Length; i++)
        {
            trails[i].Clear();
            trails[i].enabled = true;
        }
        coll.enabled = true;
        GameManager.Resource.Destroy(gameObject, 5f);
        while (true)
        {
            if (!target || !target.activeInHierarchy || !target.activeSelf)
            {
                if (SetTarget())
                {
                    transform.Translate(speed * Time.deltaTime * (target.transform.position + Vector3.up - transform.position).normalized, Space.World);
                }
                else
                {
                    transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
                }
            }
            else
            {
                transform.Translate(speed * Time.deltaTime * (target.transform.position + Vector3.up - transform.position).normalized, Space.World);
            }
                
            yield return new WaitForFixedUpdate();
        }
    }

    bool SetTarget()
    {
        PriorityQueue<GameObject, float> pq = new PriorityQueue<GameObject, float>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                pq.Enqueue(colliders[i].gameObject, Vector3.SqrMagnitude(colliders[i].transform.position - transform.position));
            }
        }
        if (pq.Count > 0)
        {
            target = pq.Dequeue();
            return true;
        }
        return false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
