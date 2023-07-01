using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// 플레이어 캐릭터의 데이터 저장소와 플레이어 캐릭터와 연관된 스크립트들의 중간 매체 역할을 하는 스크립트
/// </summary>
public class PlayerDataModel : MonoBehaviour
{
    public PlayerActionController playerAction;         // 플레이어의 행동과 관련된 작업을 하는 스크립트
    public PlayerMovementController playerMovement;     // 플레이어의 이동과 관련된 작업을 하는 스크립트
    public PlayerCameraController playerCamera;         // 플레이어의 카메라와 관련된 작업을 하는 스크립트
    public PlayerSystemController playerSystem;         // 플레이어 캐릭터와 관련된 작업을 하는 스크립트
    public Hero hero;                                   // 현재 사용중인 캐릭터 직업
    public List<Hero> heroList;                         // 사용할 수 있는 직업 리스트
    [SerializeField][Range(0, 2)] public int heroNum;   // 선택된 직업 번호
    public Animator animator;                           // 캐릭터 애니메이터(직업에 포함된 애니메이터를 참조)
    public Rigidbody rb;                                // 캐릭터 강체
    public Inventory inventory;                         // 캐릭터 인벤토리
    public Inventory Inventory { get { return inventory; } set { inventory = value; } }

    [SerializeField] float maxHP, nowHP, exp;   // 최대 체력, 현재 체력, 현재 경험치
    [SerializeField] int level, coin;           // 현재 레벨, 현재 금화
    [SerializeField] float[] status;            // 능력치 배열(이동속도, 점프높이, 공격력, 치명타 확률, 치명타 배율)
    public float[] buffModifier;                // 각 능력치에 대한 버프 배열

    public int jumpLimit, jumpCount;                                                            // 한계 점프 수, 현재 점프 수
    public bool attackCooldown, controllable, dodgeDamage, onGizmo, onESC, alive, onSession;    // 공격 쿨타임 여부, 조종 가능 여부, 회피 여부, 기즈모 출력 여부, ESC창 여부

    public float coolTime;                  // 쿨타임
    public bool[] coolChecks = new bool[4]; // 각 액션에 대한 쿨타임 여부

    public float mouseSensivity;            // 마우스 민감도
    [SerializeField] float playerTimeScale; // 플레이어 시간 배율

    public UnityEvent OnLevelEvent, OnHPEvent, OnEXPEvent;  // 레벨 업에 대한 이벤트, 체력 변화에 대한 이벤트, 경험치 변화에 대한 이벤트
    public UnityEvent<int> OnCoinEvent;                     // 금화 획득에 대한 이벤트
    public List<IDamageSubscriber> damageSubscribers;       // 데미지 발생과 연관된 인터페이스 리스트

    void Awake()
    {
        Initailze();
    }

    /// <summary>
    /// 초기화 메소드
    /// </summary>
    void Initailze()
    {
        GameManager.Data.Player = this;

        heroList = new List<Hero>();
        heroList.AddRange(GetComponentsInChildren<Hero>());
        foreach (var hero in heroList)
            hero.gameObject.SetActive(false);

        rb = GetComponent<Rigidbody>();
        playerAction = GetComponent<PlayerActionController>();
        playerMovement = GetComponent<PlayerMovementController>();
        playerCamera = GetComponent<PlayerCameraController>();
        playerSystem = GetComponent<PlayerSystemController>();
        Inventory = GetComponent<Inventory>();

        TimeScale = 1f;
        status = new float[5] { 5f, 10f, 10f, 1f, 1.2f };
        buffModifier = new float[5] { 1f, 1f, 1f, 1f, 1f };

        coolChecks = new bool[4];
        for (int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;

        damageSubscribers = new List<IDamageSubscriber>();
        alive = true;
    }

    public float MAXHP
    {
        get { return maxHP; }
        set
        {
            if (value < 0)
                maxHP = 1f;
            else
                maxHP = value;
            OnHPEvent?.Invoke();
        }
    }

    public float NOWHP
    {
        get { return nowHP; }
        set
        {
            if (value <= 0f)
            {
                if (alive)
                {
                    alive = false;
                    OnHPEvent?.Invoke();
                    playerSystem.Die();
                }
            }
            else
            {
                if(value >= nowHP)
                    GameManager.Data.Records["Heal"] += value - nowHP;
                if(value > MAXHP)
                    nowHP = MAXHP;
                else
                    nowHP = value;
                OnHPEvent?.Invoke();
            }
        }
    }

    public float EXP
    {
        get { return exp; }
        set 
        {
            exp = value;
            while (exp >= LEVEL * 100f)
            {
                exp -= LEVEL * 100f;
                LEVEL++;
            }
            OnEXPEvent?.Invoke();
        }
    }

    public int LEVEL
    {
        get { return level; }
        set
        {
            level = value;
            maxHP *= 1.1f;
            nowHP = MAXHP;
            OnLevelEvent?.Invoke();
        }
    }

    public float MoveSpeed
    {
        get
        {
            return status[0] * buffModifier[0];
        }
        set
        {
            status[0] = value;
        }
    }
    public float JumpPower
    {
        get
        {
            return status[1] * buffModifier[1];
        }
        set
        {
            status[1] = value;
        }
    }
    public float AttackDamage
    {
        get
        {
            return status[2] * buffModifier[2];
        }
        set
        {
            status[2] = value;
        }
    }
    public float CriticalProbability
    {
        get
        {
            return status[3] * buffModifier[3];
        }
        set
        {
            status[3] = value;
        }
    }
    public float CriticalRatio
    {
        get
        {
            return status[4] * buffModifier[4];
        }
        set
        {
            status[4] = value;
        }
    }

    public float TimeScale
    { 
        get 
        { 
            return playerTimeScale;
        }
        set
        {
            playerTimeScale = value;
            animator.speed = playerTimeScale;
        }
    }

    public Transform playerTransform
    {
        get { return transform; }
        set 
        {
            transform.SetPositionAndRotation(value.position, value.rotation);
            transform.localScale = value.localScale;
        }
    }

    public int Coin
    {
        get { return coin; }
        set { coin = value; OnCoinEvent?.Invoke(coin); }
    }
}
