using System.Collections;

/// <summary>
/// 피해를 받을 수 있는 인터페이스
/// </summary>
public interface IHitable
{
    /// <summary>
    /// 피해를 받는 메소드
    /// </summary>
    /// <param name="hitDamage">발생 피해</param>
    /// <param name="damageDuration">피해 지속 시간</param>
    public void Hit(float hitDamage, float damageDuration);

    /// <summary>
    /// 피해 발생 열거자
    /// </summary>
    /// <param name="hitDamage">발생 피해</param>
    /// <param name="damageDuration">피해 지속 시간</param>
    /// <returns>0.1초마다 발생</returns>
    IEnumerator HitRoutine(float hitDamage, float damageDuration);

    /// <summary>
    /// 사망 메소드
    /// </summary>
    public void Die();
}
