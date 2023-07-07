using System.Collections;
using UnityEngine;

/// <summary>
/// 제어 효과 인터페이스
/// </summary>
public interface IMezable
{
    /// <summary>
    /// 기절
    /// </summary>
    /// <param name="stunDuration">기절 시간</param>
    public void Stuned(float stunDuration);

    /// <summary>
    /// 기절 열거자
    /// </summary>
    /// <param name="stunDuration">기절 시간</param>
    /// <returns></returns>
    IEnumerator StunRoutine(float stunDuration);

    /// <summary>
    /// 감속
    /// </summary>
    /// <param name="slowDuration">감속 시간</param>
    /// <param name="slowModifier">감속 배율</param>
    public void Slowed(float slowDuration, float slowModifier);

    /// <summary>
    /// 감속 열거자
    /// </summary>
    /// <param name="slowDuration">감속 시간</param>
    /// <param name="slowModifier">감속 배율</param>
    /// <returns></returns>
    IEnumerator SlowRoutine(float slowDuration, float slowModifier);

    /// <summary>
    /// 밀림
    /// </summary>
    /// <param name="knockBackDistance">밀림 거리</param>
    /// <param name="backFromTransform">민 위치</param>
    public void KnockBack(float knockBackDistance, Transform backFromTransform);

    /// <summary>
    /// 밀림 열거자
    /// </summary>
    /// <param name="knockBackDistance">밀림 거리</param>
    /// <param name="backFromTransform">민 위치</param>
    /// <returns></returns>
    IEnumerator KnockBackRoutine(float knockBackDistance, Transform backFromTransform);
}
