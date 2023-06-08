using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    EventSystem eventSystem;
    Canvas sceneCanvas, ingameCanvas, popUpCanvas;
    Stack<PopUpUI> popUpStack;

    void Awake()
    {
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
        eventSystem.transform.parent = transform;

        ingameCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        ingameCanvas.gameObject.name = "InGameCanvas";
        ingameCanvas.sortingOrder = 0;

        sceneCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        sceneCanvas.gameObject.name = "SceneCanvas";
        sceneCanvas.sortingOrder = 1;

        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        popUpCanvas.gameObject.name = "PopupCanvas";
        popUpCanvas.sortingOrder = 100;

        popUpStack = new Stack<PopUpUI>();
    }

    public T ShowPopupUI<T>(T popup) where T : PopUpUI
    {
        if (popUpStack.Count > 0)
        {
            popUpStack.Peek().gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI<T>(popup);
        ui.transform.SetParent(popUpCanvas.transform, false);

        popUpStack.Push(ui);

        Time.timeScale = 0f;

        return ui;
    }

    public T ShowPopupUI<T>(string path) where T : PopUpUI
    {
        T uI = GameManager.Resource.Load<T>(path);
        return ShowPopupUI(uI);
    }

    public void ClosePopupUI()
    {
        GameManager.Pool.Release(popUpStack.Pop());

        if (popUpStack.Count == 0)
            Time.timeScale = 1f;

        if (popUpStack.Count > 0)
        {
            popUpStack.Peek().gameObject.SetActive(true);
        }
    }

    public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
    {
        T ui = GameManager.Pool.GetUI(inGameUI);
        ui.transform.SetParent(inGameUI.transform, false);

        return ui;
    }

    public T ShowInGameUI<T>(string path) where T : InGameUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowInGameUI(ui);
    }

    public void CloseInGameUI(InGameUI inGameUI)
    {
        GameManager.Pool.Release(inGameUI);
    }
}
