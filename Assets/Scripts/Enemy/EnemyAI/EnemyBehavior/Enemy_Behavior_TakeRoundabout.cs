using UnityEngine;

public class Enemy_Behavior_TakeRoundabout : BT_Action
{
    Vector3 roundaboutDir;

    public Enemy_Behavior_TakeRoundabout(GameObject _enemy)
    {
        enemy = _enemy;
        player = GameManager.Data.Player.gameObject;
    }

    public override void Initialize()
    {
        roundaboutDir = PathFinder.PathFinding(enemy.transform, player.transform, enemy.GetComponent<Enemy>().enemyData.Range);
    }

    public override void Terminate()
    {
        roundaboutDir = Vector3.zero;
    }

    public override NodeState Renew()
    {
        OnChase();
        return NodeState.Running;
    }

    void OnChase()
    {
        if (player)
        {
            enemy.transform.LookAt(enemy.transform.position + roundaboutDir);

            enemy.GetComponent<Rigidbody>().velocity = (enemy.transform.forward * enemy.GetComponent<Enemy>().enemyData.Speed);
            Debug.Log(enemy.GetComponent<Rigidbody>().velocity);
        }
    }
}
