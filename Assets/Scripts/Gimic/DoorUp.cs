using System.Collections;
using UnityEngine;

public class DoorUp : MonoBehaviour
{
    public void OpenUp()
    {
        StartCoroutine(DoorUpRoutine());
    }

    IEnumerator DoorUpRoutine()
    {
        for(int i = 0; i < 60; i++)
        {
            transform.Translate(Vector3.up * 0.05f);
            yield return new WaitForSeconds(0.016f);
        }
    }
}
