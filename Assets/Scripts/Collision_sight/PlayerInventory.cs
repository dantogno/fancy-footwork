using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ObjectToPutInInventory> inventory;

    private void Start()
    {
        inventory = new List<ObjectToPutInInventory>();
    }

    public void AddItemToInventory(ObjectToPutInInventory item)
    {
        inventory.Add(item);
    }

    public void AddItemToInventory(string tag, PickUpObjectEnum type, GameObject goalObject)
    {
        ObjectToPutInInventory temp = new ObjectToPutInInventory();
        temp.tag = tag;
        temp.objectType = type;
        temp.objectToUseItemOn = goalObject;
        AddItemToInventory(temp);
    }
    
}
