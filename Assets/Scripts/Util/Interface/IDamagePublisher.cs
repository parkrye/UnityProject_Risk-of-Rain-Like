/// <summary>
/// 대미지 변동을 주관하는 인터페이스
/// </summary>
public interface IDamagePublisher
{
    /// <summary>
    /// 대미지 관여 구독자를 추가한다
    /// </summary>
    /// <param name="_subscriber">대미지 관여 구독자</param>
    public void AddDamageSubscriber(IDamageSubscriber _subscriber);

    /// <summary>
    /// 대미지 관여 구독자를 해지한다
    /// </summary>
    /// <param name="_subscriber">해지할 대미지 관여 구독자</param>
    public void RemoveDamageSubscriber(IDamageSubscriber _subscriber);
    
    /// <summary>
    /// 대미지 발생시 관여 구독자의 메소드를 실행시킨다
    /// </summary>
    /// <param name="_damage">발생한 대미지</param>
    /// <returns>변동된 대미지</returns>
    public float DamageOccurrence(float _damage);
}
