using UnityEngine.EventSystems;

public class DescTargetDifficultyUI : DescTargetUI
{
    public Icon difficulty;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        descUI.Setting(difficulty.desc);
    }
}
