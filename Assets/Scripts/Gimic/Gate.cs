using UnityEngine;

public class Gate : MonoBehaviour
{
    public void MoveNextLevel()
    {
        GameManager.Data.RecordTime = false;

        string nextSceneName = GameManager.Scene.CurScene.name;
        for (int i = 0; i < 100 || nextSceneName == GameManager.Scene.CurScene.name; i++)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    nextSceneName = "LevelScene_Field";
                    break;
                case 1:
                    nextSceneName = "LevelScene_Castle";
                    break;
                case 2:
                    nextSceneName = "LevelScene_Viking";
                    break;
            }
        }
        GameManager.Data.NowRecords["Stage"] += 1f;
        GameManager.Scene.LoadScene(nextSceneName);
    }
}
