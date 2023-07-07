using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 캐릭터의 행동 및 상호작용에 대한 스크립트
/// </summary>
public class PlayerActionController : MonoBehaviour
{
    PlayerDataModel playerDataModel;                // 데이터모델

    public Transform AttackTransform;               // 공격 위치
    public Transform lookAtTransform, lookFromTransform, interactTransform, closeAttackTransform;
                                                    // 카메라 초점 끝 위치, 카메라 초점 시작 위치, 상호작용 위치, 근접공격 위치
    public float closeAttackRange, interactRange;   // 근접공격 사거리, 상호작용 사거리
    float cosResult;                                // 전방 확인을 위한 코사인 결과값
    ESCUI escUI;                                    // ESC 입력시 UI

    void Awake()
    {
        playerDataModel = GetComponent<PlayerDataModel>();
        cosResult = Trigonometrics.Cos(60f);
    }

    /// <summary>
    /// 주무기 공격에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnAction1(InputValue inputValue)
    {
        if(playerDataModel.hero.Action(0, inputValue.isPressed))
        {

        }
    }

    /// <summary>
    /// 보조무기 공격에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnAction2(InputValue inputValue)
    {
        if (playerDataModel.hero.Action(1, inputValue.isPressed))
        {

        }
    }

    /// <summary>
    /// 보조스킬 사용에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnAction3(InputValue inputValue)
    {
        if (playerDataModel.hero.Action(2, inputValue.isPressed))
        {

        }
    }

    /// <summary>
    /// 주요스킬 사용에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnAction4(InputValue inputValue)
    {
        if (playerDataModel.hero.Action(3, inputValue.isPressed))
        {

        }
    }

    /// <summary>
    /// 상호작용에 대한 메소드
    /// </summary>
    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(interactTransform.position, interactRange);    // 상호작용 지점으로부터 상호작용 거리 내의 오브젝트들에 대하여
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0f;                                                                      // 높이를 무시한 플레이어 위치와
        Vector3 playerLook = new Vector3(transform.forward.x, 0f, transform.forward.z);             // 높이를 무시한 플레이어 시선 방향을 이용하여
        for(int i = 0; i < colliders.Length; i++)
        {
            IInteractable interactable = colliders[i].GetComponent<IInteractable>();                // 만약 상호작용 가능한 오브젝트이고
            if (interactable is null)
                continue;
            Vector3 colliderPosition = colliders[i].transform.position;
            colliderPosition.y = 0f;                                                                // 높이를 무시한 오브젝트의 위치를 비교하여
            Vector3 dirTarget = (colliderPosition - playerPosition).normalized;
            if (Vector3.Dot(playerLook, dirTarget) < cosResult)
                continue;

            interactable?.Interact();                                                               // 플레이어의 전방에 있다면 상호작용한다
        }
    }

    /// <summary>
    /// 상호작용 입력에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnInteract(InputValue inputValue)
    {
        Interact();
    }

    /// <summary>
    /// ESC 입력에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnESC(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            // 현재 레벨 씬이고, 플레이 중인 경우
            if (GameManager.Scene.CurScene.name.StartsWith("LevelScene") && GameManager.Scene.ReadyToPlay)
            {
                // ESC UI가 없다면 ESC UI 생성
                if (!playerDataModel.onESC)
                {
                    escUI = GameManager.UI.ShowPopupUI<ESCUI>("UI/ESCUI");
                }
                // 있다면 ESC UI 삭제
                else
                {
                    escUI?.CloseUI();
                }
            }
        }
    }

    /// <summary>
    /// Tab 입력에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnTab(InputValue inputValue)
    {
        // 누를 때 마우스를 보이고
        if (inputValue.isPressed)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        // 뗄 때 마우스를 숨긴다
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// 기즈모 출력 메소드
    /// 그때그때 필요한 기즈모 작성 후 테스트
    /// </summary>
    void OnDrawGizmos()
    {
        if (playerDataModel.onGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(closeAttackTransform.position, closeAttackRange);
        }
    }
}
