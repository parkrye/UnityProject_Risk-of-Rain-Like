using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public int Difficulty;
    public float Time;
    public bool RecordTime;
    public UnityEvent TimeClock;

    public PlayerDataModel Player { get; set; }

    void Awake()
    {
        Time = 0f;
        Difficulty = 1;
    }

    void Update()
    {
        if (RecordTime)
        {
            Time += UnityEngine.Time.deltaTime;
            TimeClock?.Invoke();
        }
    }
}
