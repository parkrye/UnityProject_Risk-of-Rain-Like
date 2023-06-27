using UnityEngine;

public class SummonPositionGimic : MonoBehaviour
{
    SphereCollider sphere;
    [SerializeField] GameObject summonZone;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void SetGimic()
    {
        float range = sphere.radius * 0.5f;

        float xPos = Random.Range(-range * 0.5f, range * 0.5f);
        float zPos = Random.Range(-range * 0.5f, range * 0.5f);

        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.right * xPos + Vector3.forward * zPos, Vector3.down);
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            summonZone.transform.position = hit.point;
        }
    }
}
