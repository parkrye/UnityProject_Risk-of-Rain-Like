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
        Debug.Log("Teleport");
        RaycastHit hit;
        Ray ray = new Ray(player.transform.position, -player.transform.forward + Vector3.up);
        if (Physics.Raycast(ray, out hit, enemy.GetComponent<Enemy>().enemyData.Range, LayerMask.GetMask("Ground")))
        {
            enemy.transform.position = hit.point;
        }
        else
        {
            enemy.transform.position = player.transform.position + (- player.transform.forward) * enemy.GetComponent<Enemy>().enemyData.Range + Vector3.up;
        }

        return NodeState.Success;
    }
}
