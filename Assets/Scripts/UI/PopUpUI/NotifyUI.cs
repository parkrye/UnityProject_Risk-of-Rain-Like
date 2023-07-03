public class NotifyUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["NotifyButton"].onClick.AddListener(() => { CloseUI(); });
    }

    public void SetText(string text)
    {
        texts["NotifyText"].text = text;
    }
}
