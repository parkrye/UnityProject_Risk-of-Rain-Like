using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

public class PlayerDataModel : MonoBehaviour, IHitable
{
    public Hero hero;
    List<Hero> heroList;
    public Animator animator;
    public Rigidbody rb;
    public PlayerActionController playerAction;
    public PlayerMovementController playerMovement;
    public PlayerCameraController playerCamera;
    public Inventory inventory;
    [SerializeField][Range(0, 2)] public int heroNum;

    void Awake()
    {
        Initailze();
    }

    [SerializeField] float maxHP, nowHP, exp;
    [SerializeField] int level;
    public float MAXHP
    {
        get { return maxHP; }
        set
        {
            if (value < 0)
                maxHP = 1f;
            else
                maxHP = value;
            HPEvent?.Invoke();
        }
    }

    public float NOWHP
    {
        get { return nowHP; }
        set
        {
            if (value <= 0f)
            {
                Die();
                HPEvent?.Invoke();
            }
            else
            {
                if(value > MAXHP)
                    nowHP = MAXHP;
                else
                    nowHP = value;
                HPEvent?.Invoke();
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
            EXPEvent?.Invoke();
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
            LevelEvent?.Invoke();
        }
    }

    [SerializeField] float[] status;
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
    public float ArmorPoint
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
    public float coolTime;
    public int jumpLimit, jumpCount;
    public bool attackCooldown, controllable, dodgeDamage;

    public float mouseSensivity;
    [SerializeField] float playerTimeScale;
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

    public float[] buffModifier;

    /// <summary>
    /// 사용시 곱할 값, 취소시 곱한 값의 역수 입력
    /// 0: 이동속도, 1: 점프높이, 2: 공격력, 3: 방어력
    /// </summary>
    public void Buff(int num, float value)
    {
        switch (num)
        {
            case 0:
                buffModifier[num] *= value;
                break;
            case 1:
                buffModifier[num] *= value;
                break;
            case 2:
                buffModifier[num] *= value;
                break;
            case 3:
                buffModifier[num] *= value;
                break;
        }
    }

    public bool[] coolChecks = new bool[4];

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
        inventory = GetComponent<Inventory>();

        TimeScale = 1f;
        status = new float[4] { 5f, 10f, 5f, 1f };
        buffModifier = new float[4] { 1f, 1f, 1f, 1f };

        coolChecks = new bool[4];
        for (int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 캐릭터 선택
    /// </summary>
    /// <param name="num"></param>
    /// <returns>선택 성공 여부</returns>
    public bool SelectHero(int num)
    {
        if (num >= 0 && num < heroList.Count)
        {
            foreach (var hero in heroList)
                hero.gameObject.SetActive(false);
            heroList[num].gameObject.SetActive(true);
            hero = heroList[num];
            animator = hero.animator;
            hero.playerDataModel = this;
            animator.SetInteger("Hero", num);
            playerAction.AttackTransform = hero.attackTransform;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 캐릭터 파괴
    /// </summary>
    public void DestroyCharacter()
    {
        Destroy(gameObject);
    }

    public UnityEvent LevelEvent, HPEvent, EXPEvent;

    public void Hit(float damage)
    {
        if(!dodgeDamage)
            NOWHP -= damage * ArmorPoint;
    }

    public void Die()
    {
        Debug.Log("you died");
        GameManager.Data.RecordTime = false;
    }
}
