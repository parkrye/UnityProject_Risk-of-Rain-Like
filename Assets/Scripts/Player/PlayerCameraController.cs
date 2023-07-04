using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    [SerializeField] Vector2 pointerPos;
    [SerializeField] Vector3 cameraOffset, downViewOffset, upViewOffset, defaultCameraOffset, closeCameraOffset;
    [SerializeField] float xRotation;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    public Camera minimapCamera;

    public Transform lookFromTransform, lookAtTransform;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerDataModel = GetComponent<PlayerDataModel>();
        defaultCameraOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        cameraOffset = defaultCameraOffset;
        closeCameraOffset = cameraOffset / 10f;
        downViewOffset = new Vector3(0f, 5f, -2.5f);
        upViewOffset = new Vector3(0f, 0f, -0.5f);
    }

    void Update()
    {
        if (!playerDataModel.onESC)
        {
            Ray ray = new Ray(lookFromTransform.position, (virtualCamera.transform.position - lookFromTransform.position));
            if (Physics.Raycast(ray, Vector3.Distance(virtualCamera.transform.position, lookFromTransform.position), LayerMask.GetMask("Ground")))
            {
                cameraOffset = Vector3.Lerp(cameraOffset, closeCameraOffset, Time.deltaTime * playerDataModel.TimeScale);
            }
            else
            {
                cameraOffset = Vector3.Lerp(cameraOffset, defaultCameraOffset, Time.deltaTime * playerDataModel.TimeScale);
            }
        }
    }

    void LateUpdate()
    {
        if (!playerDataModel.onESC)
        {
            transform.localEulerAngles += new Vector3(0f, pointerPos.x * playerDataModel.mouseSensivity * Time.deltaTime, 0f);

            xRotation -= pointerPos.y * playerDataModel.mouseSensivity * 0.3f * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            lookFromTransform.localEulerAngles = new Vector3(xRotation, 0f, 0f);

            if (xRotation < 0f)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cameraOffset, upViewOffset, (-xRotation / 80f));
            }
            else
            {
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cameraOffset, downViewOffset, (xRotation / 80f));
            }
        }
    }

    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
