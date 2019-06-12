using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    [SerializeField]
    private Light light1, light2, spotLight;

    [SerializeField]
    private AudioClip lightsOff, lightsOn;

    [SerializeField]
    private float delay1 = 1.0f, delay2 = 1.0f;

    [SerializeField]
    private GameObject objectToActivate;

    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        triggerCollider = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            Pathfinder.calculatePath = true;
            hasBeenTriggered = true;
            Debug.Log("First trigger has been triggered.");
            StartCoroutine(TurnLights());
        }
    }

    IEnumerator TurnLights()
    {
        light1.intensity = 0.0f;
        light2.intensity = 0.0f;
        audioSource.PlayOneShot(lightsOff, 1.0f);
        yield return new WaitForSeconds(delay1);
        objectToActivate.SetActive(true);
        spotLight.intensity = 3.5f;
        audioSource.PlayOneShot(lightsOn, 1.0f);

    }
}
