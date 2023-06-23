using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPositionGimic : MonoBehaviour
{
    public GameObject Cover;    // 게임이 시작되면 사라지는 것

    public void SetGimic()
    {
        if (Cover != null)
        {
            Cover.SetActive(false);
        }
    }
}
