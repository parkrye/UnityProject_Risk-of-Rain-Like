using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public bool RecordTime;
    public UnityEvent TimeClock;
    public Dictionary<string, float> Records;

    public PlayerDataModel Player { get; set; }

    void Awake()
    {
        Records = new Dictionary<string, float>
        {
            { "Time", 0 },
            { "Difficulty", 1 },
            { "Kill", 0 },
            { "Damage", 0 },
            { "Heal", 0 },
            { "Hit", 0 },
            { "Guard", 0 },
        };
    }

    void Update()
    {
        if (RecordTime)
        {
            Records["Time"] += UnityEngine.Time.deltaTime;
            TimeClock?.Invoke();
        }
    }

    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.None;
        Records = new Dictionary<string, float>
        {
            { "Time", 0 },
            { "Difficulty", 1 },
            { "Kill", 0 },
            { "Damage", 0 },
            { "Heal", 0 },
        };
        Player.DestroyCharacter();
    }
}
