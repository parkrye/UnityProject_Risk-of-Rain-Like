using UnityEngine.EventSystems;

public class DescTargetItemUI : DescTargetUI
{
    public ItemData item;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        descUI.Setting(item);
    }
}
