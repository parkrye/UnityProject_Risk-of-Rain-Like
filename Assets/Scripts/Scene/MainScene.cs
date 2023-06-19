using System.Collections;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        yield return new WaitForEndOfFrame();
        progress = 0.5f;
        GameManager.UI.ShowSceneUI<SceneUI>("UI/MainSceneUI");
        progress = 1f;
    }
}
