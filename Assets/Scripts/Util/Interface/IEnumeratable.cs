using System.Collections;

/// <summary>
/// 열거자 보유 인터페이스
/// Monobehavior를 상속받지 못한 스크립트에게 열거자가 필요한 경우, 외부에서 스크립트의 열거자 보유 여부를 파악하고 대신 실행시켜주기 위함
/// </summary>
public interface IEnumeratable
{
    /// <summary>
    /// 외부에서 실행시켜줄 수 있는 열거자
    /// </summary>
    /// <returns></returns>
    public IEnumerator Enumerator();
}
