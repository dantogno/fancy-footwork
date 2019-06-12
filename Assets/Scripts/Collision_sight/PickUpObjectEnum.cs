using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpObjectEnum 
{
    Key,
}

public struct ObjectToPutInInventory
{
    public string tag { get; set; }

    public PickUpObjectEnum objectType { get; set; }

    public GameObject objectToUseItemOn { get; set; }
}
