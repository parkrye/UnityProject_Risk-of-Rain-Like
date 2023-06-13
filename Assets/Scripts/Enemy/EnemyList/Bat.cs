using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        enemyData = GameManager.Resource.Load<EnemyData>("Enemy/Bat");
    }
}
