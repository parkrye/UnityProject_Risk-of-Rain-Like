using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.Scene.LoadScene("MainScene");
        GameManager.ResetSession();
    }
}
