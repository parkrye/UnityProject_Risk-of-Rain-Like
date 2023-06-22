using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject enemy;

    public float MaxHP;
    public float Damage;
    public float Range;
    public float AttackSpeed;
    public float MoveSpeed;
    public float Exp;
    public float Size;

    public float[] floatdatas;
}
