using System.Collections;
using UnityEngine;

public class ReadyScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Resource.Instantiate<GameObject>("Audio/BGM/BGM_Ready");
        Progress = 0.3f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.ShowSceneUI<SelectUI>("UI/SelectUI").Initialize();
        Progress = 0.6f;

        yield return new WaitForEndOfFrame();
        Progress = 1f;
    }
}
