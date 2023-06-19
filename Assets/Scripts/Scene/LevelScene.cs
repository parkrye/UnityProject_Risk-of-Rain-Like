using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.CreateInGameScene();
        progress = 0.3f;
        /*
         * 
        GameObject player = GameManager.Resource.Load<GameObject>("Player/Player");
        player.GetComponent<PlayerDataModel>().SelectHero();
         *
         */
        progress = 0.6f;
        yield return new WaitForEndOfFrame();
        progress = 1f;
    }
}
