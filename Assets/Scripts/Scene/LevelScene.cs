using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] Transform startPosition;
    StartPositionGimic[] startPositions;
    [SerializeField] SummonPositionGimic summonPosition;
    [SerializeField] BossSummon bossSummonZone;
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyLimit;

    protected override IEnumerator LoadingRoutine()
    {
        // 플레이어 조작 제한
        GameManager.Data.Player.controllable = false;
        yield return new WaitForEndOfFrame();

        // 플레이어 시작 위치 설정
        startPositions = startPosition.GetComponentsInChildren<StartPositionGimic>();
        int spawnPosition = Random.Range(0, startPositions.Length);
        startPositions[spawnPosition].SetGimic();
        GameManager.Data.Player.transform.position = startPositions[spawnPosition].transform.position;
        progress = 0.1f;

        // 보스존 위치 설정
        summonPosition.SetGimic();
        progress = 0.2f;

        // UI 세팅
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        SceneInfoUI infoUI = GameManager.UI.ShowSceneUI<SceneInfoUI>("UI/SceneInfoUI");
        infoUI.Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();
        progress = 0.4f;

        // 목적 UI 세팅
        infoUI.UpdateObjective(SceneInfoUI.ObjectState.Search);
        bossSummonZone.ObjectStateEvent.AddListener(infoUI.UpdateObjective);
        progress = 0.6f;

        // 에너미 스폰 설정
        if (spawnDelay == 0f)
            spawnDelay = 5f;
        if (spawnDistance == 0f)
            spawnDistance = 10f;
        gameObject.AddComponent<EnemySpawner>().Initialize(spawnDelay, spawnDistance, enemyLimit);
        progress = 0.8f;

        // 플레이어 세팅
        GetComponent<YLimiter>().Initialize();
        GameManager.Data.Player.controllable = true;
        GameManager.Data.RecordTime = true;
        progress = 1f;
    }
}
