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
        OnAttack();
        return NodeState.Running;
    }

    public override void Initialize()
    {
        base.Initialize();
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnAttack()
    {
        Debug.Log("Attack");
        enemy.transform.LookAt(player.transform.position);
    }
}
