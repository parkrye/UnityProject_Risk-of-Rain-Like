using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI_TypeA : MonoBehaviour
{
    private BT_Root AI;

    void Start()
    {
        CreateBehaviorTreeAIState();
    }

    void Update()
    {
        AI.Tick();
    }

    void CreateBehaviorTreeAIState()
    {
        AI = new();

        BT_Fallback Main = new();

        BT_Fallback Sub = new();

        Enemy_Condition_CheckDistance enemy_CheckDistance = new(gameObject);
        Sub.AddChild(enemy_CheckDistance);
        Enemy_Behavior_Approach enemy_Behavior_Approach = new(gameObject);
        Sub.AddChild(enemy_Behavior_Approach);

        Main.AddChild(Sub);

        Enemy_Behavior_Attack enemy_Behavior_attack = new();
        Main.AddChild(enemy_Behavior_attack);

        AI.AddChild(Main);
    }
}
