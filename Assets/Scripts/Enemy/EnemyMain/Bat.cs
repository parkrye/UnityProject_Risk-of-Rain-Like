using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyBase
{
    void Awake()
    {
        GameManager.Data.Enemy.AddData("Bat", data);
    }
}
