using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoaderForTest : MonoBehaviour
{
    void Awake()
    {
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneInfoUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();
    }
}
