using Cinemachine;
using UnityEngine;

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
        buttons["Main"].onClick.AddListener(MainButton);

        for(int slot = 1; slot <= 4; slot++)
        {
            var actionA = $"Action{slot}A";
            var actionB = $"Action{slot}B";
            var actionC = $"Action{slot}C";

            toggles[actionA].group = toggleGroups[$"Action{slot}"];
            toggles[actionB].group = toggleGroups[$"Action{slot}"];
            toggles[actionC].group = toggleGroups[$"Action{slot}"];

            toggles[actionA].gameObject.AddComponent<ToggleColorChanger>();
            toggles[actionA].onValueChanged.AddListener(toggles[actionA].GetComponent<ToggleColorChanger>().ColorChange);
            toggles[actionB].gameObject.AddComponent<ToggleColorChanger>();
            toggles[actionB].onValueChanged.AddListener(toggles[actionB].GetComponent<ToggleColorChanger>().ColorChange);
            toggles[actionC].gameObject.AddComponent<ToggleColorChanger>();
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

        GameManager.Scene.LoadScene("LevelScene");
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

    void MainButton()
    {
        GameManager.Scene.LoadScene("MainScene");
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
        for (int slot = 1; slot <= 4; slot++)
        {
            var actionA = $"Action{slot}A";
            var actionB = $"Action{slot}B";
            var actionC = $"Action{slot}C";

            switch (characterNum)
            {
                // 궁수
                case 0:
                    toggles[actionA].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{slot}A"));
                    toggles[actionB].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{slot}B"));
                    toggles[actionC].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{slot}C"));
                    images[actionA].sprite = toggles[actionA].GetComponent<SkillListUI>().skill.SkillIcon;
                    images[actionB].sprite = toggles[actionB].GetComponent<SkillListUI>().skill.SkillIcon;
                    images[actionC].sprite = toggles[actionC].GetComponent<SkillListUI>().skill.SkillIcon;
                    break;
                // 전사
                case 1:
                    toggles[actionA].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{slot}A"));
                    toggles[actionB].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{slot}B"));
                    toggles[actionC].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{slot}C"));
                    images[actionA].sprite = toggles[actionA].GetComponent<SkillListUI>().skill.SkillIcon;
                    images[actionB].sprite = toggles[actionB].GetComponent<SkillListUI>().skill.SkillIcon;
                    images[actionC].sprite = toggles[actionC].GetComponent<SkillListUI>().skill.SkillIcon;
                    break;
                // 마법사
                case 2:
                    toggles[actionA].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{slot}A"));
                    toggles[actionB].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{slot}B"));
                    toggles[actionC].GetComponent<SkillListUI>().skill = (GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{slot}C"));
                    images[actionA].sprite = toggles[actionA].GetComponent<SkillListUI>().skill.SkillIcon;
                    images[actionB].sprite = toggles[actionB].GetComponent<SkillListUI>().skill.SkillIcon;
                    images[actionC].sprite = toggles[actionC].GetComponent<SkillListUI>().skill.SkillIcon;
                    break;
            }

            toggles[actionC].isOn = true;
            toggles[actionB].isOn = true;
            toggles[actionA].isOn = true;
        }
    }
}
