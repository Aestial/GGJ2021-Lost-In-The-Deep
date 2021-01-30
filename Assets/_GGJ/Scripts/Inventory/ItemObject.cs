using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName="Inventory/Item")]
public class ItemObject : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;

    public void Print()
    {
        Debug.Log($"Item: {name}");
    }
}
