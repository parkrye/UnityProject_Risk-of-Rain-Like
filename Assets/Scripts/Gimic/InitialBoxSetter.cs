using UnityEngine;

public class InitialBoxSetter : MonoBehaviour
{
    SphereCollider sphere;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void SetTowerBoxes()
    {
        int count = Random.Range(1, 11);
        float range = sphere.radius * 0.5f;
        float xPos, zPos;
        RaycastHit hit;
        Ray ray;

        for (int i = 0; i < count; i++)
        {
            xPos = Random.Range(-range * 0.5f, range * 0.5f);
            zPos = Random.Range(-range * 0.5f, range * 0.5f);

            ray = new Ray(transform.position + Vector3.right * xPos + Vector3.forward * zPos, Vector3.down);
            Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground"));

            int type = Random.Range(0, 2);

            GameObject box;
            if(type == 0)
                box = GameManager.Resource.Instantiate<GameObject>("Item/TowerBox");
            else
                box = GameManager.Resource.Instantiate<GameObject>("Item/RouletteBox");
            box.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z) + Vector3.up * 2f;
            GameManager.Resource.Instantiate<MinimapMarker>("Marker/MinimapMarker_Gimic", true).StartFollowing(box.transform);
        }
    }
}
