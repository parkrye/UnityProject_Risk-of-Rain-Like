using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어와 관련된 여러 작업에 대한 스크립트
/// </summary>
public class PlayerSystemController : MonoBehaviour, IHitable, IDamagePublisher
{
    PlayerDataModel playerDataModel;    // 플레이어 데이터 모델
    ParticleSystem levelupParticle;     // 레벨업 효과

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
                playerDataModel.heroList[i].gameObject.SetActive(false);    // 모든 영웅 비활성화
            playerDataModel.heroNum = num;                                  // 영웅 번호 등록
            playerDataModel.heroList[num].gameObject.SetActive(true);       // 선택된 영웅 활성화
            playerDataModel.hero = playerDataModel.heroList[num];           // 선택된 영웅 참조
            playerDataModel.animator = playerDataModel.hero.animator;       // 선택된 영웅의 애니메이터 참조
            playerDataModel.hero.playerDataModel = playerDataModel;         // 선택된 영웅에 데이터 모델 참조
            playerDataModel.playerAction.AttackTransform = playerDataModel.hero.attackTransform;
                                                                            // 선택된 영웅의 공격 지점을 참조
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

    /// <summary>
    /// 피격 메소드
    /// </summary>
    /// <param name="damage">대미지</param>
    /// <param name="Time">지속 시간</param>
    public void Hit(float damage, float Time = 0f)
    {
        if (!playerDataModel.dodgeDamage)
            StartCoroutine(HitRoutine(damage, Time));
    }

    /// <summary>
    /// 대미지 발생 열거자
    /// </summary>
    /// <param name="damage">대미지</param>
    /// <param name="time">지속 시간</param>
    /// <returns>0.1초마다 발생</returns>
    public IEnumerator HitRoutine(float damage, float time)
    {
        float nowTime = 0f;
        while (nowTime <= time)
        {
            float _damage = DamageOccurrence(damage);       // 대미지 발생 이벤트로 감소된 대미지에 대하여
            playerDataModel.NOWHP -= _damage;               // 현재 체력을 줄이고
            GameManager.Data.NowRecords["Hit"] += _damage;  // 현재 피격량을 저장하고
            nowTime += 0.1f;                                // 비교 시간을 증가
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 레벨업 메소드
    /// </summary>
    public void LevelUp()
    {
        playerDataModel.MAXHP *= 1.1f;                  // 최대 체력을 10% 증가
        playerDataModel.NOWHP = playerDataModel.MAXHP;  // 현재 체력을 최대 체력으로
        playerDataModel.AttackDamage *= 1.1f;           // 공격 데미지를 10% 증가

        GameManager.Resource.Instantiate(levelupParticle, playerDataModel.playerTransform.position, Quaternion.identity, transform, true);
                                                        // 레벨업 파티클 발생
    }

    /// <summary>
    /// 사망 메소드
    /// </summary>
    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    /// <summary>
    /// 대미지에 대한 구독자 추가 메소드
    /// </summary>
    /// <param name="_subscriber">추가할 구독자</param>
    public void AddDamageSubscriber(IDamageSubscriber _subscriber)
    {
        playerDataModel.damageSubscribers.Add(_subscriber);
    }

    /// <summary>
    /// 대미지에 대한 구독자 취소 메소드
    /// </summary>
    /// <param name="_subscriber">취소할 구독자</param>
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

    /// <summary>
    /// 대미지 발생에 대한 구독자들의 효과 적용
    /// </summary>
    /// <param name="_damage">발생한 대미지</param>
    /// <returns>변동된 대미지</returns>
    public float DamageOccurrence(float _damage)
    {
        float damage = _damage;
        for (int i = 0; i < playerDataModel.damageSubscribers.Count; i++)
        {
            damage = playerDataModel.damageSubscribers[i].ModifiyDamage(damage);    // 각 구돚가의 대미지 변환 함수를 적용하여 대미지 변환
        }
        return damage;
    }

    /// <summary>
    /// 사망시 데이터 정산 및 UI 생성 열거자
    /// </summary>
    /// <returns></returns>
    IEnumerator DieRoutine()
    {
        GameManager.Data.RecordTime = false;                    // 시간 기록 정지
        GameManager.UI.ShowPopupUI<PopUpUI>("UI/RecordUI");     // 기록 UI 생성

        bool achive = false;

        // 이하 각 기록, 업적에 대한 갱신 및 UI 생성

        // 스테이지
        yield return null;
        GameManager.Data.AddAchives("StageCount", (int)GameManager.Data.NowRecords["Stage"]);
        if (GameManager.Data.NowRecords["Stage"] > GameManager.Data.Records["StageCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"탐사 기록 갱신!\n{GameManager.Data.Records["StageCount"]} => {(int)GameManager.Data.NowRecords["Stage"]}");
            GameManager.Data.SetRecords("StageCount", (int)GameManager.Data.NowRecords["Stage"]);
            achive = true;
        }

        // 시간
        yield return null;
        GameManager.Data.AddAchives("TimeCount", (int)GameManager.Data.NowRecords["Time"]);
        if (GameManager.Data.NowRecords["Time"] > GameManager.Data.Records["TimeCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"생존시간 기록 갱신!\n{GameManager.Data.Records["TimeCount"]} => {(int)GameManager.Data.NowRecords["Time"]}");
            GameManager.Data.SetRecords("TimeCount", (int)GameManager.Data.NowRecords["Time"]);
            achive = true;
        }
        
        // 처치 수
        yield return null;
        GameManager.Data.AddAchives("KillCount", (int)GameManager.Data.NowRecords["Kill"]);
        if (GameManager.Data.NowRecords["Kill"] > GameManager.Data.Records["KillCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"처치 수 기록 갱신!\n{GameManager.Data.Records["KillCount"]} => {(int)GameManager.Data.NowRecords["Kill"]}");
            GameManager.Data.SetRecords("KillCount", (int)GameManager.Data.NowRecords["Kill"]);
            achive = true;
        }

        // 가한 피해량
        yield return null;
        GameManager.Data.AddAchives("DamageCount", (int)GameManager.Data.NowRecords["Damage"]);
        if (GameManager.Data.NowRecords["Damage"] > GameManager.Data.Records["DamageCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"가한 피해량 기록 갱신!\n{GameManager.Data.Records["DamageCount"]} => {(int)GameManager.Data.NowRecords["Damage"]}");
            GameManager.Data.SetRecords("DamageCount", (int)GameManager.Data.NowRecords["Damage"]);
            achive = true;
        }

        // 받은 피해량
        yield return null;
        GameManager.Data.AddAchives("HitCount", (int)GameManager.Data.NowRecords["Hit"]);
        if (GameManager.Data.NowRecords["Hit"] > GameManager.Data.Records["HitCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"받은 피해량 기록 갱신!\n{GameManager.Data.Records["HitCount"]} => {GameManager.Data.NowRecords["Hit"]}");
            GameManager.Data.SetRecords("HitCount", (int)GameManager.Data.NowRecords["Hit"]);
            achive = true;
        }

        // 회복량
        yield return null;
        GameManager.Data.AddAchives("HealCount", (int)GameManager.Data.NowRecords["Heal"]);
        if (GameManager.Data.NowRecords["Heal"] > GameManager.Data.Records["HealCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"회복량 기록 갱신!\n{GameManager.Data.Records["HealCount"]} => {(int)GameManager.Data.NowRecords["Heal"]}");
            GameManager.Data.SetRecords("HealCount", (int)GameManager.Data.NowRecords["Heal"]);
            achive = true;
        }

        // 획득 재화
        yield return null;
        GameManager.Data.AddAchives("MoneyCount", (int)GameManager.Data.NowRecords["Money"]);
        if (GameManager.Data.NowRecords["Money"] > GameManager.Data.Records["MoneyCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"획득 재화 기록 갱신!\n{GameManager.Data.Records["MoneyCount"]} => {(int)GameManager.Data.NowRecords["Money"]}");
            GameManager.Data.SetRecords("MoneyCount", (int)GameManager.Data.NowRecords["Money"]);
            achive = true;
        }

        // 소모 재화
        yield return null;
        GameManager.Data.AddAchives("CostCount", (int)GameManager.Data.NowRecords["Cost"]);
        if (GameManager.Data.NowRecords["Cost"] > GameManager.Data.Records["CostCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"사용 재화 기록 갱신!\n{GameManager.Data.Records["CostCount"]} => {(int)GameManager.Data.NowRecords["Cost"]}");
            GameManager.Data.SetRecords("CostCount", (int)GameManager.Data.NowRecords["Cost"]);
            achive = true;
        }

        // 레벨
        yield return null;
        GameManager.Data.AddAchives("LevelCount", (int)GameManager.Data.Player.LEVEL);
        if (GameManager.Data.Player.LEVEL > GameManager.Data.Records["LevelCount"])
        {
            NotifyUI notifyUI = GameManager.UI.ShowPopupUI<NotifyUI>("UI/NotifyUI");
            notifyUI.SetText($"레벨 기록 갱신!\n{GameManager.Data.Records["LevelCount"]} => {GameManager.Data.Player.LEVEL}");
            GameManager.Data.SetRecords("LevelCount", GameManager.Data.Player.LEVEL);
            achive = true;
        }

        // 업적을 저장하고 기록 갱신시 기록 저장
        yield return null;
        GameManager.Data.SaveAchivements();
        if (achive)
            GameManager.Data.SaveRecords();
    }
}
