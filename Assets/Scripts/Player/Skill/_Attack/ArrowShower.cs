using System.Collections;
using UnityEngine;

public class ArrowShower : MonoBehaviour
{
    public void Shot(Transform transform, float damage)
    {
        StartCoroutine(ShowerRoutine(transform, damage));
    }

    IEnumerator ShowerRoutine(Transform transform, float damage)
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject arrow = GameManager.Resource.Instantiate(GameManager.Resource.Load<GameObject>("Attack/Arrow"), true);
            arrow.transform.position = transform.position + Vector3.right * Random.Range(-7.5f, 7.5f) + Vector3.forward * Random.Range(-7.5f, 7.5f) + Vector3.up * 50f;
            arrow.GetComponent<Arrow>().Shot(transform.position, damage);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
