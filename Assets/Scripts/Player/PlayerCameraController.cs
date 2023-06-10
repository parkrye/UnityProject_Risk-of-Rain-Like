using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    PlayerDataModel playerDataModel;

    [SerializeField] Vector2 pointerPos;
    [SerializeField] float lookX;
    [SerializeField] Transform lookAtTransform;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerDataModel = GetComponent<PlayerDataModel>();
    }

    void Update()
    {
        if(Mathf.Abs(pointerPos.x) > playerDataModel.turnSensivity * 2)
        {
            transform.localEulerAngles += new Vector3(0f, Mathf.Clamp(pointerPos.x, -1f, 1f) * playerDataModel.turnSpeed * Time.deltaTime, 0f);
        }

        if (Mathf.Abs(pointerPos.y) > playerDataModel.turnSensivity)
        {
            lookX = Mathf.Clamp(pointerPos.y, -1f, 1f) * Mathf.Sqrt(playerDataModel.turnSpeed) * Time.deltaTime;
            lookAtTransform.Translate(new Vector3(0f, lookX, 0f), Space.Self);
            if(lookAtTransform.localPosition.y > 3f)
            {
                lookAtTransform.localPosition = new Vector3(0f, 3f, 5f);
            }
            else if (lookAtTransform.localPosition.y < -3f)
            {
                lookAtTransform.localPosition = new Vector3(0f, -3f, 5f);
            }
        }
    }

    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
