using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 pointerPos;
    [SerializeField] Transform lookFromPoint;
    [SerializeField] float turnSpeed, lookSpeed;

    void Update()
    {
        if(pointerPos.x != 0)
        {
            transform.Rotate(Vector3.up * Mathf.Clamp(pointerPos.x, -1f, 1f) * turnSpeed);
        }
        
        if(pointerPos.y != 0)
        {
            lookFromPoint.Rotate(-Vector3.right * Mathf.Clamp(pointerPos.y, -1f, 1f) * lookSpeed);

            // 회전 각도 제한
        }
    }

    void OnPointer(InputValue inputValue)
    {
        pointerPos = inputValue.Get<Vector2>();
    }
}
