using System.Collections;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] float markerHeight, cameraHeight;
    [SerializeField] bool isCamera;

    void OnEnable()
    {
        StartCoroutine(FixRoutine());
    }

    IEnumerator FixRoutine()
    {
        while (isActiveAndEnabled)
        {
            if (isCamera)
            {
                transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);
            }
            else
            {
                transform.LookAt(transform.position + Vector3.up);
                transform.position = new Vector3(transform.position.x, markerHeight, transform.position.z);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
