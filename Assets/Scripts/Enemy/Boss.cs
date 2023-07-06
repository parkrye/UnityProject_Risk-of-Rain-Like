using UnityEngine;

public abstract class Boss : Enemy
{
    protected Transform playerTransform;
    protected enum LifeMode { Full, Half }
    protected LifeMode lifeMode;
    protected enum RangeMode { NearRange, FarRange }
    protected RangeMode rangeMode;

    protected bool patternCoroutineIsRunning;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameManager.Data.Player.playerTransform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        lifeMode = LifeMode.Full;
        rangeMode = RangeMode.NearRange;
        patternCoroutineIsRunning = false;
    }

    public void ChangeToNear()
    {
        rangeMode = RangeMode.NearRange;
    }

    public void ChangeToFar()
    {
        rangeMode = RangeMode.FarRange;
    }

    public void ChangeToCritical()
    {
        lifeMode = LifeMode.Half;
    }
}
