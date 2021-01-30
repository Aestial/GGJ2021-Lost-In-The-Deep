using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSave : MonoBehaviour
{
    [SerializeField]
    ItemObject itemObject;

    Inventory inventory;

    public void Save() 
    {
        itemObject.Print();
        inventory.Add(itemObject);
    }

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();   
    }

}
