using System.Collections;
using UnityEngine;

public class LevelScene : BaseScene
{
    [SerializeField] StartPositionGimic startPositionGimic;
    [SerializeField] BossSummon bossSummonZone;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] ItemDropper itemDropper;
    [SerializeField] GameObject bossZone;
    [SerializeField] GameObject directionalLight;
    [SerializeField] float spawnDelay, spawnDistance;
    [SerializeField] int enemyLimit;

    public enum LevelState { Search, Keep, ComeBack, Fight, Win }

    protected override IEnumerator LoadingRoutine()
    {
        // 플레이어 조작 제한
        GameManager.Data.Player.controllable = false;

        // 플레이어, 보스존 시작 위치 설정
        startPositionGimic = GetComponent<StartPositionGimic>();
        startPositionGimic.SetGimic(GameManager.Data.Player.gameObject, 1f);
        startPositionGimic.SetGimic(bossZone, 2f);
        Progress = 0.2f;

        // UI 세팅
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.CreatePopupCanvas();
        SceneInfoUI infoUI = GameManager.UI.ShowSceneUI<SceneInfoUI>("UI/SceneInfoUI");
        infoUI.Initialize();
        infoUI.UpdateObjective(LevelState.Search);
        GameManager.UI.ShowSceneUI<SceneItemUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneKeyUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneStatusUI>("UI/SceneStatusUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneMinimapUI>("UI/SceneMinimapUI").Initialize();
        Progress = 0.4f;

        // 에너미 스폰 설정
        enemySpawner = GetComponent<EnemySpawner>();
        if (spawnDelay == 0f)
            spawnDelay = 10f;
        if (spawnDistance == 0f)
            spawnDistance = 10f;
        if (enemyLimit == 0)
            enemyLimit = 5;
        enemySpawner.Initialize(spawnDelay, spawnDistance, enemyLimit);
        Progress = 0.6f;

        // 이벤트 세팅
        itemDropper = GetComponent<ItemDropper>();
        bossSummonZone.ObjectStateEvent.AddListener(infoUI.UpdateObjective);
        bossSummonZone.ObjectStateEvent.AddListener(enemySpawner.StopSpawn);
        bossSummonZone.ObjectStateEvent.AddListener(itemDropper.StopDrop);
        Progress = 0.8f;

        // 레벨 배경 설정
        Material[] skyMaterials = GameManager.Resource.LoadAll<Material>("SkyBox");
        RenderSettings.skybox = skyMaterials[Random.Range(0, skyMaterials.Length)];
        directionalLight.transform.localEulerAngles = new Vector3(Random.Range(30f, 150f), directionalLight.transform.localEulerAngles.y, directionalLight.transform.localEulerAngles.z);
        switch (Random.Range(0, 10))
        {
            default:
                break;
            case 0:
                GameManager.Resource.Instantiate<PositionFixer>("Particle/Dust").SetTarget(GameManager.Data.Player.playerTransform);
                break;
            case 1:
                GameManager.Resource.Instantiate<PositionFixer>("Particle/Fog").SetTarget(GameManager.Data.Player.playerTransform);
                break;
            case 2:
                GameManager.Resource.Instantiate<PositionFixer>("Particle/Rain").SetTarget(GameManager.Data.Player.playerTransform);
                break;
        }

        // 플레이어 세팅
        GetComponent<YLimiter>().Initialize();
        GameManager.Data.Player.controllable = true;
        GameManager.Data.Player.onSession = true;
        GameManager.Data.RecordTime = true;
        GameManager.Resource.Instantiate<MinimapMarker>("Marker/MinimapMarker_Player", true).StartFollowing(GameManager.Data.Player.playerTransform);

        yield return new WaitForEndOfFrame();
        Progress = 1f;
    }
}
