using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    Image image = default;
    
    public void Set(ItemObject item)
    {
        image.sprite = item.icon;
    }
}
