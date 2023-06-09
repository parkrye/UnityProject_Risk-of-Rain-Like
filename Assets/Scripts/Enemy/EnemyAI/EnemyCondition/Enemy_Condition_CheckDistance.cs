using UnityEngine;

public class Enemy_Condition_CheckDistance : BT_Condition
{
    GameObject enemy;

    public Enemy_Condition_CheckDistance(GameObject _enemy)
    {
        enemy = _enemy;
    }

    public override NodeState Renew()
    {
        GameObject player = GameManager.Data.Player.gameObject;
        if (player)
        {
            float fDistance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (fDistance < enemy.GetComponentInChildren<EnemyData>().Range)
            {
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}
