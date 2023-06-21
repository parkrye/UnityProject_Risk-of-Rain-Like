using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_AI : MonoBehaviour
{
    protected BT_Root AI;

    protected virtual void Update()
    {
        AI.Tick();
    }

    public Stack<Vector3> bypassRoute { get; private set; }

    public void SetBypassRoute(Stack<Vector3> route)
    {
        bypassRoute = route;
    }

    public abstract void CreateBehaviorTreeAIState();
}
