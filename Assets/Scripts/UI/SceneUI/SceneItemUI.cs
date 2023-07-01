using TMPro;
using UnityEngine.UI;

public class SceneItemUI : SceneUI
{
    public override void Initialize()
    {
        GameManager.Data.Player.Inventory.ItemEvent.RemoveAllListeners();
        GameManager.Data.Player.Inventory.ItemEvent.AddListener(AddItem);
    }

    /// <summary>
    /// 아이템 추가
    /// 중복시 아이템 우하단에 숫자
    /// </summary>
    public void AddItem(ItemData itemData, int quantity)
    {
        if(quantity == 1)
        {
            Image newItemImage =  GameManager.Resource.InstantiateDontDestroyOnLoad<Image>("UI/ItemImage", transform);
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
