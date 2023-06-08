using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float hitPoint, exp;
    public int level;

    private void Awake()
    {
        hitPoint = 100f;
        level = 1;
        exp = 0f;
    }
}
