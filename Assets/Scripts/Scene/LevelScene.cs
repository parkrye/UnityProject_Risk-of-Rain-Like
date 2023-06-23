using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] Transform startPosition;
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyLimit;

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Data.Player.transform.position = startPosition.position;
        progress = 0.25f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.CreateInGameScene();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneInfoUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();
        progress = 0.5f;

        if (spawnDelay == 0f)
            spawnDelay = 5f;
        if (spawnDistance == 0f)
            spawnDistance = 10f;
        gameObject.AddComponent<EnemySpawner>().Initialize(spawnDelay, spawnDistance, enemyLimit);
        progress = 0.7f;

        GetComponent<YLimiter>().Initialize();
        progress = 1f;

        GameManager.Data.RecordTime = true;
    }
}
