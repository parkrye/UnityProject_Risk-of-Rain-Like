using System.Collections;
using UnityEngine;

public class ReadyScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.ShowSceneUI<SelectUI>("UI/SelectUI").Initialize();
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            Progress += 0.01f;
        }
        Progress = 1f;
    }
}
