using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] Transform startPosition;

    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Data.Player.transform.position = startPosition.position;
        progress = 0.3f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.CreateInGameScene();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneInfoUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();
        progress = 0.6f;

        yield return new WaitForEndOfFrame();
        progress = 1f;
    }
}
