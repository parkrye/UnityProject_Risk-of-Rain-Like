using System.Collections;
using UnityEngine;

public class FollowBolt : BoltType
{
    [SerializeField] float range;
    [SerializeField] GameObject target;

    protected override IEnumerator ReadyToShot(float delay)
    {
        yield return new WaitForSeconds(delay);
        coll.enabled = true;
        trail.Clear();
        trail.enabled = true;
        GameManager.Resource.Destroy(gameObject, 5f);
        while (true)
        {
            if (target == null || target.activeInHierarchy || target.activeSelf)
            {
                if (SetTarget())
                {
                    transform.Translate((target.transform.position - transform.position) * speed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
                }
            }
            else
            {
                transform.Translate((target.transform.position - transform.position) * speed * Time.deltaTime, Space.World);
            }
                
            yield return new WaitForFixedUpdate();
        }
    }

    bool SetTarget()
    {
        PriorityQueue<GameObject, float> pq = new PriorityQueue<GameObject, float>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                pq.Enqueue(collider.gameObject, Vector3.Distance(transform.position, collider.transform.position));
            }
        }
        if(pq.Count > 0)
        {
            target = pq.Dequeue();
            return true;
        }
        return false;
    }
}
