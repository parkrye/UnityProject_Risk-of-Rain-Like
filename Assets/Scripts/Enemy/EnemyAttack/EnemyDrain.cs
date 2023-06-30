using System.Collections;
using UnityEngine;

public class EnemyDrain : MonoBehaviour
{
    LineRenderer lineRenderer;
    Enemy enemy;
    Transform startTransform;
    float damage;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void StartDrain(Enemy _enemy)
    {
        enemy = _enemy;
        startTransform = _enemy.attackTransform;
        damage = _enemy.damage;
        StartCoroutine(DrainRoutine());
    }

    void OnDisable()
    {
        StopCoroutine(DrainRoutine());
    }

    IEnumerator DrainRoutine()
    {
        lineRenderer.SetPosition(0, startTransform.position);
        while (true)
        {
            GameManager.Data.Player.playerSystem.Hit(damage, 0f);
            enemy.HP += damage * 0.5f;
            lineRenderer.SetPosition(1, GameManager.Data.Player.playerTransform.position + Vector3.up);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
