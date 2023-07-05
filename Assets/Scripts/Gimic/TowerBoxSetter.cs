using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoxSetter : MonoBehaviour
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

            GameObject towerBox = GameManager.Resource.Instantiate<GameObject>("Item/TowerBox");
            towerBox.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            GameManager.Resource.Instantiate<MinimapMarker>("Marker/MinimapMarker_Tower", true).StartFollowing(towerBox.transform);
        }
    }
}
