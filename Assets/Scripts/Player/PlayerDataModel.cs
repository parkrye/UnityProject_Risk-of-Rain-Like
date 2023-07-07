using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                                                        // 인벤토리에 대한 프로퍼티

    [SerializeField] float maxHP, nowHP, exp;           // 최대 체력, 현재 체력, 현재 경험치
    [SerializeField] int level, coin;                   // 현재 레벨, 현재 금화
    [SerializeField] float[] status;                    // 능력치 배열(이동속도, 점프높이, 공격력, 치명타 확률, 치명타 배율)
    public float[] buffModifier;                        // 각 능력치에 대한 버프 배열

    public int jumpLimit, jumpCount;                                                            // 한계 점프 수, 현재 점프 수
    public bool attackCooldown, controllable, dodgeDamage, onGizmo, onESC, alive, onSession;    // 공격 쿨타임 여부, 조종 가능 여부, 회피 여부, 기즈모 출력 여부, ESC창 여부

    public float coolTime;                                  // 쿨타임
    public bool[] coolChecks = new bool[4];                 // 각 액션 쿨타임 여부

    public float mouseSensivity;                            // 마우스 민감도
    [SerializeField] bool playerTimeScaleMultiplier;        // 플레이어 시간 배속 여부

    public UnityEvent OnLevelEvent, OnHPEvent, OnEXPEvent;  // 레벨 업 이벤트, 체력 변화 이벤트, 경험치 변화 이벤트
    public UnityEvent<int> OnCoinEvent;                     // 금화 획득이벤트
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
        // 데이터 매니저에 플레이어 등록
        GameManager.Data.Player = this;

        // 영웅 설정
        heroList = new List<Hero>();                            // 영웅 리스트 초기화
        heroList.AddRange(GetComponentsInChildren<Hero>());     // 캐릭터에 포함된 영웅들을 리스트에 추가
        for(int i = 0; i < heroList.Count; i++)
            heroList[i].gameObject.SetActive(false);            // 모든 영웅을 일단 비활성화

        // 필요 컴포넌트 참조
        rb = GetComponent<Rigidbody>();                             // 강체
        playerAction = GetComponent<PlayerActionController>();      // 행동 및 상호작용 스크립트
        playerMovement = GetComponent<PlayerMovementController>();  // 이동 스크립트
        playerCamera = GetComponent<PlayerCameraController>();      // 카메라 스크립트
        playerSystem = GetComponent<PlayerSystemController>();      // 기타 스크립트
        Inventory = GetComponent<Inventory>();                      // 인벤토리

        // 인게임 관련 데이터 초기화
        playerTimeScaleMultiplier = false;                          // 시간 배속 여부
        status = new float[5] { 5f, 10f, 10f, 1f, 1.2f };         // 초기 능력치 : 이동속도, 점프높이, 공격 대미지, 치명타 확률, 치명타 배율
        buffModifier = new float[5] { 1f, 1f, 1f, 1f, 1f };         // 버프 보정치 : 능력치와 같은 순서
        coolChecks = new bool[4];                                   // 4개 기술에 대한 쿨타임 체크
        for (int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;                                   // 쿨타임 초기화
        damageSubscribers = new List<IDamageSubscriber>();          // 대미지 구독자 리스트
        alive = true;                                               // 플레이어 생존 여부
    }

    /// <summary>
    /// 최대 체력 프로퍼티
    /// </summary>
    public float MAXHP
    {
        get { return maxHP; }
        set
        {
            if (value < 1f)
                maxHP = 1f;         // 최소 1 이상의 값을 갖는다
            else
                maxHP = value;
            OnHPEvent?.Invoke();    // 체력 이벤트 발생
        }
    }

    /// <summary>
    /// 현재 체력 프로퍼티
    /// </summary>
    public float NOWHP
    {
        get { return nowHP; }
        set
        {
            // 값이 0 이하이고 현재 살아있다면
            if (value <= 0f && alive)
            {
                alive = false;          // 사망 설정
                OnHPEvent?.Invoke();    // 체력 이벤트 발생
                playerSystem.Die();     // 사망 메소드 실행
            }
            else
            {
                if(value >= nowHP)      // 현재 체력이 늘어난다면 회복량에 추가
                    GameManager.Data.NowRecords["Heal"] += value - nowHP;
                if(value > MAXHP)       // 늘어난 값이 최대보다 크다면 최대 체력까지만
                    nowHP = MAXHP;
                else
                    nowHP = value;
                OnHPEvent?.Invoke();    // 체력 이벤트 발생
            }
        }
    }

    /// <summary>
    /// 경험치 프로퍼티
    /// </summary>
    public float EXP
    {
        get { return exp; }
        set 
        {
            exp = value;
            while (LEVEL != 0 && exp >= LEVEL * 100f) // 레벨 0이 아닌데, 경험치가 레벨 * 100 이상이라면
            {
                exp -= LEVEL * 100f;                  // 경험치를 줄이고
                LEVEL++;                              // 레벨을 증가
            }
            OnEXPEvent?.Invoke();                     // 경험치 이벤트 발생
        }
    }

    /// <summary>
    /// 레벨 프로퍼티
    /// </summary>
    public int LEVEL
    {
        get { return level; }
        set
        {
            level = value;              // 레벨 값을 저장하고
            playerSystem.LevelUp();     // 레벨업 메소드 실행
            OnLevelEvent?.Invoke();     // 레벨 이벤트 발생
        }
    }

    /// <summary>
    /// 이동 속도 프로퍼티
    /// </summary>
    public float MoveSpeed
    {
        // 이동 속도 * 이동속도 보정값을 반환
        get { return status[0] * buffModifier[0];}
        // 이동 속도는 그대로 저장
        set { status[0] = value; }
    }

    /// <summary>
    /// 점프 높이 프로퍼티
    /// </summary>
    public float JumpPower
    {
        // 점프 높이 * 점프 높이 보정값을 반환
        get { return status[1] * buffModifier[1]; }
        // 점프 높이는 그대로 저장
        set { status[1] = value; }
    }

    /// <summary>
    /// 공격 대미지 프로퍼티
    /// </summary>
    public float AttackDamage
    {
        // 공격 대미지 * 공격 대미지 보정값을 반환
        get { return status[2] * buffModifier[2]; }
        // 공격 대미지는 그대로 저장
        set { status[2] = value; }
    }

    /// <summary>
    /// 치명타 확률 프로퍼티
    /// </summary>
    public float CriticalProbability
    {
        // 치명타 확률 * 치명타 확률 보정값을 반환
        get { return status[3] * buffModifier[3]; }
        // 치명타 확률은 그대로 저장
        set { status[3] = value; }
    }

    /// <summary>
    /// 치명타 배율 프로퍼티
    /// </summary>
    public float CriticalRatio
    {
        // 치명타 배율 * 치명타 배율 보정값을 반환
        get { return status[4] * buffModifier[4]; }
        // 치명타 배율은 그대로 저장
        set { status[4] = value; }
    }

    /// <summary>
    /// 플레이어 시간 배속 프로퍼티
    /// </summary>
    public float TimeScale
    { 
        get 
        {
            if (playerTimeScaleMultiplier)  // 배속중이라면 2 반환
                return 2f;
            else                            // 아니라면 1 반환
                return 1f;
        }
        set
        {
            if (value == 2f)                // 2를 입력받으면 배속임
                playerTimeScaleMultiplier = true;
            else                            // 아니라면 배속이 아님
                playerTimeScaleMultiplier = false;
        }
    }

    /// <summary>
    /// 시간 배속 비율의 역수 프로퍼티
    /// </summary>
    public float ReverseTimeScale
    {
        get
        {
            if (playerTimeScaleMultiplier)  // 2의 역수 0.5 반환
                return 0.5f;
            else                            // 1의 역수 1 반환
                return 1f;
        }
    }

    /// <summary>
    /// 플레이어 transform 프로퍼티
    /// </summary>
    public Transform playerTransform
    {
        get { return transform; }   // transform 반환
        set 
        {
            // 위치, 회전, 크기 저장
            transform.SetPositionAndRotation(value.position, value.rotation);
            transform.localScale = value.localScale;
        }
    }

    /// <summary>
    /// 재화 프로퍼티
    /// </summary>
    public int Coin
    {
        get { return coin; }    // 재화 반환
        set 
        { 
            if(value > coin)            // 재화가 늘어났다면 획득 재화 추가
                GameManager.Data.NowRecords["Money"] += (value - coin);
            else if(value < coin)       // 줄어들었다면 소모 재화 추가
                GameManager.Data.NowRecords["Cost"] += (coin - value);
            coin = value;               // 재화를 저장하고
            OnCoinEvent?.Invoke(coin);  // 재화 이벤트 발동
        }
    }
}
