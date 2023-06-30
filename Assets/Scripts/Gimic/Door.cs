using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    float cosResult;
    [SerializeField] float upDistance, sideDir;
    [SerializeField] bool isOpen;
    [SerializeField] int fowardDir;

    void Awake()
    {
        cosResult = Mathf.Cos(60f * Mathf.Deg2Rad);
        if (sideDir == 0f)
            sideDir = 1f;
    }

    public void DoorUpOpen()
    {
        if (!isOpen)
        {
            StartCoroutine(DoorUpOpenRoutine());
        }
    }
    public void DoorSideOpen()
    {
        StartCoroutine(DoorSideOpenRoutine());
    }

    IEnumerator DoorUpOpenRoutine()
    {
        isOpen = true;
        for (int i = 0; i < upDistance * 100f; i++)
        {
            transform.Translate(Vector3.up * 0.01f);
            yield return new WaitForSeconds(0.016f);
        }
    }

    IEnumerator DoorSideOpenRoutine()
    {
        Vector3 doorFoward = transform.forward;
        if(fowardDir == 1)
        {
            doorFoward = transform.right;
        }
        else if(fowardDir == 2)
        {
            doorFoward += transform.right;
        }
        else if(fowardDir == 3)
        {
            doorFoward -= transform.right;
        }

        if (Vector3.Dot(GameManager.Data.Player.playerTransform.forward, doorFoward) < cosResult)
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
