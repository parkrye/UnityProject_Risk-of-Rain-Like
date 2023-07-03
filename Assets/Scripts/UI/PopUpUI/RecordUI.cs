using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        texts["StageText"].text += ((int)GameManager.Data.Records["Stage"]).ToString();
        texts["TimeText"].text += ((int)GameManager.Data.Records["Time"]).ToString();
        switch (GameManager.Data.Records["Difficulty"])
        {
            case 1f:
                texts["DifficultyText"].text += "Easy";
                break;
            case 2:
                texts["DifficultyText"].text += "Normal";
                break;
            case 3:
                texts["DifficultyText"].text += "Hard";
                break;
        }
        texts["KillText"].text += ((int)GameManager.Data.Records["Kill"]).ToString();
        texts["DamageText"].text += ((int)GameManager.Data.Records["Damage"]).ToString();
        texts["HitText"].text += ((int)GameManager.Data.Records["Hit"]).ToString();
        texts["HealText"].text += ((int)GameManager.Data.Records["Heal"]).ToString();
        texts["MoneyText"].text += ((int)GameManager.Data.Records["Money"]).ToString();
        texts["CostText"].text += ((int)GameManager.Data.Records["Cost"]).ToString();

        switch (GameManager.Data.Player.heroNum)
        {
            case 0:
                texts["HeroText"].text += "궁수";
                break;
            case 1:
                texts["HeroText"].text += "전사";
                break;
            case 2:
                texts["HeroText"].text += "마법사";
                break;
        }
        texts["LevelText"].text += GameManager.Data.Player.LEVEL;
        foreach (KeyValuePair<ItemData, int> item in GameManager.Data.Player.Inventory.GetInventory)
        {
            Image itemIcon = GameManager.Resource.Instantiate<Image>("UI/ItemImage", images["Items"].transform);
            itemIcon.sprite = item.Key.ItemIcon;
            if(item.Value > 1)
            {
                itemIcon.GetComponentInChildren<TextMeshProUGUI>().text = item.Value.ToString();
                break;
            }
        }

        buttons["RetryButton"].onClick.AddListener(RetryButton);
        buttons["TitleButton"].onClick.AddListener(TitleButton);

        CheckAchievement();
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    void RetryButton()
    {
        Time.timeScale = 1f;
        GameManager.ResetSession();
        GameManager.Scene.LoadScene("ReadyScene");
    }

    void TitleButton()
    {
        Time.timeScale = 1f;
        GameManager.Scene.LoadScene("TitleScene");
    }

    void CheckAchievement()
    {
        bool achive = false;

        if (GameManager.Data.Records["Stage"] > GameManager.Data.Achievement["StageCount"])
        {
            GameManager.Data.SetAchievement("StageCount", (int)GameManager.Data.Records["Stage"]);
            achive = true;
        }

        if (GameManager.Data.Records["Time"] > GameManager.Data.Achievement["TimeCount"])
        {
            GameManager.Data.SetAchievement("TimeCount", (int)GameManager.Data.Records["Time"]);
            achive = true;
        }

        if (GameManager.Data.Records["Kill"] > GameManager.Data.Achievement["KillCount"])
        {
            GameManager.Data.SetAchievement("KillCount", (int)GameManager.Data.Records["Kill"]);
            achive = true;
        }

        if (GameManager.Data.Records["Damage"] > GameManager.Data.Achievement["DamageCount"])
        {
            GameManager.Data.SetAchievement("DamageCount", (int)GameManager.Data.Records["Damage"]);
            achive = true;
        }

        if (GameManager.Data.Records["Hit"] > GameManager.Data.Achievement["HitCount"])
        {
            GameManager.Data.SetAchievement("HitCount", (int)GameManager.Data.Records["Hit"]);
            achive = true;
        }

        if (GameManager.Data.Records["Heal"] > GameManager.Data.Achievement["HealCount"])
        {
            GameManager.Data.SetAchievement("HealCount", (int)GameManager.Data.Records["Heal"]);
            achive = true;
        }

        if (GameManager.Data.Records["Money"] > GameManager.Data.Achievement["MoneyCount"])
        {
            GameManager.Data.SetAchievement("MoneyCount", (int)GameManager.Data.Records["Money"]);
            achive = true;
        }

        if (GameManager.Data.Records["Cost"] > GameManager.Data.Achievement["CostCount"])
        {
            GameManager.Data.SetAchievement("CostCount", (int)GameManager.Data.Records["Cost"]);
            achive = true;
        }

        if (GameManager.Data.Player.LEVEL > GameManager.Data.Achievement["LevelCount"])
        {
            GameManager.Data.SetAchievement("LevelCount", GameManager.Data.Player.LEVEL);
            achive = true;
        }

        if(achive)
            GameManager.Data.SaveAchiveMent();
    }
}
