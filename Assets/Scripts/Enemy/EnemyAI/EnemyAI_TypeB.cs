using System.Collections;
using UnityEngine;

/// <summary>
/// 보스 몬스터 AI
/// 플레이어와의 거리에 따라 두가지 패턴을 취하는 AI
/// 보스 패턴은 보스 몬스터마다 구현
/// </summary>
public class EnemyAI_TypeB : EnemyAI
{
    enum AI_State { CloseRange, FarRange }
    AI_State state;
    [SerializeField] bool onGizmo;
    Boss boss;
    Vector3 playerPos;

    protected override void Awake()
    {
        base.Awake();
        boss = GetComponent<Boss>();
        state = AI_State.FarRange;
    }

     protected override void OnEnable()
    {
        base.OnEnable();
        boss.StartAttack();
    }

    void FixedUpdate()
    {
        transform.LookAt(playerPos);
    }

    protected override IEnumerator StateRoutine()
    {
        while (this)
        {
            playerPos = playerTransform.position + Vector3.up;
            if (Vector3.SqrMagnitude(playerPos - enemy.EnemyPos) <= (enemy.enemyData.Range * enemy.enemyData.Range))
            {
                if (state != AI_State.CloseRange)
                {
                    boss.ChangeToClose();
                    state = AI_State.CloseRange;
                }
            }
            else
            {
                if (state != AI_State.FarRange)
                {
                    boss.ChangeToFar();
                    state = AI_State.FarRange;
                }
            }
            yield return null;
        }
    }

    protected override IEnumerator BehaviorRoutine()
    {
        yield return null;
    }
}
