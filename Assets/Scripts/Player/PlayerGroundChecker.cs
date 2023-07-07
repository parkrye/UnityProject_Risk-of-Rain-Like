using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어의 지면 접촉에 대한 스크립트
/// </summary>
public class PlayerGroundChecker : MonoBehaviour
{
    /// <summary>
    /// 지면 접촉 여부
    /// 직전(0.5f)에 점프하지 않았고 닿아있는 지면이 1개 이상이라면 true, 아니라면 false
    /// </summary>
    public bool IsGround
    {
        get 
        { 
            if (ready && groundCounter > 0) 
                return true;
            return false;
        }
    }
    int groundCounter;
    bool ready;

    void Awake()
    {
        groundCounter = 0;
        ready = true;
    }

    /// <summary>
    /// 지면 접촉시 카운터를 1 늘린다
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            groundCounter++;
        }
    }

    /// <summary>
    /// 지면 탈출시 카운터를 1 줄인다
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if((1 << other.gameObject.layer) == LayerMask.GetMask("Ground"))
        {
            groundCounter--;
        }
    }

    /// <summary>
    /// 점프 시 일시적으로 지면 검사를 거짓으로 하는 메소드
    ///  임의의 경우 캐릭터의 위치와 강체의 속도의 y값에서 3~4 프레임 단위로 이상값(마이너스 값으로 변함)이 발생
    ///  이 문제로 플레이어의 점프 초기화 시점이 의도치 않게 발생하여, 지면에서 점프한 직후 지면 탐색을 무조건 false로 만들어주는 메소드로 임시 해결
    /// </summary>
    public void JumpReady()
    {
        StartCoroutine(JumpReadyRoutine());
    }

    IEnumerator JumpReadyRoutine()
    {
        ready = false;
        yield return new WaitForSeconds(0.5f);
        ready = true;
    }
}
