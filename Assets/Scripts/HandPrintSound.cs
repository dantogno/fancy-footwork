using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPrintSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip laugh;

    private AudioSource audioSource;
    private BoxCollider triggerCollider;
    private bool hasBeenTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        triggerCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            audioSource.PlayOneShot(laugh);
            hasBeenTriggered = true;
        }
    }
}
