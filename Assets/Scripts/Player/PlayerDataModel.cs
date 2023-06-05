using System.Collections.Generic;
using UnityEngine;

public class PlayerDataModel : MonoBehaviour
{
    public Hero hero;
    List<Hero> heroList;
    public Animator animator;
    public Rigidbody rb;
    [SerializeField][Range(0, 2)] int heroNum;

    void Awake()
    {
        heroList = new List<Hero>();
        heroList.AddRange(GetComponentsInChildren<Hero>());
        foreach(var hero in heroList)
            hero.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();

        DontDestroyOnLoad(gameObject);

        SelectHero(heroNum);
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
            animator = hero.Animator;
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

    public float hitPoint;
    public float moveSpeed, jumpPower;
    public float attackCoolTime, skillCoolTime;
    public int jumpLimit, jumpCount;
    public bool isJump, attackCooldown;
}
