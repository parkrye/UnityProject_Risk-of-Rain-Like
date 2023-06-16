using System.Collections;
using UnityEngine;

public class TitleScene : BaseScene
{
    public void OnStartButton()
    {
        GameManager.Scene.LoadScene("SelectScene");
    }

    public void OnOptionButton()
    {
        GameManager.UI.ShowPopupUI<PopUpUI>("UI/OptionUI");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();
        progress = 1f;
    }
}
