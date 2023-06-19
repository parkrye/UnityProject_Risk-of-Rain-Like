using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.Scene.LoadScene("MainScene");
    }
}
