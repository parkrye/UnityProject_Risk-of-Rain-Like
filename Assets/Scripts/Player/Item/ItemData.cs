using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public GameObject ItemObject;

    public string ItemName;
    public string ItemDesc;
    public Sprite ItemIcon;

    public abstract void GetFirstEffect();

    public abstract void GetNextEffect();
}
