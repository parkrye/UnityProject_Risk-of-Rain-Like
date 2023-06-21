using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject enemy;

    public float MaxHP;
    public float Damage;
    public float Range;
    public float Speed;
    public float exp;
    public float size;
}
