using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    bool recordTime;
    int[] time;
    Dictionary<string, float> records;

    public UnityEvent TimeClock;
    public bool RecordTime { get { return recordTime; } set { recordTime = value; } }
    public int[] Time { get { return time; } set { time = value; } } 
    public Dictionary<string, float> NowRecords { get { return records; } set { records = value; } }
    public Dictionary<string, int> Records { get; private set; }
    public Dictionary<string, List<int>> Achivements { get; set; }

    public PlayerDataModel Player { get; set; }

    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.None;
        NowRecords = new Dictionary<string, float>
        {
            { "Stage", 1f },
            { "Time", 0f },
            { "Difficulty", 1f },
            { "Kill", 0f },
            { "Damage", 0f },
            { "Heal", 0f },
            { "Hit", 0f },
            { "Money", 0f },
            { "Cost", 0f },
        };

        Records = CSVRW.ReadCSV_Records();

        if (Player != null)
        {
            Player.playerSystem.DestroyCharacter();
            Player = null;
        }

        Achivements = CSVRW.ReadCSV_Achivements();

        time = new int[2];

        StartCoroutine(TimeRoutine());
    }

    IEnumerator TimeRoutine()
    {
        while (this)
        {
            if (++time[1] >= 60)
            {
                time[0]++;
                time[1] = 0;
            }
            NowRecords["Time"] += 1f;
            TimeClock?.Invoke();
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    /// <summary>
    /// 기록의 특정 값을 수정하는 메소드
    /// </summary>
    /// <param name="index">Records의 키</param>
    /// <param name="value">Records에 저장할 값</param>
    public void SetRecords(string index, int value)
    {
        if(!Records.ContainsKey(index))
            throw new IndexOutOfRangeException();
        Records[index] = value;
    }

    /// <summary>
    /// 기록을 저장하는 메소드
    /// </summary>
    public void SaveRecords()
    {
        CSVRW.WriteCSV_Records(Records);
    }

    /// <summary>
    /// 기록을 초기화하는 메소드
    /// </summary>
    public void ResetRecords()
    {
        Records["StageCount"] = 0;
        Records["TimeCount"] = 0;
        Records["KillCount"] = 0;
        Records["DamageCount"] = 0;
        Records["HitCount"] = 0;
        Records["HealCount"] = 0;
        Records["MoneyCount"] = 0;
        Records["CostCount"] = 0;
        Records["LevelCount"] = 0;
        CSVRW.WriteCSV_Records(Records);
    }

    /// <summary>
    /// 업적의 특정 값을 수정하는 메소드
    /// </summary>
    /// <param name="index">Achivement의 키</param>
    /// <param name="value">Achivement에 저장할 값</param>
    public void SetAchives(string index, int value)
    {
        if (!Achivements.ContainsKey(index))
            throw new IndexOutOfRangeException();
        int total = Achivements[index][0] + value;
        Achivements[index][0] = total;
    }
    /// <summary>
    /// 업적을 저장하는 메소드
    /// </summary>
    public void SaveAchivements()
    {
        CSVRW.WriteCSV_Achivements(Achivements);
    }
}
