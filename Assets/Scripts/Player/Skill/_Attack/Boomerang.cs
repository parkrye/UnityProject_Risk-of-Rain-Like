using System.Collections;
using UnityEngine;

public class Boomerang : BoltType
{
    [SerializeField] float rotateSpeed;
    Transform home;
    bool back;

    public void Shot(Transform transform, Vector3 target, float _damage, float delay = 0f)
    {
        home = transform;
        transform.LookAt(target + Vector3.up * yModifier);
        damage = _damage;
        StartCoroutine(ReadyToShot(delay));
    }

    protected override IEnumerator ReadyToShot(float delay)
    {
        yield return new WaitForSeconds(delay);
        coll.enabled = true;

        back = false;
        float backTime = 0f;
        Vector3 rotate = Vector3.up * rotateSpeed * Time.deltaTime;
        while (backTime < 5f)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            coll.transform.Rotate(rotate);
            backTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        back = true;
        GameManager.Resource.Destroy(gameObject, 10f);
        while (true)
        {
            transform.Translate((home.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
            coll.transform.Rotate(rotate);
            yield return new WaitForFixedUpdate();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage);
        }
        
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            back = false;
            GameManager.Pool.Release(gameObject);
        }
        else if(back && other.CompareTag("Player"))
        {
            back = false;
            GameManager.Pool.Release(gameObject);
        }
    }
}
