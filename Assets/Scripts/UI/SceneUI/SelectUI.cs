using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        for(int slot = 1; slot <= 4; slot++)
        {
            var actionA = $"Action{slot}A";
            var actionB = $"Action{slot}B";
            var actionC = $"Action{slot}C";

            toggles[actionA].group = toggleGroups[$"Action{slot}"];
            toggles[actionB].group = toggleGroups[$"Action{slot}"];
            toggles[actionC].group = toggleGroups[$"Action{slot}"];

            toggles[actionA].AddComponent<ToggleColorChanger>();
            toggles[actionA].onValueChanged.AddListener(toggles[actionA].GetComponent<ToggleColorChanger>().ColorChange);
            toggles[actionB].AddComponent<ToggleColorChanger>();
            toggles[actionB].onValueChanged.AddListener(toggles[actionB].GetComponent<ToggleColorChanger>().ColorChange);
            toggles[actionC].AddComponent<ToggleColorChanger>();
            toggles[actionC].onValueChanged.AddListener(toggles[actionC].GetComponent<ToggleColorChanger>().ColorChange);

            toggles[actionC].isOn = true;
            toggles[actionB].isOn = true;
            toggles[actionA].isOn = true;
        }
        MoveCamera();
    }

    void SelectButton()
    {
        YesNoPopUpUI yesNoPopUpUI = GameManager.UI.ShowPopupUI<YesNoPopUpUI>("UI/YesNoUI");
        yesNoPopUpUI.SetText(0, "Start Level?");
        yesNoPopUpUI.SetText(1, "Yes");
        yesNoPopUpUI.SetText(2, "No");
        yesNoPopUpUI.YesEvent.RemoveAllListeners();
        yesNoPopUpUI.NoEvent.RemoveAllListeners();
        yesNoPopUpUI.YesEvent.AddListener(StartLevel);
        yesNoPopUpUI.NoEvent.AddListener(CancelSelect);
    }

    void StartLevel()
    {
        GameObject player = GameManager.Resource.Instantiate<GameObject>("Player/Player");
        player.GetComponent<PlayerDataModel>().SelectHero(characterNum);
        for (int slot = 1; slot <= 4; slot++)
        {
            var actionA = $"Action{slot}A";
            var actionB = $"Action{slot}B";
            var actionC = $"Action{slot}C";

            var skillNum = 1;

            if (toggles[actionA].isOn)
            {
                skillNum = 1;
            }
            else if (toggles[actionB].isOn)
            {
                skillNum = 2;
            }
            else if (!toggles[actionC].isOn)
            {
                skillNum = 3;
            }

            player.GetComponent<PlayerDataModel>().hero.SettingSkill(slot, skillNum);
        }

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
                virtualCameras[i].Priority = 10;
            }
            else
            {
                virtualCameras[i].Priority = 9;
            }
        }
    }

    void SettingSkills()
    {
        for (int i = 1; i <= 4; i++)
        {
            var actionA = $"Action{i}A";
            var actionB = $"Action{i}B";
            var actionC = $"Action{i}C";

            switch (characterNum)
            {
                // 궁수
                case 0:
                    images[actionA].sprite = GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{i}A").SkillIcon;
                    images[actionB].sprite = GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{i}B").SkillIcon;
                    images[actionC].sprite = GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{i}C").SkillIcon;
                    break;
                // 전사
                case 1:
                    images[actionA].sprite = GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{i}A").SkillIcon;
                    images[actionB].sprite = GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{i}B").SkillIcon;
                    images[actionC].sprite = GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{i}C").SkillIcon;
                    break;
                // 마법사
                case 2:
                    images[actionA].sprite = GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{i}A").SkillIcon;
                    images[actionB].sprite = GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{i}B").SkillIcon;
                    images[actionC].sprite = GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{i}C").SkillIcon;
                    break;
            }

            toggles[actionC].isOn = true;
            toggles[actionB].isOn = true;
            toggles[actionA].isOn = true;
        }
    }
}
