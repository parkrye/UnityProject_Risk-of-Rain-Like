using UnityEngine;

public class StartPositionGimic : MonoBehaviour
{
    SphereCollider sphere;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void SetGimic(GameObject _target)
    {
        bool check = false;
        float range = sphere.radius;
        RaycastHit hit;

        while (!check)
        {
            float xPos = Random.Range(-range, range);
            float zPos = Random.Range(-range, range);

            Ray ray = new Ray(transform.position + Vector3.right * xPos + Vector3.forward * zPos, Vector3.down);
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                _target.transform.position = hit.point;
                check = true;
            }
        }
    }
}
