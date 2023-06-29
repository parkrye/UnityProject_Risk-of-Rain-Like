using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    bool recordTime;
    Dictionary<string, float> records;

    public UnityEvent TimeClock;
    public bool RecordTime { get { return recordTime; } set { recordTime = value; } }
    public Dictionary<string, float> Records { get { return records; } set { records = value; } }

    public PlayerDataModel Player { get; set; }

    void Update()
    {
        if (RecordTime)
        {
            Records["Time"] += Time.deltaTime;
            TimeClock?.Invoke();
        }
    }

    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.None;
        Records = new Dictionary<string, float>
        {
            { "Time", 0f },
            { "Difficulty", 1f },
            { "Kill", 0f },
            { "Damage", 0f },
            { "Heal", 0f },
            { "Hit", 0f },
        };
        if(Player != null)
        {
            Player.playerSystem.DestroyCharacter();
            Player = null;
        }
    }
}
