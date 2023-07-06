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

    public void StartDrain(Enemy _enemy, float time)
    {
        enemy = _enemy;
        damage = _enemy.damage;
        StartCoroutine(DrainRoutine(time));
    }

    IEnumerator DrainRoutine(float time)
    {
        float now = 0f;
        while (now <= time)
        {
            GameManager.Data.Player.playerSystem.Hit(damage * 0.5f, 0f);
            Vector3 dir = GameManager.Data.Player.playerTransform.position - enemy.attackTransform.position;
            for (int i = 0; i < 11; i ++)
            {
                lineRenderer.SetPosition(i, enemy.attackTransform.position + dir * i * 0.1f + Vector3.up * Trigonometrics.Sin(i * 18f));
            }
            enemy.HP += damage * 0.25f;
            now += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
