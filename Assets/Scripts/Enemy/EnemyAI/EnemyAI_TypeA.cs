using System.Collections.Generic;
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
        // 트리 구성
        // AI - Main - ApproachInRange
        //     (and) - BypassAndAttack
        //           - Attack

        //             ApproachInRange - CheckDist: 거리 측정
        //             (or)            - Bypass: 우회
        //                             - Approach: 접근

        //                   Bypass - CheckWall: 벽 확인
        //                  (and)   - CheckBypass: 우회 확인
        //                          - Bypass: 우회

        //             BypassAndAttack  - Bypass
        //             (or)             - Attack

        //             Attack: 공격

        AI = new();

        BT_Sequence Main = new();


        BT_Fallback ApproachInRange = new();
        Main.AddChild(ApproachInRange);


        Enemy_Condition_CheckDistance enemy_CheckDistance = new(gameObject);
        ApproachInRange.AddChild(enemy_CheckDistance);

        BT_Sequence Bypass = new();
        ApproachInRange.AddChild(Bypass);

        Enemy_Behavior_Approach enemy_Behavior_Approach = new(gameObject);
        ApproachInRange.AddChild(enemy_Behavior_Approach);


        Enemy_Condition_CheckWall enemy_Condition_CheckWall = new(gameObject);
        Bypass.AddChild(enemy_Condition_CheckWall);

        Enemy_Condition_CheckBypassRoute enemy_Condition_CheckBypassRoute = new(gameObject);
        Bypass.AddChild(enemy_Condition_CheckBypassRoute);

        Enemy_Behavior_Bypass enemy_Behavior_Bypass = new(gameObject);
        Bypass.AddChild(enemy_Behavior_Bypass);


        BT_Fallback BypassAndAttack = new();
        Main.AddChild(BypassAndAttack);

        BypassAndAttack.AddChild(Bypass);

        Enemy_Behavior_Attack enemy_Behavior_attack = new(gameObject);
        BypassAndAttack.AddChild(enemy_Behavior_attack);


        Main.AddChild(enemy_Behavior_attack);

        AI.AddChild(Main);
    }
}
