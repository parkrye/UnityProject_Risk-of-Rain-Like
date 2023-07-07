using UnityEngine;

/// <summary>
/// 특수한 이동에 대한 인터페이스
/// GroundChecker를 이용하여 이동 후 GroundChecker로 지면에 접촉할 경우 이동을 취소하도록 구현
/// </summary>
public interface ITranslatable
{
    /// <summary>
    /// 물체를 서서히 이동시키는 함수
    /// 만약 이동 경로에 벽이 있다면 이동하지 않는다
    /// </summary>
    /// <param name="dir">이동 방향</param>
    /// <param name="distance">이동 거리</param>
    /// <returns>이동 성공 여부</returns>
    bool TranslateGradually(Vector3 dir, float distance);

    /// <summary>
    /// 물체를 한번에 이동시키는 함수
    /// 만약 이동 위치에 물체가 있다면 조건에 따라 이동시키지 않는다
    /// </summary>
    /// <param name="pos">이동 좌표(월드 좌표)</param>
    /// <param name="ignoreGround">충돌 판정 여부</param>
    /// <returns>이동 성공 여부</returns>
    bool TranslateSuddenly(Vector3 pos, bool ignoreGround = true);
}
