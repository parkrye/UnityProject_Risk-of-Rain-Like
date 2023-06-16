using UnityEngine;

[CreateAssetMenu(fileName = "ITem", menuName = "Data/ITem")]
public class ItemData : ScriptableObject
{
    public GameObject ItemObject;

    public string ItemName;
    public string ItemDesc;
    public Sprite ItemIcon;
}
