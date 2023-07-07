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
            try
            {
                transform.position = new Vector3(target.position.x, height, target.position.z);
            }
            catch
            {
                GameManager.Resource.Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
