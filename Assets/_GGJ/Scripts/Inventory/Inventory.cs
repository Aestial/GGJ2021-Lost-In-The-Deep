using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemObjectEvent : UnityEvent<ItemObject> {}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    ItemObjectEvent OnItemCollected = default;
    [SerializeField]
    ItemObjectEvent OnItemRegistered = default;
    [SerializeField]
    UnityEvent OnCollectedAll = default;
    
    [Header("Debug Inspector")]
    public List<ItemObject> items;
    public List<ItemObject> collected;

    public void Collect(ItemObject item)
    {
        collected.Add(item);
        OnItemCollected.Invoke(item);
        VerifyCollection(collected.Count);
    }
    public void Register(ItemObject item)
    {
        items.Add(item);
        OnItemRegistered.Invoke(item);
    }
    private void Start() 
    {
        // Debug.Log($"Inventory: Found {items.Count} Items");
    }
    private void VerifyCollection(int length)
    {
        if (length >= items.Count)
            OnCollectedAll.Invoke();
    }
 }
