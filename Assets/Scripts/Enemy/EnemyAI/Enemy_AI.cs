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

    public abstract void CreateBehaviorTreeAIState();
}
