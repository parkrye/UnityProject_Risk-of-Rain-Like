using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    TrailRenderer trail;
    float damage;
    [SerializeField] float speed;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        trail.enabled = false;
    }

    public void Shot(float _damage, float delay)
    {
        damage = _damage;
        StartCoroutine(ReadyToShot(delay));
    }

    public void Shot(float _damage = 1f)
    {
        Shot(_damage, 0f);
    }

    IEnumerator ReadyToShot(float delay)
    {
        yield return new WaitForSeconds(delay);
        trail.Clear();
        trail.enabled = true;
        GameManager.Resource.Destroy(gameObject, 10f);
        while (true)
        {
            transform.Translate((transform.forward * speed + Vector3.up * Physics.gravity.y) * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

    void OnDisable()
    {
        trail.Clear();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
        else if(1 << other.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            GameManager.Pool.Release(gameObject);
        }
    }
}
