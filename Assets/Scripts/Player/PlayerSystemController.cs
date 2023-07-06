using System.Collections;
using UnityEngine;

public class PlayerSystemController : MonoBehaviour, IHitable, IDamagePublisher
{
    PlayerDataModel playerDataModel;
    ParticleSystem levelupParticle;

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
        levelupParticle = GameManager.Resource.Load<ParticleSystem>("Particle/LevelUp");
    }

    /// <summary>
    /// 캐릭터 선택
    /// </summary>
    /// <param name="num"></param>
    /// <returns>선택 성공 여부</returns>
    public bool SelectHero(int num)
    {
        if (num >= 0 && num < playerDataModel.heroList.Count)
        {
            for(int i = 0; i < playerDataModel.heroList.Count; i++)
                playerDataModel.heroList[i].gameObject.SetActive(false);
            playerDataModel.heroNum = num;
            playerDataModel.heroList[num].gameObject.SetActive(true);
            playerDataModel.hero = playerDataModel.heroList[num];
            playerDataModel.animator = playerDataModel.hero.animator;
            playerDataModel.hero.playerDataModel = playerDataModel;
            playerDataModel.animator.SetInteger("Hero", num);
            playerDataModel.playerAction.AttackTransform = playerDataModel.hero.attackTransform;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 캐릭터 파괴
    /// </summary>
    public void DestroyCharacter()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    /// <summary>
    /// 사용시 곱할 값, 취소시 곱한 값의 역수 입력
    /// 0: 이동속도, 1: 점프높이, 2: 공격력, 3: 치명타 확률, 4: 치명타 배율
    /// </summary>
    public void Buff(int num, float value)
    {
        playerDataModel.buffModifier[num] *= value;
    }

    public void Hit(float damage, float Time)
    {
        if (!playerDataModel.dodgeDamage)
        {
            StartCoroutine(HitRoutine(damage, Time));
        }
    }

    public IEnumerator HitRoutine(float damage, float time)
    {
        float nowTime = 0f;
        while (nowTime <= time)
        {
            float _damage = DamageOccurrence(damage);
            playerDataModel.NOWHP -= _damage;
            GameManager.Data.NowRecords["Hit"] += _damage;
            nowTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void LevelUp()
    {
        playerDataModel.MAXHP *= 1.1f;
        playerDataModel.NOWHP = playerDataModel.MAXHP;
        playerDataModel.AttackDamage *= 1.1f;

        GameManager.Resource.Instantiate(levelupParticle, playerDataModel.playerTransform.position, Quaternion.identity, true);
    }

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    public void AddDamageSubscriber(IDamageSubscriber _subscriber)
    {
        playerDataModel.damageSubscribers.Add(_subscriber);
    }

    public void RemoveDamageSubscriber(IDamageSubscriber _subscriber)
    {
        for (int i = playerDataModel.damageSubscribers.Count - 1; i >= 0; i--)
        {
            if (playerDataModel.damageSubscribers[i].Equals(_subscriber))
            {
                playerDataModel.damageSubscribers.RemoveAt(i);
            }
        }
    }

    public float DamageOccurrence(float _damage)
    {
        float damage = _damage;
        for (int i = 0; i < playerDataModel.damageSubscribers.Count; i++)
        {
            damage = playerDataModel.damageSubscribers[i].ModifiyDamage(damage);
        }
        return damage;
    }

    /// <summary>
    /// 사망시 데이터 정산 및 UI 생성에 대한 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DieRoutine()
    {
        GameManager.Data.RecordTime = false;
        GameManager.UI.ShowPopupUI<PopUpUI>("UI/RecordUI");

        bool achive = false;

        yield return null;
        GameManager.Data.AddAchives("StageCount", (int)GameManager.Data.NowRecords["Stage"]);
        if (GameManager.Data.NowRecords["Stage"] > GameManager.Data.Records["StageCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"탐사 기록 갱신!\n{GameManager.Data.Records["StageCount"]} => {(int)GameManager.Data.NowRecords["Stage"]}");
            GameManager.Data.SetRecords("StageCount", (int)GameManager.Data.NowRecords["Stage"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("TimeCount", (int)GameManager.Data.NowRecords["Time"]);
        if (GameManager.Data.NowRecords["Time"] > GameManager.Data.Records["TimeCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"생존시간 기록 갱신!\n{GameManager.Data.Records["TimeCount"]} => {(int)GameManager.Data.NowRecords["Time"]}");
            GameManager.Data.SetRecords("TimeCount", (int)GameManager.Data.NowRecords["Time"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("KillCount", (int)GameManager.Data.NowRecords["Kill"]);
        if (GameManager.Data.NowRecords["Kill"] > GameManager.Data.Records["KillCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"처치 수 기록 갱신!\n{GameManager.Data.Records["KillCount"]} => {(int)GameManager.Data.NowRecords["Kill"]}");
            GameManager.Data.SetRecords("KillCount", (int)GameManager.Data.NowRecords["Kill"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("DamageCount", (int)GameManager.Data.NowRecords["Damage"]);
        if (GameManager.Data.NowRecords["Damage"] > GameManager.Data.Records["DamageCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"가한 피해량 기록 갱신!\n{GameManager.Data.Records["DamageCount"]} => {(int)GameManager.Data.NowRecords["Damage"]}");
            GameManager.Data.SetRecords("DamageCount", (int)GameManager.Data.NowRecords["Damage"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("HitCount", (int)GameManager.Data.NowRecords["Hit"]);
        if (GameManager.Data.NowRecords["Hit"] > GameManager.Data.Records["HitCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"받은 피해량 기록 갱신!\n{GameManager.Data.Records["HitCount"]} => {GameManager.Data.NowRecords["Hit"]}");
            GameManager.Data.SetRecords("HitCount", (int)GameManager.Data.NowRecords["Hit"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("HealCount", (int)GameManager.Data.NowRecords["Heal"]);
        if (GameManager.Data.NowRecords["Heal"] > GameManager.Data.Records["HealCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"회복량 기록 갱신!\n{GameManager.Data.Records["HealCount"]} => {(int)GameManager.Data.NowRecords["Heal"]}");
            GameManager.Data.SetRecords("HealCount", (int)GameManager.Data.NowRecords["Heal"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("MoneyCount", (int)GameManager.Data.NowRecords["Money"]);
        if (GameManager.Data.NowRecords["Money"] > GameManager.Data.Records["MoneyCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"획득 재화 기록 갱신!\n{GameManager.Data.Records["MoneyCount"]} => {(int)GameManager.Data.NowRecords["Money"]}");
            GameManager.Data.SetRecords("MoneyCount", (int)GameManager.Data.NowRecords["Money"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("CostCount", (int)GameManager.Data.NowRecords["Cost"]);
        if (GameManager.Data.NowRecords["Cost"] > GameManager.Data.Records["CostCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"사용 재화 기록 갱신!\n{GameManager.Data.Records["CostCount"]} => {(int)GameManager.Data.NowRecords["Cost"]}");
            GameManager.Data.SetRecords("CostCount", (int)GameManager.Data.NowRecords["Cost"]);
            achive = true;
        }

        yield return null;
        GameManager.Data.AddAchives("LevelCount", (int)GameManager.Data.Player.LEVEL);
        if (GameManager.Data.Player.LEVEL > GameManager.Data.Records["LevelCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"레벨 기록 갱신!\n{GameManager.Data.Records["LevelCount"]} => {GameManager.Data.Player.LEVEL}");
            GameManager.Data.SetRecords("LevelCount", GameManager.Data.Player.LEVEL);
            achive = true;
        }

        yield return null;
        GameManager.Data.SaveAchivements();
        if (achive)
            GameManager.Data.SaveRecords();
    }
}
