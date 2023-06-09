using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // 대대적 수정

    [SerializeField] Vector2 pointerPos;
    [SerializeField] float turnSpeed, lookX, turnSensivity;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Mathf.Abs(pointerPos.x) > turnSensivity * 2)
        {
            transform.localEulerAngles += new Vector3(0f, Mathf.Clamp(pointerPos.x, -1f, 1f) * turnSpeed * Time.deltaTime, 0f);
        }

        if (Mathf.Abs(pointerPos.y) > turnSensivity)
        {
            lookX -= Mathf.Clamp(pointerPos.y, -1f, 1f) * turnSpeed * Time.deltaTime;
        }
    }

    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
