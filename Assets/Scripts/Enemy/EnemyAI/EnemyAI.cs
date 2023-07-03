using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected Enemy enemy;
    protected Transform playerTransform;

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
        playerTransform = GameManager.Data.Player.playerTransform;
    }
}
