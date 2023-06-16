using UnityEditor.SceneManagement;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float time;
    public int difficulty;
    public PlayerDataModel Player { get; set; }

    void Awake()
    {
        time = 1f;
        difficulty = 1;
    }
}
