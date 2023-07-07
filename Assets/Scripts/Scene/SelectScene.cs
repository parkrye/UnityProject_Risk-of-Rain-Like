using System.Collections;
using UnityEngine;

public class SelectScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Resource.Instantiate<GameObject>("Audio/BGM/BGM_Ready");
        Progress = 0.25f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        SelectUI selectUI = GameManager.UI.ShowSceneUI<SelectUI>("UI/SelectUI");
        Progress = 0.5f;

        selectUI.Initialize();
        selectUI.AddListener(GetComponent<SelectSceneAnimationController>().PlayAnimation);
        Progress = 0.75f;

        yield return null;
        Progress = 1f;
    }
}
