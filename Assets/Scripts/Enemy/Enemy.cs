using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyData enemyData;
    Enemy_AI enemy_AI;

    void Awake()
    {
        enemyData = GetComponent<EnemyData>();
        enemy_AI = GetComponent<Enemy_AI>();
    }

    void OnEnable()
    {
        enemy_AI.CreateBehaviorTreeAIState();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyData.Range);
    }
}
