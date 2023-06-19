using UnityEngine.Events;

/// <summary>
/// 범용 예/아니오 팝업창
/// </summary>
public class YesNoPopUpUI : PopUpUI
{
    public UnityEvent YesEvent, NoEvent;

    protected override void Awake()
    {
        base.Awake();

        buttons["YesButton"].onClick.AddListener(YesButton);
        buttons["NoButton"].onClick.AddListener(NoButton);
    }

    /// <summary>
    /// YesEvent를 발동시키고 삭제
    /// </summary>
    public void YesButton()
    {
        YesEvent?.Invoke();
        CloseUI();
    }

    /// <summary>
    /// NoEvent를 발동시키고 삭제
    /// </summary>
    public void NoButton()
    {
        NoEvent?.Invoke();
        CloseUI();
    }
}
