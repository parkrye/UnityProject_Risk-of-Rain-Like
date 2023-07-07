using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 스킬 추상 스크립트. ScriptableObject
/// </summary>
public abstract class Skill : ScriptableObject
{
    public Hero hero;                   // 이 스킬을 소유한 영웅
    public string SkillName;            // 스킬의 이름
    public string SkillDesc;            // 스킬의 설명
    public Sprite SkillIcon;            // 스킬의 아이콘

    public float coolTime, modifier;    // 스킬 쿨타임, 스킬 조정치
    bool coolCheck;                     // 쿨타임 체크
    protected string[] actionKeys = {"Action1A", "Action2A", "Action3A", "Action4A",
                                    "Action1B", "Action2B", "Action3B", "Action4B",
                                    "Action1C", "Action2C", "Action3C", "Action4C"};
                                        // 애니메이션 트리거 키
    [SerializeField] protected int actionNum;
                                        // 애니메이션 트리거 키 번호

    /// <summary>
    /// 쿨타임 프로퍼티
    /// </summary>
    public bool CoolCheck
    {
        get { return coolCheck; }           // 쿨타임 반환
        set
        {
            coolCheck = value;              // 쿨타임 체크
            CoolEvent?.Invoke(CoolCheck);   // 쿨타임 이벤트 발동
        }
    }
    public UnityEvent<bool> CoolEvent;      // 쿨타임 이벤트

    private void OnEnable()
    {
        CoolCheck = true;
    }

    /// <summary>
    /// 스킬 사용 메소드
    /// </summary>
    /// <param name="isPressed">버튼 클릭 정보</param>
    /// <param name="param">추가적으로 사용되는 데이터</param>
    /// <returns>스킬 성공 여부</returns>
    public abstract bool Active(bool isPressed, params float[] param);

    /// <summary>
    /// 쿨타임 측정 열거자
    /// </summary>
    /// <param name="coolModifier">쿨타임 관여값</param>
    /// <returns></returns>
    public IEnumerator CoolTime(float coolModifier)
    {
        while (CoolCheck)       // 쿨타임이 false가 될때까지 일단 대기
            yield return null;
        yield return new WaitForSeconds(coolTime * coolModifier * hero.playerDataModel.ReverseTimeScale);
                                // 스킬 쿨타임 * 쿨타임 관여값 ( * 시간 배속의 경우 2배로 빨리 쿨이 돌아야하므로 역수)
        CoolCheck = true;       // 쿨타임 true
    }
}
