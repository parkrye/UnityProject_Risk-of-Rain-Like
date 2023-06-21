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
        bypassStack = enemy.GetComponent<Enemy_AI>().bypassRoute;
    }

    public override void Terminate()
    {
        bypassStack.Clear();
    }

    public override NodeState Renew()
    {
        Debug.Log("Bypass");
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
