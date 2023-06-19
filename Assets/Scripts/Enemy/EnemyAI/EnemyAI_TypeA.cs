using UnityEngine;

/// <summary>
/// Approach ( => Bypass ) => Attack
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

        BT_Fallback Sub1 = new();
        Main.AddChild(Sub1);

        Enemy_Condition_CheckDistance enemy_CheckDistance = new(gameObject);
        Sub1.AddChild(enemy_CheckDistance);

        BT_Sequence Sub2 = new();
        Sub1.AddChild(Sub2);

        Enemy_Condition_CheckWall enemy_Condition_CheckWall = new(gameObject);
        Sub2.AddChild(enemy_Condition_CheckWall);

        Enemy_Behavior_Bypass enemy_Behavior_TakeRoundabout = new(gameObject);
        Sub2.AddChild(enemy_Behavior_TakeRoundabout);

        Enemy_Behavior_Approach enemy_Behavior_Approach = new(gameObject);
        Sub1.AddChild(enemy_Behavior_Approach);

        Enemy_Behavior_Attack enemy_Behavior_attack = new(gameObject);
        Main.AddChild(enemy_Behavior_attack);

        AI.AddChild(Main);
    }
}
