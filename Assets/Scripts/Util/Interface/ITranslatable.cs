using UnityEngine;

/// <summary>
/// 특수한 이동에 대한 인터페이스
/// 경우에 따라 매 업데이트마다 SphereCast를 사용할 수 있으므로 부하가 생길 수 있음을 유의
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
