using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoUI : SceneUI
{
    public override void Initialize()
    {
        SettingDifficulty();
        GameManager.Data.TimeClock.AddListener(UpdateTime);
    }

    /// <summary>
    /// 초기 난이도 배치
    /// </summary>
    void SettingDifficulty()
    {
        switch(GameManager.Data.Difficulty)
        {
            case 1:
                images["DifficultyImage"].sprite = GameManager.Resource.Load<Icon>("Icon/EasyModeIcon").sprite;
                break;
            case 2:
                images["DifficultyImage"].sprite = GameManager.Resource.Load<Icon>("Icon/NormalModeIcon").sprite;
                break;
            case 3:
                images["DifficultyImage"].sprite = GameManager.Resource.Load<Icon>("Icon/HardModeIcon").sprite;
                break;
        }
    }

    /// <summary>
    /// 시간 흐름
    /// </summary>
    public void UpdateTime()
    {
        int seconds = (int)GameManager.Data.Time;
        string minText = seconds / 60 < 10 ? "0" + (seconds / 60).ToString() : (seconds / 60).ToString();
        string secText = seconds % 60 < 10 ? "0" + (seconds % 60).ToString() : (seconds % 60).ToString();
        texts["TimeText"].text = $"{minText}:{secText}";
    }

    /// <summary>
    /// 목표 관련 이벤트
    /// </summary>
    public void UpdateObjective()
    {

    }
}
