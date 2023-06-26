using System.Collections;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    SphereCollider sphere;
    [SerializeField] float dropDelay;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    void Start()
    {
        StartCoroutine(ItemDropRoutine());
    }

    IEnumerator ItemDropRoutine()
    {
        while (true)
        {
            float range = sphere.radius * 0.5f;

            float xPos = Random.Range(-range * 0.5f, range * 0.5f);
            float zPos = Random.Range(-range * 0.5f, range * 0.5f);

            RaycastHit hit;
            Ray ray = new Ray(transform.position + Vector3.right * xPos + Vector3.forward * zPos, Vector3.down);
            Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground"));

            GameObject drop = GameManager.Resource.Instantiate<GameObject>("Item/MerchantBox");
            drop.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            
            yield return new WaitForSeconds(dropDelay);
        }
    }
}
