using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour, IInitializable
{
    public int Difficulty;
    public float Time;
    public bool RecordTime;
    public UnityEvent TimeClock;

    public PlayerDataModel Player { get; set; }

    public void Initialize()
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
