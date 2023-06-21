using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 우회로가 있다면 성공, 없다면 실패
/// </summary>
public class Enemy_Condition_CheckBypassRoute : BT_Condition
{
    public Stack<Vector3> bypassStack { get; private set; }

    public Enemy_Condition_CheckBypassRoute(GameObject _enemy)
    {
        enemy = _enemy;
        player = GameManager.Data.Player.gameObject;
        bypassStack = PathFinder.PathFindingForAerial(enemy.transform, player.transform, enemy.GetComponent<Enemy>().enemyData.Range);
    }

    public override NodeState Renew()
    {
        if(bypassStack.Count > 0)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
