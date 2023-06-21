using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string ItemName;
    public string ItemDesc;
    public Sprite ItemIcon;

    public abstract void GetFirstEffect();

    public abstract void GetNextEffect();
}
