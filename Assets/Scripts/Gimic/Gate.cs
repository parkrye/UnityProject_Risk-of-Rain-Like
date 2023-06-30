using UnityEngine;

public class Gate : MonoBehaviour
{
    public void MoveNextLevel()
    {
        GameManager.Data.RecordTime = false;

        switch (Random.Range(0, 2))
        {
            case 0:
                GameManager.Scene.LoadScene("LevelScene_Field");
                break;
            case 1:
                GameManager.Scene.LoadScene("LevelScene_Castle");
                break;
        }
    }
}
