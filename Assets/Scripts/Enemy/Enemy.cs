using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    protected Enemy_AI enemy_AI;

    protected virtual void OnEnable()
    {
        enemy_AI = GetComponent<Enemy_AI>();
        enemy_AI.CreateBehaviorTreeAIState();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyData.Range);
    }
}
