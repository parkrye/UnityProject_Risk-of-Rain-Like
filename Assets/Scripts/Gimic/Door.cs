using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float upDistance;
    [SerializeField] bool isOpen;

    public void DoorUpOpen()
    {
        if (!isOpen)
        {
            StartCoroutine(DoorUpOpenRoutine());
        }
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
}
