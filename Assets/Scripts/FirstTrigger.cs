using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    //[SerializeField]
    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;

    private void Awake()
    {
        triggerCollider = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            Pathfinder.calculatePath = true;
            hasBeenTriggered = true;
            Debug.Log("First trigger has been triggered.");
        }
    }
}
