using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public abstract class EnemyAI : MonoBehaviour
{
    protected Enemy enemy;
    protected Transform player;

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
        player = GameManager.Data.Player.transform;
    }
}
