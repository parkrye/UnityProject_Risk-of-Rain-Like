using System.Collections;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        yield return new WaitForEndOfFrame();
        Progress = 0.5f;
        GameManager.UI.ShowSceneUI<SceneUI>("UI/MainSceneUI");
        Progress = 1f;
    }
}
