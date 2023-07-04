using System.Collections.Generic;
using UnityEngine;

public class AchivementUI : SceneUI
{
    [SerializeField] GameObject[] uis = new GameObject[3];
    [SerializeField] Transform achiveTransform;

    public override void Initialize()
    {
        buttons["MainButton"].onClick.AddListener(OnMainButton);
        buttons["RecordButton"].onClick.AddListener(OnRecordButton);
        buttons["AchivementButton"].onClick.AddListener(OnAchivementButton);

        RecordSetting();
        AchivementSetting();

        OnRecordButton();
    }

    void RecordSetting()
    {
        texts["StageText"].text += GameManager.Data.Records["StageCount"];
        texts["LevelText"].text += GameManager.Data.Records["LevelCount"];
        texts["TimeText"].text += GameManager.Data.Records["TimeCount"];
        texts["KillText"].text += GameManager.Data.Records["KillCount"];
        texts["DamageText"].text += GameManager.Data.Records["DamageCount"];
        texts["HitText"].text += GameManager.Data.Records["HitCount"];
        texts["HealText"].text += GameManager.Data.Records["HealCount"];
        texts["MoneyText"].text += GameManager.Data.Records["MoneyCount"];
        texts["CostText"].text += GameManager.Data.Records["CostCount"];
    }

    void AchivementSetting()
    {
        AchivementElementUI achiveUI = GameManager.Resource.Load<AchivementElementUI>("UI/AchivementElementUI");
        Sprite noneIcon = GameManager.Resource.Load<Icon>("Icon/AchiveNoneIcon").sprite;
        Sprite clearIcon = GameManager.Resource.Load<Icon>("Icon/AchiveClearIcon").sprite;
        int achiveCount = 0;

        foreach (KeyValuePair<string, List<int>> pair in GameManager.Data.Achivements)
        {
            string achiveTitle, achiveContent;
            switch (pair.Key)
            {
                default:
                case "StageCount":
                    achiveTitle = "등산가";
                    achiveContent = "회 다음 스테이지에 도달한다";
                    break;
                case "LevelCount":
                    achiveTitle = "모험자";
                    achiveContent = " 레벨을 달성한다";
                    break;
                case "TimeCount":
                    achiveTitle = "생존자";
                    achiveContent = "초 생존한다";
                    break;
                case "KillCount":
                    achiveTitle = "학살자";
                    achiveContent = "마리의 적을 사냥한다";
                    break;
                case "DamageCount":
                    achiveTitle = "딜러";
                    achiveContent = " 데미지를 입힌다";
                    break;
                case "HitCount":
                    achiveTitle = "탱커";
                    achiveContent = " 데미지를 입는다";
                    break;
                case "HealCount":
                    achiveTitle = "힐러";
                    achiveContent = " 데미지를 회복한다";
                    break;
                case "MoneyCount":
                    achiveTitle = "부자";
                    achiveContent = "의 재화를 획득한다";
                    break;
                case "CostCount":
                    achiveTitle = "납세자";
                    achiveContent = "의 재화를 소모한다";
                    break;
            }

            for (int i = 1; i < pair.Value.Count; i++)
            {
                string content = pair.Value[i].ToString() + achiveContent;
                if (pair.Value[0] >= pair.Value[i])
                    GameManager.UI.ShowSceneUI(achiveUI, achiveTransform).SetContent(clearIcon, achiveTitle + $"({i})", content);
                else
                    GameManager.UI.ShowSceneUI(achiveUI, achiveTransform).SetContent(noneIcon, achiveTitle + $"({i})", content);

                achiveCount++;
            }
        }
        achiveTransform.GetComponent<RectTransform>().offsetMin = new Vector2(0, -achiveCount * 300f);
    }

    void OnMainButton()
    {
        GameManager.Scene.LoadScene("MainScene");
    }

    void OnRecordButton()
    {
        UISetter(0);
    }

    void OnAchivementButton()
    {
        UISetter(1);
    }

    void UISetter(int live)
    {
        for(int i = 0; i < uis.Length; i++)
        {
            uis[i].gameObject.SetActive(false);
        }
        uis[live].gameObject.SetActive(true);
    }
}
