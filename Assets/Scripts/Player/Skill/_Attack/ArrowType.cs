using System.Collections;
using UnityEngine;

public class ArrowType : MonoBehaviour
{
    protected TrailRenderer trail;
    protected float damage, yVelocity;
    [SerializeField] protected float speed, yModifier, yAccModifier;

    protected virtual void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        trail.enabled = false;
        yVelocity = 0f;
    }

    public void Shot(Vector3 target, float _damage, float delay)
    {
        transform.LookAt(target + Vector3.up * yModifier);
        damage = _damage;
        StartCoroutine(ReadyToShot(delay));
    }

    public void Shot(Vector3 target, float _damage = 1f)
    {
        Shot(target, _damage, 0f);
    }

    protected virtual IEnumerator ReadyToShot(float delay)
    {
        yield return new WaitForSeconds(delay);
        trail.Clear();
        trail.enabled = true;
        GameManager.Resource.Destroy(gameObject, 10f);
        while (true)
        {
            transform.Translate((transform.forward * speed + Vector3.up * yVelocity) * Time.deltaTime, Space.World);
            yVelocity += Physics.gravity.y * yAccModifier;
            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual void OnDisable()
    {
        trail.Clear();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage, 0f);
            GameManager.Resource.Destroy(gameObject);
        }
        else if (1 << other.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
