using System.Collections;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    LoadingUI loadingUI;
    Canvas loadingCanvas;

    BaseScene curScene;

    public BaseScene CurScene
    {
        get
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();

            return curScene;
        }
    }

    void Awake()
    {
        loadingCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        loadingCanvas.gameObject.name = "LoadingCanvas";
        loadingCanvas.sortingOrder = 10;
        loadingCanvas.transform.SetParent(transform, false);

        LoadingUI _loadingUI = Resources.Load<LoadingUI>("UI/LoadingUI");
        loadingUI = Instantiate(_loadingUI);
        loadingUI.transform.SetParent(loadingCanvas.transform, false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.SetProgress(0f);
        loadingUI.FadeIn();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (!oper.isDone)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
            yield return null;
        }

        if (CurScene != null)
        {
            CurScene.LoadAsync();
            while (CurScene.progress < 1f)
            {
                loadingUI.SetProgress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
                yield return null;
            }
        }

        loadingUI.SetProgress(1f);
        loadingUI.FadeOut();
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
    }
}
