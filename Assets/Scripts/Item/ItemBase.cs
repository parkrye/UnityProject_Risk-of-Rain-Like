using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Item", menuName = "Data/Item")]
public class ItemBase : ScriptableObject
{
    public string ItemName;
    public string ItemDesc;
    public Sprite ITemImag;

    public virtual void AddItem()
    {

    }

    public virtual void RemoveItem()
    {

    }
}
