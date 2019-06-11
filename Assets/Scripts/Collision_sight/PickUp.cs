using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Interactable interact;
    public GameObject player=null;

    public PickUpObjectEnum objectType;

    public GameObject objectToUseItemOn;

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interactable>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(interact.triggered)
        {
            PickedUp();
        }
    }

    private void PickedUp()
    {
        ObjectToPutInInventory tempItem = new ObjectToPutInInventory();
        player.GetComponent<PlayerInventory>().AddItemToInventory(tempItem.tag,objectType, objectToUseItemOn);
        gameObject.SetActive(false);
    }
}
