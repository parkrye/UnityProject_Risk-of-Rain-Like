using UnityEngine;

/// <summary>
/// 보스 몬스터 AI
/// 플레이어와의 거리에 따라 두가지 패턴을 취하는 AI
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

    void OnEnable()
    {
        boss.StartAttack();
    }

    void Update()
    {
        playerPos = playerTransform.position + Vector3.up;
        StateCheck();
    }

    void StateCheck()
    {
        if (Vector3.SqrMagnitude(playerPos - enemy.enemyPos) <= Mathf.Pow(enemy.enemyData.Range, 2))
        {
            if(state != AI_State.CloseRange)
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
    }

    void FixedUpdate()
    {
        transform.LookAt(playerPos);
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }
}
