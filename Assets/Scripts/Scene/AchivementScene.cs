using System.Collections;
using UnityEngine;

public class AchivementScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Resource.Instantiate<GameObject>("Audio/BGM/BGM_Achivement");
        Progress = 0.3f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/AchivementUI").Initialize();
        Progress = 0.6f;

        yield return null;
        Progress = 1f;
    }
}
