using UnityEditor.SceneManagement;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float time;
    public int difficulty;

    public EnemyDataContainer Enemy { get; private set; }
    public GameObject Player { get; set; }

    void Awake()
    {
        time = 1f;
        difficulty = 1;

        Enemy = gameObject.AddComponent<EnemyDataContainer>();
    }
}
