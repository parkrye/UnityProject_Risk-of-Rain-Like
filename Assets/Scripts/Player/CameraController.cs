using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 pointerPos;
    [SerializeField] Transform lookFromPoint;
    [SerializeField] float turnSpeed, lookX, turnSensivity;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Mathf.Abs(pointerPos.x) > turnSensivity)
        {
            transform.localEulerAngles += new Vector3(0f, Mathf.Clamp(pointerPos.x, -1f, 1f) * turnSpeed * Time.deltaTime, 0f);
        }

        if (Mathf.Abs(pointerPos.y) > turnSensivity)
        {
            lookX -= Mathf.Clamp(pointerPos.y, -1f, 1f) * turnSpeed * Time.deltaTime;
            lookFromPoint.localEulerAngles = new Vector3(Mathf.Clamp(lookX, -60f, 60f), 0f, 0f);
        }
    }

    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
