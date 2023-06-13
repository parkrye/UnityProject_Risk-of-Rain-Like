using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel : MonoBehaviour
{
    public Hero hero;
    List<Hero> heroList;
    public Animator animator;
    public Rigidbody rb;
    public PlayerActionController playerAction;
    public PlayerMovementController playerMovement;
    public PlayerCameraController playerCamera;
    [SerializeField][Range(0, 2)] int heroNum;

    void Awake()
    {
        Initailze();
    }

    public float maxHP, nowHP, exp;
    public int level;

    public float moveSpeed, highSpeed, jumpPower, coolTime, climbPower;
    public int jumpLimit, jumpCount;
    public bool attackCooldown, controlleable;

    public float climbCheckLowHeight, climbCheckHighHeight, climbCheckLength;

    public float turnSpeed, turnSensivity;

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

        coolChecks = new bool[4];
        for (int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;

        maxHP = 100f;
        level = 1;
        exp = 0f;
        controlleable = true;

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

    public void AddEventListener(UnityEvent target, UnityAction action)
    {
        target.AddListener(action);
    }

    void LevelUP()
    {
        exp -= level * 100;
        level++;
        LevelEvent?.Invoke();
    }

    public void ModifyHP(float _modifier)
    {
        if(_modifier < 0f)
        {
            nowHP += _modifier;
            if(nowHP <= 0f)
            {
                nowHP = 0f;
            }
            HPEvent?.Invoke();
        }
        else
        {
            nowHP += _modifier;
            if(nowHP > maxHP)
            {
                HPEvent?.Invoke();
            }
        }
    }

    public void ModifyEXP(float _modifier)
    {
        if (_modifier < 0f)
        {
            exp += _modifier;
            if (exp < 0f)
                exp = 0f;
        }
        else
        {
            exp += _modifier;
            while(exp >= level * 100f)
            {
                LevelUP();
            }
        }
    }
}
