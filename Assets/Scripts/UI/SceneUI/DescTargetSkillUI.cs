using UnityEngine.EventSystems;

public class DescTargetSkillUI : DescTargetUI
{
    public Skill skill;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        descUI.Setting(skill);
    }
}
