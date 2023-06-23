using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    void Awake()
    {
        GameManager.UI.CreatePopupCanvas();
        GameManager.UI.CreateSceneCanvas();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneInfoUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneItemUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneKeyUI").Initialize();
        GameManager.UI.ShowSceneUI<SceneUI>("UI/SceneStatusUI").Initialize();

        GameManager.Data.Player.SelectHero(GameManager.Data.Player.heroNum);
        GameManager.Data.Player.hero.SettingSkill(1, 1);
        GameManager.Data.Player.hero.SettingSkill(2, 1);
        GameManager.Data.Player.hero.SettingSkill(3, 1);
        GameManager.Data.Player.hero.SettingSkill(4, 1);
    }
}
