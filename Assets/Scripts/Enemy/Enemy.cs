using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHitable
{
    public EnemyData enemyData;
    protected Enemy_AI enemy_AI;

    [SerializeField] float hp;

    protected virtual void Awake()
    {
        enemy_AI = GetComponent<Enemy_AI>();
    }

    void OnEnable()
    {
        enemy_AI.CreateBehaviorTreeAIState();
        hp = enemyData.MaxHP;
    }

    public void Hit(float damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if(hp <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Resource.Destroy(gameObject);
    }
}
