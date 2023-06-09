using UnityEngine;

[CreateAssetMenu (fileName = "Item", menuName = "Data/Item")]
public abstract class Item : ScriptableObject
{
    public string ItemName;
    public string ItemDesc;
    public Sprite ItemIcon;

    public abstract void AddItem();

    public abstract void RemoveItem();
}
