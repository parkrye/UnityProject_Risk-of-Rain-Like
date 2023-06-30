using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Door : MonoBehaviour
{
    float cosResult;
    [SerializeField] float upDistance, sideDir;
    [SerializeField] bool isOpen;

    void Awake()
    {
        cosResult = Mathf.Cos(60f * Mathf.Deg2Rad);
        if (sideDir == 0f)
            sideDir = 1f;
    }

    public void DoorUpOpen()
    {
        StartCoroutine(DoorUpOpenRoutine());
    }
    public void DoorSideOpen()
    {
        StartCoroutine(DoorSideOpenRoutine());
    }

    IEnumerator DoorUpOpenRoutine()
    {
        for(int i = 0; i < upDistance * 100f; i++)
        {
            transform.Translate(Vector3.up * 0.01f);
            yield return new WaitForSeconds(0.016f);
        }
    }

    IEnumerator DoorSideOpenRoutine()
    {
        if (Vector3.Dot(GameManager.Data.Player.playerTransform.forward, transform.forward) < cosResult)
        {
            if(transform.localEulerAngles.y < 45f || transform.localEulerAngles.y > 135f)
            {
                for (int i = 0; i < 90; i++)
                {
                    transform.Rotate(Vector3.up * 1f * sideDir);
                    yield return new WaitForSeconds(0.016f);
                }
            }
        }
        else
        {
            if ((transform.localEulerAngles.y < 0f) ? (transform.localEulerAngles.y > -45f) : (transform.localEulerAngles.y > 315f))
            {
                for (int i = 0; i < 90; i++)
                {
                    transform.Rotate(Vector3.up * -1f * sideDir);
                    yield return new WaitForSeconds(0.016f);
                }
            }
        }
    }
}
