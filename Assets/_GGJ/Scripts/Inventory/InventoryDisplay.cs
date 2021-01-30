using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    RectTransform container = default;
    
    [SerializeField]
    GameObject boxPrefab = default;

    public void OnAdded(ItemObject item)
    {
        var itemGO = Instantiate(boxPrefab, container);
        var itemDisplay = itemGO.GetComponent<ItemDisplay>();
        itemDisplay.Set(item);
    }
}
