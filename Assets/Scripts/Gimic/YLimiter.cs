using System.Collections;
using UnityEngine;

public class YLimiter : MonoBehaviour
{
    [SerializeField] Transform[] transforms;
    [SerializeField] Transform spawnTransform;
    
    public void Initialize()
    {
        transforms = GetComponentsInChildren<Transform>();
        StartCoroutine(CheckYRoutine());
    }

    IEnumerator CheckYRoutine()
    {
        while (true)
        {
            yield return null;
            Vector3 player = GameManager.Data.Player.transform.position;
            if(player.y > transforms[1].position.y || player.y < transforms[2].position.y)
            {
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(player.x, transforms[1].position.y, player.z), Vector3.down);
                if (Physics.Raycast(ray, out hit))
                {
                    GameManager.Data.Player.transform.position = hit.point + Vector3.up * 2f;
                }
                else
                {
                    GameManager.Data.Player.transform.position = spawnTransform.position;
                }
            }
        }
    }
}
