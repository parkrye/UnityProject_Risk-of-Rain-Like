using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    /// <summary>
    /// 랜덤한 위치에 랜덤한 에너미를 소환하는 메소드
    /// </summary>
    /// <param name="location">중심 위치</param>
    /// <param name="distance">랜덤 거리</param>
    /// <returns>소환한 에너미 오브젝트</returns>
    public static GameObject RandomLocationSummon(Transform location, float distance)
    {
        EnemyData[] enemyDatas = GameManager.Resource.LoadAll<EnemyData>("Enemy");

        Vector3 spawnPosition = Vector3.zero;
        float remainDistance = distance;

        spawnPosition.x = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
        remainDistance -= spawnPosition.x;

        spawnPosition.y = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
        remainDistance -= spawnPosition.y;

        spawnPosition.z = remainDistance;

        GameObject enemy = GameManager.Resource.Instantiate(enemyDatas[Random.Range(0, enemyDatas.Length)].enemy, location.position + spawnPosition, Quaternion.identity, true);
        return enemy;
    }

    /// <summary>
    /// 랜덤한 위치에 특정한 에너미를 소환하는 메소드
    /// </summary>
    /// <param name="location">중심 위치</param>
    /// <param name="distance">랜덤 거리</param>
    /// <param name="path">에너미 데이터의 경로</param>
    /// <returns>소환한 에너미 오브젝트</returns>
    public static GameObject RandomLocationSummon(Transform location, float distance, string path)
    {
        Vector3 spawnPosition = Vector3.zero;
        float remainDistance = distance;

        spawnPosition.x = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
        remainDistance -= spawnPosition.x;

        spawnPosition.y = Random.Range(remainDistance * 0.2f, remainDistance * 0.8f);
        remainDistance -= spawnPosition.y;

        spawnPosition.z = remainDistance;

        GameObject enemy = GameManager.Resource.Instantiate(GameManager.Resource.Load<EnemyData>(path).enemy, location.position + spawnPosition, Quaternion.identity, true);
        return enemy;
    }

    /// <summary>
    /// 특정한 위치에 랜덤한 에너미를 소환하는 메소드
    /// </summary>
    /// <param name="location">소환 위치</param>
    /// <returns>소환한 에너미 오브젝트</returns>
    public static GameObject TargetLocationSummon(Transform location)
    {
        EnemyData[] enemyDatas = GameManager.Resource.LoadAll<EnemyData>("Enemy");
        GameObject enemy = GameManager.Resource.Instantiate(enemyDatas[Random.Range(0, enemyDatas.Length)].enemy, location.position, Quaternion.identity, true);
        return enemy;
    }

    /// <summary>
    /// 특정한 위치에 특정한 에너미를 소환하는 메소드
    /// </summary>
    /// <param name="location">소환 위치</param>
    /// <param name="path">소환할 에너미 데이터의 경로</param>
    /// <returns>소환한 에너미 오브젝트</returns>
    public static GameObject TargetLocationSummon(Transform location, string path)
    {
        GameObject enemy = GameManager.Resource.Instantiate(GameManager.Resource.Load<EnemyData>(path).enemy, location.position, Quaternion.identity, true);
        return enemy;
    }

    /// <summary>
    /// 특정한 위치에 특정한 에너미를 소환하는 메소드
    /// </summary>
    /// <param name="location">소환 위치</param>
    /// <param name="enemyData">소환할 에너미 데이터</param>
    /// <returns>소환한 에너미 오브젝트</returns>
    public static GameObject TargetLocationSummon(Transform location, EnemyData enemyData)
    {
        GameObject enemy = GameManager.Resource.Instantiate(enemyData.enemy, location.position, Quaternion.identity, true);
        return enemy;
    }
}
