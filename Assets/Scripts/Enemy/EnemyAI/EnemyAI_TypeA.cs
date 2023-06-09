using UnityEngine;

/// <summary>
/// Approach => Attack
/// </summary>
public class EnemyAI_TypeA : Enemy_AI
{
    protected override void Update()
    {
        AI.Tick();
    }

    public override void CreateBehaviorTreeAIState()
    {
        AI = new();

        BT_Sequence Main = new();

        BT_Fallback Sub = new();
        Main.AddChild(Sub);

        Enemy_Condition_CheckDistance enemy_CheckDistance = new(gameObject);
        Sub.AddChild(enemy_CheckDistance);
        Enemy_Behavior_Approach enemy_Behavior_Approach = new(gameObject);
        Sub.AddChild(enemy_Behavior_Approach);

        Enemy_Behavior_Attack enemy_Behavior_attack = new();
        Main.AddChild(enemy_Behavior_attack);

        AI.AddChild(Main);
    }
}
