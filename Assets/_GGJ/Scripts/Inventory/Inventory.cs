using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemObjectEvent : UnityEvent<ItemObject> {}

public class Inventory : MonoBehaviour
{
    public List<ItemObject> items;
    [SerializeField]
    ItemObjectEvent OnItemAdded = default;

    public void Add(ItemObject item)
    {
        items.Add(item);
        OnItemAdded.Invoke(item);
    }
 }
