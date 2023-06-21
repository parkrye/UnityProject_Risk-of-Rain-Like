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

        Enemy_Behavior_Attack enemy_Behavior_attack = new(gameObject);
        Main.AddChild(enemy_Behavior_attack);


        Enemy_Condition_CheckDistance enemy_CheckDistance = new(gameObject);
        Sub1.AddChild(enemy_CheckDistance);

        BT_Fallback Sub2 = new();
        Sub1.AddChild(Sub2);

        Enemy_Behavior_Approach enemy_Behavior_Approach = new(gameObject);
        Sub1.AddChild(enemy_Behavior_Approach);



        Enemy_Condition_CheckWall enemy_Condition_CheckWall = new(gameObject);
        Sub2.AddChild(enemy_Condition_CheckWall);

        BT_Sequence Sub3 = new();
        Sub2.AddChild(Sub3);

        // 텔레포트를 Sub2에


        Enemy_Condition_CheckBypassRoute enemy_Condition_CheckBypassRoute = new(gameObject);
        Sub3.AddChild(enemy_Condition_CheckBypassRoute);

        Enemy_Behavior_Bypass enemy_Behavior_Bypass = new(gameObject);
        Sub3.AddChild(enemy_Behavior_Bypass);

        AI.AddChild(Main);
    }
}
