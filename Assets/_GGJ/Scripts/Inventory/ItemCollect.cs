using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] 
    ItemObject itemObject = default;

    Inventory inventory;

    public void Collect() 
    {
        itemObject.Print();
        inventory.Collect(itemObject);
    }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.Register(this.itemObject);
    }

}
