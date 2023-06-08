using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Enemy", menuName = "Data/Enemy")]
public class EnemyDataBase : ScriptableObject
{
    public float MaxHP;
    public float Damage;
    public float Range;
    public float Speed;
}
