using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    EventSystem eventSystem;
    public Canvas sceneCanvas, popUpCanvas;
    Stack<PopUpUI> popUpStack;

    void Awake()
    {
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
        eventSystem.transform.parent = transform;
    }

    public void CreateSceneCanvas()
    {
        sceneCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        sceneCanvas.gameObject.name = "SceneCanvas";
        sceneCanvas.sortingOrder = 1;
    }

    public void CreatePopupCanvas()
    {
        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        popUpCanvas.gameObject.name = "PopupCanvas";
        popUpCanvas.sortingOrder = 5;

        popUpStack = new Stack<PopUpUI>();
    }

    public T GetPlayerSceneUI<T>() where T : SceneUI
    {
        T ui = sceneCanvas.GetComponentInChildren<T>();
        return ui;
    }

    // ���� PopUpUI

    public T ShowPopupUI<T>(T popup) where T : PopUpUI
    {
        if (popUpStack.Count > 0)
        {
            popUpStack.Peek().gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI<T>(popup);
        ui.transform.SetParent(popUpCanvas.transform, false);

        popUpStack.Push(ui);

        return ui;
    }

    public T ShowPopupUI<T>(string path) where T : PopUpUI
    {
        T uI = GameManager.Resource.Load<T>(path);
        return ShowPopupUI(uI);
    }

    public void ClosePopupUI()
    {
        GameManager.Pool.ReleaseUI(popUpStack.Pop());

        if (popUpStack.Count > 0)
        {
            popUpStack.Peek().gameObject.SetActive(true);
        }
    }

    // ���� SceneUI
    public T ShowSceneUI<T>(T sceneUI) where T : SceneUI
    {
        T ui = GameManager.Pool.GetUI(sceneUI);
        ui.transform.SetParent(sceneCanvas.transform, false);

        return ui;
    }

    public T ShowSceneUI<T>(string path) where T : SceneUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowSceneUI(ui);
    }

    public void CloseSceneUI(SceneUI sceneUI)
    {
        GameManager.Pool.Release(sceneUI);
    }
}
