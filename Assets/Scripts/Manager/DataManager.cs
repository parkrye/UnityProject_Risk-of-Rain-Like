using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public bool RecordTime;
    public UnityEvent TimeClock;
    public Dictionary<string, float> Records;

    public PlayerDataModel Player { get; set; }

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
