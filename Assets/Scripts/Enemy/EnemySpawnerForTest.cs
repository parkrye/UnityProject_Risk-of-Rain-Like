using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnerForTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        EnemyData enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Bat");
        GameManager.Resource.Instantiate(enemyData.enemy, transform.position, transform.rotation, true);
    }
}
