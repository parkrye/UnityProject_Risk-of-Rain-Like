using System.Collections;
using UnityEngine;

public class MinimapMarker : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float height;

    public void StartFollowing(Transform _target)
    {
        target = _target;
        StartCoroutine(FollowRoutine());
    }

    IEnumerator FollowRoutine()
    {
        while (this)
        {
            if (!target)
                break;
            transform.position = new Vector3(target.position.x, height, target.position.z);
            yield return new WaitForEndOfFrame();
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
