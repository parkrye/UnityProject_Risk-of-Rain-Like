using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField][Range(0, 2)] int heroNum;

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
            if (value < 0f)
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

    public float moveSpeed, jumpPower, coolTime, climbPower, attackDamage, armorPoint;
    public int jumpLimit, jumpCount;
    public bool attackCooldown, controlleable;

    public float climbCheckLowHeight, climbCheckHighHeight, climbCheckLength;

    public float mouseSensivity;

    public bool[] coolChecks = new bool[4];

    void Initailze()
    {
        heroList = new List<Hero>();
        heroList.AddRange(GetComponentsInChildren<Hero>());
        foreach (var hero in heroList)
            hero.gameObject.SetActive(false);

        rb = GetComponent<Rigidbody>();
        playerAction = GetComponent<PlayerActionController>();
        playerMovement = GetComponent<PlayerMovementController>();
        playerCamera = GetComponent<PlayerCameraController>();
        inventory = GetComponent<Inventory>();

        coolChecks = new bool[4];
        for (int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;

        SelectHero(heroNum);

        DontDestroyOnLoad(gameObject);

        GameManager.Data.Player = this;
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
        NOWHP -= damage * armorPoint;
    }

    public void Die()
    {
        Debug.Log("you died");
    }
}
