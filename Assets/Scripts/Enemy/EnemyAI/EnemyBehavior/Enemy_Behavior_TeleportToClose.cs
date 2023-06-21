using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Behavior_TeleportToClose : BT_Action
{
    public Enemy_Behavior_TeleportToClose(GameObject _enemy)
    {
        enemy = _enemy;
        player = GameManager.Data.Player.gameObject;
    }

    public override NodeState Renew()
    {
        RaycastHit hit;
        Ray ray = new Ray(player.transform.position, -player.transform.forward);
        if (Physics.Raycast(ray, out hit, enemy.GetComponent<Enemy>().enemyData.Range, LayerMask.GetMask("Ground")))
        {
            enemy.transform.position = hit.point;
        }
        else
        {
            enemy.transform.position = player.transform.position - player.transform.forward * enemy.GetComponent<Enemy>().enemyData.Range;
        }

        return NodeState.Success;
    }
}
