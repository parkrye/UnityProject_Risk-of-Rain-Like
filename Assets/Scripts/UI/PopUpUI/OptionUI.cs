using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["DoneButton"].onClick.AddListener(OnDoneButton);
        buttons["ResetButton"].onClick.AddListener(OnResetButton);
    }

    void OnDoneButton()
    {
        YesNoPopUpUI yesNoPopUpUI = GameManager.UI.ShowPopupUI<YesNoPopUpUI>("UI/YesNoUI");
        yesNoPopUpUI.SetText(0, "Set Options?");
        yesNoPopUpUI.SetText(1, "Yes");
        yesNoPopUpUI.SetText(2, "No");
        yesNoPopUpUI.YesEvent.RemoveAllListeners();
        yesNoPopUpUI.NoEvent.RemoveAllListeners();
        yesNoPopUpUI.YesEvent.AddListener(SetOptions);
    }

    void OnResetButton()
    {
        YesNoPopUpUI yesNoPopUpUI = GameManager.UI.ShowPopupUI<YesNoPopUpUI>("UI/YesNoUI");
        yesNoPopUpUI.SetText(0, "Reset Options?");
        yesNoPopUpUI.SetText(1, "Yes");
        yesNoPopUpUI.SetText(2, "No");
        yesNoPopUpUI.YesEvent.RemoveAllListeners();
        yesNoPopUpUI.NoEvent.RemoveAllListeners();
        yesNoPopUpUI.YesEvent.AddListener(ResetOptions);
    }

    void SetOptions()
    {
        CloseUI();
    }

    void ResetOptions()
    {

    }
}
