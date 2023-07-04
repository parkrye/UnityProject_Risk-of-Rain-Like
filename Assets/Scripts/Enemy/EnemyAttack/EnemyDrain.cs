using System.Collections;
using UnityEngine;

public class EnemyDrain : MonoBehaviour
{
    LineRenderer lineRenderer;
    Enemy enemy;
    float damage;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void StartDrain(Enemy _enemy)
    {
        enemy = _enemy;
        damage = _enemy.damage;
        StartCoroutine(DrainRoutine());
    }

    void OnDisable()
    {
        StopCoroutine(DrainRoutine());
    }

    IEnumerator DrainRoutine()
    {
        while (true)
        {
            GameManager.Data.Player.playerSystem.Hit(damage, 0f);
            Vector3 dir = GameManager.Data.Player.playerTransform.position - enemy.attackTransform.position;
            for (int i = 0; i < 11; i ++)
            {
                lineRenderer.SetPosition(i, enemy.attackTransform.position + dir * i * 0.1f + Vector3.up * Trigonometrics.Sin(i * 18f));
            }
            enemy.HP += damage * 0.5f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
