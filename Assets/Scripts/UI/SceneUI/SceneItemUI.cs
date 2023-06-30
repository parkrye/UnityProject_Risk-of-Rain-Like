using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SceneItemUI : SceneUI
{
    public override void Initialize()
    {
        GameManager.Data.Player.Inventory.ItemEvent.AddListener(AddItem);
        Setting();
    }

    void Setting()
    {
        foreach (KeyValuePair<ItemData, int> item in GameManager.Data.Player.Inventory.GetInventory)
        {
            AddItem(item.Key, item.Value);
        }
    }

    /// <summary>
    /// 아이템 추가
    /// 중복시 아이템 우하단에 숫자
    /// </summary>
    public void AddItem(ItemData itemData, int quantity)
    {
        if(quantity == 1)
        {
            Image newItemImage =  GameManager.Resource.Instantiate<Image>("UI/ItemImage", transform);
            newItemImage.sprite = itemData.ItemIcon;
            newItemImage.name = itemData.ItemName;
        }
        else
        {
            Image[] images = GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                if(image.name == itemData.ItemName)
                {
                    image.GetComponentInChildren<TextMeshProUGUI>().text = quantity.ToString();
                    return;
                }
            }
        }
    }
}
