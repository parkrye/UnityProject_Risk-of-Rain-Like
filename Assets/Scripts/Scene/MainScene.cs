using System.Collections;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Resource.Instantiate<GameObject>("Audio/BGM/BGM_Main");
        Progress = 0.3f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        Progress = 0.6f;
        GameManager.UI.ShowSceneUI<SceneUI>("UI/MainSceneUI");

        yield return new WaitForEndOfFrame();
        Progress = 1f;
    }
}
