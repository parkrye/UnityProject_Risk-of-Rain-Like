using UnityEngine;

public class StartPositionGimic : MonoBehaviour
{
    SphereCollider sphere;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void SetGimic(GameObject _target, float radius)
    {
        bool check = false;
        float range = sphere.radius;
        RaycastHit hit;

        for (int i = 0; i < 100 || !check; i++)
        {
            float xPos = Random.Range(-range, range);
            float zPos = Random.Range(-range, range);

            if(Physics.SphereCast(transform.position + Vector3.right * xPos + Vector3.forward * zPos, radius, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                _target.transform.position = hit.point;
                check = true;
            }
        }
    }
}
