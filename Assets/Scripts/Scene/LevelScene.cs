using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : BaseScene
{

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();

        progress = 1f;
    }
}
