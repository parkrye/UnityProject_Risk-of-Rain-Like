using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagArrow : ArrowType
{
    public UnityEvent<Transform> OnTriggerEnterEvent;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IHitable>()?.Hit(damage);
            GameManager.Pool.Release(gameObject);
        }
        else if (1 << other.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            GameManager.Pool.Release(gameObject);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnTriggerEnterEvent?.Invoke(transform);
    }
}
