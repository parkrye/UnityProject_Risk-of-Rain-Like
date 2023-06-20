using UnityEngine.EventSystems;
using UnityEngine;

public class SkillListUI : SceneUI, IPointerEnterHandler, IPointerExitHandler
{
    public Skill skill;
    SkillDescUI skillDescUI;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public override void Initialize()
    {
        skillDescUI = GameManager.Resource.Load<SkillDescUI>("UI/SkillDescUI");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.UI.ShowPopupUI(skillDescUI);
        skillDescUI.Setting(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillDescUI.CloseUI();
    }
}
