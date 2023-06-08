using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : BaseUI
{
    public Transform followTarget;
    public Vector3 followOffset;

    void LateUpdate()
    {
        Follow();
    }

    public void SetTarget(Transform target)
    {
        followTarget = target;
        Follow();
    }

    public void SetOffset(Vector3 offset)
    {
        followOffset = offset;
        Follow();
    }

    void Follow()
    {

        if (followTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }
}
