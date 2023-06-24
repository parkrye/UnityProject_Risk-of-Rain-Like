using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    static PoolManager poolManager;
    static ResourceManager resourceManager;
    static DataManager dataManager;
    static UIManager uiManager;
    static SceneManager sceneManager;
    static AudioManager audioManager;
    static ScriptManager scriptManager;

    public static GameManager Instance { get { return instance; } }
    public static PoolManager Pool { get { return poolManager; } }
    public static ResourceManager Resource { get { return resourceManager; } }
    public static DataManager Data { get { return dataManager; } }
    public static UIManager UI {  get { return uiManager; } }
    public static SceneManager Scene { get { return sceneManager; } }
    public static AudioManager Audio { get { return audioManager; } }
    public static ScriptManager Script { get { return scriptManager; } }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    void InitManagers()
    {
        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        resourceManager = resourceObj.AddComponent<ResourceManager>();
        resourceManager.Initialize();

        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        poolManager = poolObj.AddComponent<PoolManager>();
        poolManager.Initialize();

        GameObject dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform;
        dataManager = dataObj.AddComponent<DataManager>();
        dataManager.Initialize();

        GameObject uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform;
        uiManager = uiObj.AddComponent<UIManager>();
        uiManager.Initialize();

        GameObject sceneObj = new GameObject();
        sceneObj.name = "SceneManager";
        sceneObj.transform.parent = transform;
        sceneManager = sceneObj.AddComponent<SceneManager>();
        sceneManager.Initialize();

        GameObject audioObj = new GameObject();
        audioObj.name = "AudioManager";
        audioObj.transform.parent = transform;
        audioManager = audioObj.AddComponent<AudioManager>();
        audioManager.Initialize();

        GameObject scriptObj = new GameObject();
        scriptObj.name = "ScriptManager";
        scriptObj.transform.parent = transform;
        scriptManager = sceneObj.AddComponent<ScriptManager>();
        scriptManager.Initialize();
    }
}
