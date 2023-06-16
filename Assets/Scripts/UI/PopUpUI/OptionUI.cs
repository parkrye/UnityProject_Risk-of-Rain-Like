using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["DoneButton"].onClick.AddListener(() => { CloseUI(); });
        buttons["ResetButton"].onClick.AddListener(() => { CloseUI(); });
    }
}
