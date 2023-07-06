using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SceneItemUI : SceneUI
{
    public override void Initialize()
    {
        GameManager.Data.Player.Inventory.ItemEvent.RemoveAllListeners();
        GameManager.Data.Player.Inventory.ItemEvent.AddListener(AddItem);
    }

    void OnEnable()
    {
        foreach(KeyValuePair<ItemData, int> pair in GameManager.Data.Player.Inventory.GetInventory)
        {
            Image newItemImage = GameManager.Resource.Instantiate<Image>("UI/ItemImage", transform);
            newItemImage.sprite = pair.Key.ItemIcon;
            newItemImage.name = pair.Key.ItemName;
            newItemImage.GetComponent<DescTargetItemUI>().item = pair.Key;
            if (pair.Value > 1)
            {
                newItemImage.GetComponentInChildren<TextMeshProUGUI>().text = pair.Value.ToString();
            }
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
            newItemImage.GetComponent<DescTargetItemUI>().item = itemData;
        }
        else
        {
            Image[] images = GetComponentsInChildren<Image>();
            for(int i = 0; i < images.Length; i++)
            {
                if (images[i].name == itemData.ItemName)
                {
                    images[i].GetComponentInChildren<TextMeshProUGUI>().text = quantity.ToString();
                    return;
                }
            }
        }
    }
}
