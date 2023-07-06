using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectUI : SceneUI
{
    int characterNum;
    CinemachineVirtualCamera[] virtualCameras;
    [SerializeField] Color selected, nonSelected;
    [SerializeField] UnityEvent<int> startLevelEvent;

    public override void Initialize()
    {
        selected = new Color(1f, 1f, 1f);
        nonSelected = new Color(0.5f, 0.5f, 0.5f);

        virtualCameras = GameObject.Find("Cams").GetComponentsInChildren<CinemachineVirtualCamera>();

        buttons["Left"].onClick.AddListener(LeftButton);
        buttons["Right"].onClick.AddListener(RightButton);
        buttons["Select"].onClick.AddListener(SelectButton);
        buttons["Main"].onClick.AddListener(MainButton);
        buttons["EasyButton"].onClick.AddListener(() => ChangeDifficulty(1));
        buttons["EasyButton"].GetComponentInChildren<DescTargetDifficultyUI>().difficulty = GameManager.Resource.Load<Icon>("Icon/EasyModeIcon");
        buttons["NormalButton"].onClick.AddListener(() => ChangeDifficulty(2));
        buttons["NormalButton"].GetComponentInChildren<DescTargetDifficultyUI>().difficulty = GameManager.Resource.Load<Icon>("Icon/NormalModeIcon");
        buttons["HardButton"].onClick.AddListener(() => ChangeDifficulty(3));
        buttons["HardButton"].GetComponentInChildren<DescTargetDifficultyUI>().difficulty = GameManager.Resource.Load<Icon>("Icon/HardModeIcon");

        for (int slot = 1; slot <= 4; slot++)
        {
            string actionA = $"Action{slot}A";
            string actionB = $"Action{slot}B";
            string actionC = $"Action{slot}C";

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
        ChangeDifficulty();
    }

    void SelectButton()
    {
        YesNoPopUpUI yesNoPopUpUI = GameManager.UI.ShowPopupUI<YesNoPopUpUI>("UI/YesNoUI");
        yesNoPopUpUI.SetText(0, "이대로 도전하시겠습니까?");
        yesNoPopUpUI.SetText(1, "예");
        yesNoPopUpUI.SetText(2, "아니오");
        yesNoPopUpUI.YesEvent.RemoveAllListeners();
        yesNoPopUpUI.NoEvent.RemoveAllListeners();
        yesNoPopUpUI.YesEvent.AddListener(StartLevel);
        yesNoPopUpUI.NoEvent.AddListener(CancelSelect);
    }

    void StartLevel()
    {
        startLevelEvent?.Invoke(characterNum);
        StartCoroutine(StartLevelRoutine());
    }

    IEnumerator StartLevelRoutine()
    {
        yield return new WaitForSeconds(1f);

        GameObject player = GameManager.Resource.InstantiateDontDestroyOnLoad<GameObject>("Player/Player", null, false);
        player.GetComponent<PlayerDataModel>().playerSystem.SelectHero(characterNum);
        for (int slot = 1; slot <= 4; slot++)
        {
            string actionA = $"Action{slot}A";
            string actionB = $"Action{slot}B";
            string actionC = $"Action{slot}C";

            int skillNum = 1;

            if (toggles[actionA].isOn)
            {
                skillNum = 1;
            }
            else if (toggles[actionB].isOn)
            {
                skillNum = 2;
            }
            else if (toggles[actionC].isOn)
            {
                skillNum = 3;
            }

            player.GetComponent<PlayerDataModel>().hero.SettingSkill(slot, skillNum);
        }

        switch (Random.Range(0, 3))
        {
            case 0:
                GameManager.Scene.LoadScene("LevelScene_Field");
                break;
            case 1:
                GameManager.Scene.LoadScene("LevelScene_Castle");
                break;
            case 2:
                GameManager.Scene.LoadScene("LevelScene_Viking");
                break;
        }
    }

    void ChangeDifficulty(int num = 1)
    {
        GameManager.Data.NowRecords["Difficulty"] = num;
        switch(num)
        {
            case 1:
                for(int i = 0; i < buttons["EasyButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["EasyButton"].GetComponentsInChildren<Image>()[i].color = selected;
                }
                for (int i = 0; i < buttons["NormalButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["NormalButton"].GetComponentsInChildren<Image>()[i].color = nonSelected;
                }
                for (int i = 0; i < buttons["HardButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["HardButton"].GetComponentsInChildren<Image>()[i].color = nonSelected;
                }
                break;
            case 2:
                for (int i = 0; i < buttons["EasyButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["EasyButton"].GetComponentsInChildren<Image>()[i].color = nonSelected;
                }
                for (int i = 0; i < buttons["NormalButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["NormalButton"].GetComponentsInChildren<Image>()[i].color = selected;
                }
                for (int i = 0; i < buttons["HardButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["HardButton"].GetComponentsInChildren<Image>()[i].color = nonSelected;
                }
                break;
            case 3:
                for (int i = 0; i < buttons["EasyButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["EasyButton"].GetComponentsInChildren<Image>()[i].color = nonSelected;
                }
                for (int i = 0; i < buttons["NormalButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["NormalButton"].GetComponentsInChildren<Image>()[i].color = nonSelected;
                }
                for (int i = 0; i < buttons["HardButton"].GetComponentsInChildren<Image>().Length; i++)
                {
                    buttons["HardButton"].GetComponentsInChildren<Image>()[i].color = selected;
                }
                break;
        }
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
        if (characterNum >= virtualCameras.Length)
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
            string actionA = $"Action{slot}A";
            string actionB = $"Action{slot}B";
            string actionC = $"Action{slot}C";

            DescTargetSkillUI skillA = toggles[actionA].GetComponent<DescTargetSkillUI>();
            DescTargetSkillUI skillB = toggles[actionB].GetComponent<DescTargetSkillUI>();
            DescTargetSkillUI skillC = toggles[actionC].GetComponent<DescTargetSkillUI>();

            switch (characterNum)
            {
                // 궁수
                case 0:
                    skillA.skill = (GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{slot}A"));
                    skillB.skill = (GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{slot}B"));
                    skillC.skill = (GameManager.Resource.Load<Skill>($"Skill/Archer/Archer_Action{slot}C"));
                    images[actionA].sprite = skillA.skill.SkillIcon;
                    images[actionB].sprite = skillB.skill.SkillIcon;
                    images[actionC].sprite = skillC.skill.SkillIcon;
                    break;
                // 전사
                case 1:
                    skillA.skill = (GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{slot}A"));
                    skillB.skill = (GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{slot}B"));
                    skillC.skill = (GameManager.Resource.Load<Skill>($"Skill/Warrior/Warrior_Action{slot}C"));
                    images[actionA].sprite = skillA.skill.SkillIcon;
                    images[actionB].sprite = skillB.skill.SkillIcon;
                    images[actionC].sprite = skillC.skill.SkillIcon;
                    break;
                // 마법사
                case 2:
                    skillA.skill = (GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{slot}A"));
                    skillB.skill = (GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{slot}B"));
                    skillC.skill = (GameManager.Resource.Load<Skill>($"Skill/Wizard/Wizard_Action{slot}C"));
                    images[actionA].sprite = skillA.skill.SkillIcon;
                    images[actionB].sprite = skillB.skill.SkillIcon;
                    images[actionC].sprite = skillC.skill.SkillIcon;
                    break;
            }

            toggles[actionC].isOn = true;
            toggles[actionB].isOn = true;
            toggles[actionA].isOn = true;
        }
    }

    public void AddListener(UnityAction<int> action)
    {
        startLevelEvent.AddListener(action);
    }
}
