using UnityEngine.EventSystems;
using UnityEngine;

public class DescTargetUI : SceneUI, IPointerEnterHandler, IPointerExitHandler
{
    protected DescUI descUI;
    protected Vector3 posOffset;
    RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public override void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        descUI = GameManager.Resource.Load<DescUI>("UI/DescUI");
        posOffset = new Vector3(100f, 50f, 0f);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        descUI = GameManager.UI.ShowPopupUI(descUI);

        Vector3 descPos = rectTransform.position + posOffset;
        Vector2 descSize = descUI.GetComponent<RectTransform>().sizeDelta;
        if (descPos.x + descSize.x > Screen.width)
        {
            descPos.x -= rectTransform.sizeDelta.x + descSize.x;
        }
        if (descPos.y - descSize.y < 0)
        {
            descPos.y = rectTransform.sizeDelta.y + descSize.y;
        }

        descUI.GetComponent<RectTransform>().position = descPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descUI.CloseUI();
    }
}
