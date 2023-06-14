using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForEndOfFrame();
        progress = 1f;
    }
}
