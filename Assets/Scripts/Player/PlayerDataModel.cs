using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerDataModel : MonoBehaviour
{
    public Hero hero;
    List<Hero> heroList;
    public Animator animator;
    public Rigidbody rb;
    [SerializeField][Range(0, 2)] int heroNum;

    void Awake()
    {
        Initailze();
    }

    /// <summary>
    /// 캐릭터 선택
    /// </summary>
    /// <param name="num"></param>
    /// <returns>선택 성공 여부</returns>
    public bool SelectHero(int num)
    {
        if(num >= 0 && num < heroList.Count)
        {
            foreach (var hero in heroList)
                hero.gameObject.SetActive(false);
            heroList[num].gameObject.SetActive(true);
            hero = heroList[num];
            animator = hero.animator;
            hero.playerDataModel = this;
            animator.SetInteger("Hero", num);
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

    public float hitPoint, exp;
    public int level;

    public float moveSpeed, highSpeed, jumpPower;
    public float attackCoolTime, skillCoolTime;
    public int jumpLimit, jumpCount;
    public bool isJump, attackCooldown;

    public Vector3 moveDir, prevDir;
    public Dictionary<Vector3, bool> climable;
    public float climbCheckLowHeight, climbCheckHighHeight, climbCheckLength;

    public bool[] coolChecks = new bool[4];

    void Initailze()
    {
        heroList = new List<Hero>();
        heroList.AddRange(GetComponentsInChildren<Hero>());
        foreach (var hero in heroList)
            hero.gameObject.SetActive(false);

        rb = GetComponent<Rigidbody>();
        climable = new Dictionary<Vector3, bool>();

        coolChecks = new bool[4];
        for (int i = 0; i < coolChecks.Length; i++)
            coolChecks[i] = true;

        hitPoint = 100f;
        level = 1;
        exp = 0f;

        SelectHero(heroNum);

        DontDestroyOnLoad(gameObject);

        GameManager.Data.Player = gameObject;
    }
}
