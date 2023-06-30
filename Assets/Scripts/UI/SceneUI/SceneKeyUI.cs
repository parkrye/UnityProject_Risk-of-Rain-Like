using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeyUI : SceneUI
{
    Color enableColor, disableColor;

    public override void Initialize()
    {
        enableColor = new Color(0f, 0f, 0f, 0f);
        disableColor = new Color(0f, 0f, 0f, 0.8f);
        SettingKeyIcons();
        GameManager.Data.Player.hero.ActionEvent.RemoveAllListeners();
        GameManager.Data.Player.hero.ActionEvent.AddListener(UpdateCoolTime);
    }

    /// <summary>
    /// 초기 아이콘 배치
    /// </summary>
    void SettingKeyIcons()
    {
        images["Action1Icon"].sprite = GameManager.Data.Player.hero.skills[0].SkillIcon;
        images["Action2Icon"].sprite = GameManager.Data.Player.hero.skills[1].SkillIcon;
        images["Action3Icon"].sprite = GameManager.Data.Player.hero.skills[2].SkillIcon;
        images["Action4Icon"].sprite = GameManager.Data.Player.hero.skills[3].SkillIcon;
    }

    /// <summary>
    /// 스킬 이벤트 활성화
    /// </summary>
    public void UpdateCoolTime(bool[] cools)
    {
        images["Cool1Icon"].color = cools[0] ? enableColor : disableColor;
        images["Cool2Icon"].color = cools[1] ? enableColor : disableColor;
        images["Cool3Icon"].color = cools[2] ? enableColor : disableColor;
        images["Cool4Icon"].color = cools[3] ? enableColor : disableColor;
    }
}
