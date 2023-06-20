using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] Transform startPosition;

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.CreateInGameScene();
        progress = 0.3f;
        GameManager.Data.Player.transform.position = startPosition.position;
        progress = 0.6f;
        yield return new WaitForEndOfFrame();
        progress = 1f;
    }
}
