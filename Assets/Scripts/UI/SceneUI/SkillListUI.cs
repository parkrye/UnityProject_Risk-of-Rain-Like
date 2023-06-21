using UnityEngine.EventSystems;
using UnityEngine;

public class SkillListUI : SceneUI, IPointerEnterHandler, IPointerExitHandler
{
    public Skill skill;
    SkillDescUI skillDescUI;
    Vector3 posOffset;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public override void Initialize()
    {
        skillDescUI = GameManager.Resource.Load<SkillDescUI>("UI/SkillDescUI");
        posOffset = new Vector3(100f, 0f, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillDescUI = GameManager.UI.ShowPopupUI(skillDescUI);

        skillDescUI.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position + posOffset;

        skillDescUI.Setting(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillDescUI.CloseUI();
    }
}
