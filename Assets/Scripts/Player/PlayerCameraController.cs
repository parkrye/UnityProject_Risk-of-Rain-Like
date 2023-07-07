using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 카메라 이동에 대한 스크립트
/// </summary>
public class PlayerCameraController : MonoBehaviour
{
    PlayerDataModel playerDataModel;                            // 플레이어 데이터 모델

    [SerializeField] Vector2 pointerPos;                        // 포인터 위치
    [SerializeField] Vector3 cameraOffset, downViewOffset, upViewOffset, defaultCameraOffset, closeCameraOffset;
                                                                // 카메라 조정치, 하향 조정치, 상향 조정치, 기본 조정치, 근접 조정치
    [SerializeField] float xRotation;                           // x 회전각
    [SerializeField] CinemachineVirtualCamera virtualCamera;    // 메인 카메라
    public Camera minimapCamera;                                // 미니맵 카메라

    public Transform lookFromTransform, lookAtTransform;        // 카메라 초점 시작 위치, 초점 끝 위치

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;               // 커서를 숨기고
        playerDataModel = GetComponent<PlayerDataModel>();      // 데이터 모델 참조
        defaultCameraOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
                                                                // 초기 조정치 저장
        cameraOffset = defaultCameraOffset;                     // 카메라 조정치를 초기 조정치로
        closeCameraOffset = cameraOffset * 0.1f;                // 근접 조정치는 초기 조정치의 10분의 1
        downViewOffset = new Vector3(0f, 5f, -2.5f);            // 하향 조정치
        upViewOffset = new Vector3(0f, 0f, -0.5f);              // 상향 조정치
    }

    void Update()
    {
        // ESC UI 없는 상황이라면
        if (!playerDataModel.onESC)
        {
            // 초점 시작으로부터 카메라 방향으로
            Ray ray = new Ray(lookFromTransform.position, (virtualCamera.transform.position - lookFromTransform.position));
            // 벽이 존재한다면
            if (Physics.Raycast(ray, Vector3.Distance(virtualCamera.transform.position, lookFromTransform.position), LayerMask.GetMask("Ground")))
            {
                // 카메라 조정치를 근접값에 가깝게 이동시킨다
                cameraOffset = Vector3.Lerp(cameraOffset, closeCameraOffset, Time.deltaTime * playerDataModel.TimeScale);
            }
            // 없다면
            else
            {
                // 카메라 조정치를 기본값에 가깝게 이동시킨다
                cameraOffset = Vector3.Lerp(cameraOffset, defaultCameraOffset, Time.deltaTime * playerDataModel.TimeScale);
            }
        }
    }

    void LateUpdate()
    {
        // ESC UI 없는 상황이라면
        if (!playerDataModel.onESC)
        {
            // y축 회전에 대한 캐릭터 회전
            transform.localEulerAngles += new Vector3(0f, pointerPos.x * playerDataModel.mouseSensivity * Time.deltaTime, 0f);

            // x축 회전에 대한 값 조정
            xRotation -= pointerPos.y * playerDataModel.mouseSensivity * 3f * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            // x축 회전에 대한 카메라 초점 시작점 회전
            lookFromTransform.localEulerAngles = new Vector3(xRotation, 0f, 0f);

            if (xRotation < 0f) // x축 회전값이 0보다 작다면(위를 바라본다면) 카메라는 상향한다
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cameraOffset, upViewOffset, (-xRotation * 0.0125f));
            else                // 아니라면(아래를 바라본다면) 카메라는 하향한다
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cameraOffset, downViewOffset, (xRotation * 0.0125f));
        }
    }

    /// <summary>
    /// 마우스 입력에 대한 메소드
    /// </summary>
    /// <param name="inputValue"></param>
    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
