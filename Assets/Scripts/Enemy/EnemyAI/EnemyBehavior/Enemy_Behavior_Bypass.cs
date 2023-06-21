using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior_Bypass : BT_Action
{
    Stack<Vector3> bypassStack;

    public Enemy_Behavior_Bypass(GameObject _enemy)
    {
        enemy = _enemy;
        player = GameManager.Data.Player.gameObject;
    }

    /// <summary>
    /// 초기화시 길찾기 루트 생성
    /// </summary>
    public override void Initialize()
    {
        if (GetParent() is not BT_Composite)
            throw new Exception("Enemy_Behavior_Bypass는 BT_Composite의 자식으로만 존재할 수 있습니다");
        if((GetParent() as BT_Composite).GetChild(GetIndex() - 1) is not Enemy_Condition_CheckBypassRoute)
            throw new Exception("Enemy_Behavior_Bypass는 Enemy_Condition_CheckBypassRoute 다음에만 존재할 수 있습니다");
        bypassStack = ((GetParent() as BT_Composite).GetChild(GetIndex() - 1) as Enemy_Condition_CheckBypassRoute).bypassStack;
    }

    public override void Terminate()
    {
        bypassStack.Clear();
    }

    public override NodeState Renew()
    {
        OnBypass();
        return NodeState.Running;
    }

    void OnBypass()
    {
        if (player)
        {
            enemy.GetComponent<Rigidbody>().velocity = (bypassStack.Peek().normalized * enemy.GetComponent<Enemy>().enemyData.Speed);
            if(Vector3.Distance(enemy.transform.position, bypassStack.Peek()) <= 0.5f)
                bypassStack.Pop();
        }
    }
}
