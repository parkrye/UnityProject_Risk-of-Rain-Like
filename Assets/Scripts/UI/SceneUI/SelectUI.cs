using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SelectUI : SceneUI
{
    int characterNum;
    CinemachineVirtualCamera[] virtualCameras;

    public override void Initialize()
    {
        virtualCameras = GameObject.Find("Cams").GetComponentsInChildren<CinemachineVirtualCamera>();

        buttons["Left"].onClick.AddListener(LeftButton);
        buttons["Right"].onClick.AddListener(RightButton);
        buttons["Select"].onClick.AddListener(SelectButton);

        for(int i = 1; i <= 4; i++)
        {
            var a = $"Action{i}A";
            var b = $"Action{i}B";
            var c = $"Action{i}C";

            toggles[a].group = toggleGroups[$"Action{i}"];
            toggles[b].group = toggleGroups[$"Action{i}"];
            toggles[c].group = toggleGroups[$"Action{i}"];

            toggles[a].AddComponent<ToggleColorChanger>();
            toggles[a].onValueChanged.AddListener(toggles[a].GetComponent<ToggleColorChanger>().ColorChange);
            toggles[b].AddComponent<ToggleColorChanger>();
            toggles[b].onValueChanged.AddListener(toggles[b].GetComponent<ToggleColorChanger>().ColorChange);
            toggles[c].AddComponent<ToggleColorChanger>();
            toggles[c].onValueChanged.AddListener(toggles[c].GetComponent<ToggleColorChanger>().ColorChange);

            toggles[c].isOn = true;
            toggles[b].isOn = true;
            toggles[a].isOn = true;
        }
        MoveCamera();
    }

    void SelectButton()
    {
        YesNoPopUpUI yesNoPopUpUI = GameManager.UI.ShowPopupUI<YesNoPopUpUI>("UI/YesNoPopUpUI");
        yesNoPopUpUI.YesEvent.AddListener(StartLevel);
        yesNoPopUpUI.NoEvent.AddListener(CancelSelect);
    }

    void StartLevel()
    {
        GameManager.Scene.LoadScene("LevelScene_Dungeon");
    }

    void CancelSelect()
    {

    }

    void LeftButton()
    {
        characterNum--;
        if (characterNum < 0)
            characterNum = virtualCameras.Length - 1;
        MoveCamera();
    }

    void RightButton()
    {
        characterNum++;
        if (characterNum > virtualCameras.Length)
            characterNum = 0;
        MoveCamera();
    }

    void MoveCamera()
    {
        SettingSkills();
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            if(i == characterNum)
            {
                virtualCameras[i].Priority = 1;
            }
            else
            {
                virtualCameras[i].Priority = 0;
            }
        }
    }

    void SettingSkills()
    {
        for (int i = 1; i <= 4; i++)
        {
            var a = $"Action{i}A";
            var b = $"Action{i}B";
            var c = $"Action{i}C";

            switch (characterNum)
            {
                // 궁수
                case 0:
                    images[a].sprite = GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{i}A").SkillIcon;
                    images[b].sprite = GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{i}B").SkillIcon;
                    images[c].sprite = GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{i}C").SkillIcon;
                    break;
                // 전사
                case 1:
                    images[a].sprite = GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{i}A").SkillIcon;
                    images[b].sprite = GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{i}B").SkillIcon;
                    images[c].sprite = GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{i}C").SkillIcon;
                    break;
                // 마법사
                case 2:
                    images[a].sprite = GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{i}A").SkillIcon;
                    images[b].sprite = GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{i}B").SkillIcon;
                    images[c].sprite = GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{i}C").SkillIcon;
                    break;
            }

            toggles[c].isOn = true;
            toggles[b].isOn = true;
            toggles[a].isOn = true;
        }
    }
}
