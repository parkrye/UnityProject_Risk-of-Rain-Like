using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionGimic : MonoBehaviour
{
    public GameObject Door;    // 게임이 시작되면 사라지는 것

    public void SetGimic()
    {
        if(Door != null)
        {
            Door.SetActive(false);
        }
    }
}
