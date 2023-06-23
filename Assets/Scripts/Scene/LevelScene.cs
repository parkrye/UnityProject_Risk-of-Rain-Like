using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] Transform startPosition, summonPosition;
    StartPositionGimic[] startPositions;
    SummonPositionGimic[] summonPositions;
    [SerializeField] BossSummon bossSummonZone;
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyLimit;

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();
        startPositions = startPosition.GetComponentsInChildren<StartPositionGimic>();
        int spawnPosition = Random.Range(0, startPositions.Length);
        startPositions[spawnPosition].SetGimic();
        GameManager.Data.Player.transform.position = startPositions[spawnPosition].transform.position;
        progress = 0.1f;

        summonPositions = summonPosition.GetComponentsInChildren<SummonPositionGimic>();
        spawnPosition = Random.Range(0, summonPositions.Length);
        summonPositions[spawnPosition].SetGimic();
        bossSummonZone.transform.position = summonPositions[spawnPosition].transform.position;
        progress = 0.2f;

        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        SceneInfoUI infoUI = GameManager.UI.ShowSceneUI<SceneInfoUI>("UI/SceneInfoUI");
        infoUI.Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();
        progress = 0.4f;

        infoUI.UpdateObjective(SceneInfoUI.ObjectState.Search);
        bossSummonZone.ObjectStateEvent.AddListener(infoUI.UpdateObjective);
        progress = 0.6f;

        if (spawnDelay == 0f)
            spawnDelay = 5f;
        if (spawnDistance == 0f)
            spawnDistance = 10f;
        gameObject.AddComponent<EnemySpawner>().Initialize(spawnDelay, spawnDistance, enemyLimit);
        progress = 0.8f;

        GetComponent<YLimiter>().Initialize();
        progress = 1f;

        GameManager.Data.RecordTime = true;
    }
}
