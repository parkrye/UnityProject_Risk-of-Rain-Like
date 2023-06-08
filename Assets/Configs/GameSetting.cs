using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        InitGameManager();
    }

    static void InitGameManager()
    {
        if (GameManager.Instance == null)
        {
            GameObject gameManager = new()
            {
                name = "GameManager"
            };

            gameManager.AddComponent<GameManager>();
        }
    }
}
