using UnityEngine;

public class Gate : MonoBehaviour
{
    public void MoveNextLevel()
    {
        GameManager.Data.RecordTime = false;
        GameManager.Scene.LoadScene("TitleScene");
        GameManager.Data.Initialize();
    }
}
