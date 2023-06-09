using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior_Approach : BT_Action
{
    GameObject enemy;

    public Enemy_Behavior_Approach(GameObject _enemy)
    {
        enemy = _enemy;
    }

    public override void Initialize()
    {

    }

    public override void Terminate()
    {

    }

    public override NodeState Renew()
    {
        OnChase();
        return NodeState.Running;
    }

    void OnChase()
    {
        GameObject player = GameManager.Data.Player.gameObject;
        if (player)
        {
            Vector3 vDir = player.transform.position - enemy.transform.position;

            //회전
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(vDir), Time.deltaTime * 4.0f);

            //이동
            enemy.transform.Translate(Vector3.forward * enemy.GetComponentInChildren<EnemyData>().Speed * Time.deltaTime);
        }
    }
}
