using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDoorTrigger : MonoBehaviour
{
    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;

    public static event Action PlayerOpenedDoor;

    private void Awake()
    {
        triggerCollider = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            PlayerOpenedDoor?.Invoke();
            hasBeenTriggered = true;
        }
    }
}
