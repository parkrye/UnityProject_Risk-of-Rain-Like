using System.Collections;
using UnityEngine;

public class Boomerang : BoltType
{
    [SerializeField] float rotateSpeed;
    Transform home;
    bool back;
    MeshRenderer child;

    protected override void Awake()
    {
        base.Awake();
        child = GetComponentInChildren<MeshRenderer>();
    }

    public void Shot(Transform _transform, Vector3 target, float _damage, float delay = 0f)
    {
        home = _transform;
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
        Vector3 rotate = transform.up * rotateSpeed * Time.deltaTime;
        while (backTime < 2f)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            child.transform.Rotate(rotate);
            backTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        back = true;
        GameManager.Resource.Destroy(gameObject, 5f);
        while (true)
        {
            transform.Translate((home.position + Vector3.up - transform.position).normalized * speed * Time.deltaTime, Space.World);
            child.transform.Rotate(rotate);
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
