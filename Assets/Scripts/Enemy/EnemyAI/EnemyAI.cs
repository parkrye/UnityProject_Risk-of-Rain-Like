using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public abstract class EnemyAI : MonoBehaviour
{
    protected Enemy enemy;
    protected Transform playerTransform;

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
        playerTransform = GameManager.Data.Player.playerTransform;
    }
}
