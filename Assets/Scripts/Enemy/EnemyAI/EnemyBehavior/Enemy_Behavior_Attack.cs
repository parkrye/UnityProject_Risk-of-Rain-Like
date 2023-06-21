using UnityEngine;

public class Enemy_Behavior_Attack : BT_Action
{
    public Enemy_Behavior_Attack(GameObject _enemy)
    {
        enemy = _enemy;
        player = GameManager.Data.Player.gameObject;
    }

    public override NodeState Renew()
    {
        Debug.Log("Attack");
        OnAttack();
        return NodeState.Running;
    }

    public override void Initialize()
    {
        base.Initialize();
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void Terminate()
    {
        base.Terminate();
        enemy.GetComponent<Enemy>().StopAttack();
    }

    void OnAttack()
    {
        enemy.GetComponent<Enemy>().StartAttack();
        enemy.transform.LookAt(player.transform.position);
    }
}
