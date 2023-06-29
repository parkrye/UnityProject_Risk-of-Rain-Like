using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
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
        texts["DamageText"].text += GameManager.Data.Records["Damage"].ToString();
        texts["HealText"].text += GameManager.Data.Records["Heal"].ToString();
        texts["HitText"].text += GameManager.Data.Records["Hit"].ToString();

        switch (GameManager.Data.Player.heroNum)
        {
            case 0:
                texts["HeroText"].text = "Archer";
                break;
            case 1:
                texts["HeroText"].text = "Warrior";
                break;
            case 2:
                texts["HeroText"].text = "Wizard";
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
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    void RetryButton()
    {
        Debug.Log("Retry");
        Time.timeScale = 1f;
        GameManager.ResetSession();
        GameManager.Scene.LoadScene("ReadyScene");
    }

    void TitleButton()
    {
        Debug.Log("Title");
        Time.timeScale = 1f;
        GameManager.Scene.LoadScene("TitleScene");
    }
}
