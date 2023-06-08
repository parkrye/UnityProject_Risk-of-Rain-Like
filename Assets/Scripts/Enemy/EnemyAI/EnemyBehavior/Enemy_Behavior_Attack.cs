using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior_Attack : BT_Action
{
    public override void Initialize()
    {

    }

    public override void Terminate()
    {

    }

    public override NodeState Renew()
    {
        OnAttack();
        return NodeState.Running;
    }

    void OnAttack()
    {
        Debug.Log("Attack");
    }
}
