using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    [SerializeField] Vector2 pointerPos;
    [SerializeField] Vector3 cameraOffset, downViewOffset, upViewOffset;
    [SerializeField] float xRotation;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public Transform lookFromTransform, lookAtTransform;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerDataModel = GetComponent<PlayerDataModel>();
        cameraOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        downViewOffset = new Vector3 (0f, 10f, -5f);
        upViewOffset = new Vector3 (0f, 0f, -0.5f);
    }

    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, pointerPos.x * playerDataModel.mouseSensivity * Time.deltaTime, 0f);

        xRotation -= pointerPos.y * Mathf.Sqrt(playerDataModel.mouseSensivity) * 10f * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        lookFromTransform.localEulerAngles = new Vector3(xRotation, 0f, 0f);

        if(xRotation < 0f)
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cameraOffset, upViewOffset, -xRotation / 80f);
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cameraOffset, downViewOffset, xRotation / 80f);
        }
    }

    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
