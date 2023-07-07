/// <summary>
/// 대미지 관여 구독자
/// </summary>
public interface IDamageSubscriber
{
    /// <summary>
    /// 발생 대미지를 변동하여 반환한다
    /// </summary>
    /// <param name="originDamage">입력 대미지</param>
    /// <returns>변동 대미지</returns>
    public float ModifiyDamage(float originDamage);
}
