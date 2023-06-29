using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] Transform startPosition;
    StartPositionGimic[] startPositions;
    [SerializeField] SummonPositionGimic summonPosition;
    [SerializeField] BossSummon bossSummonZone;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] ItemDropper itemDropper;
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyLimit;

    public enum LevelState { Search, Keep, ComeBack, Fight, Win }

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
        Progress = 0.1f;

        // 보스존 위치 설정
        summonPosition.SetGimic();
        Progress = 0.2f;

        // UI 세팅
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        SceneInfoUI infoUI = GameManager.UI.ShowSceneUI<SceneInfoUI>("UI/SceneInfoUI");
        infoUI.Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();
        infoUI.UpdateObjective(LevelState.Search);
        Progress = 0.4f;

        // 에너미 스폰 설정
        if (spawnDelay == 0f)
            spawnDelay = 5f;
        if (spawnDistance == 0f)
            spawnDistance = 10f;
        enemySpawner.Initialize(spawnDelay, spawnDistance, enemyLimit);
        Progress = 0.6f;

        // 이벤트 세팅
        bossSummonZone.ObjectStateEvent.AddListener(infoUI.UpdateObjective);
        bossSummonZone.ObjectStateEvent.AddListener(enemySpawner.StopSpawn);
        bossSummonZone.ObjectStateEvent.AddListener(itemDropper.StopDrop);
        Progress = 0.8f;

        // 플레이어 세팅
        GetComponent<YLimiter>().Initialize();
        GameManager.Data.Player.controllable = true;
        GameManager.Data.RecordTime = true;
        Progress = 1f;
    }
}
