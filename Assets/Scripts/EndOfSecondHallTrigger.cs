using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndOfSecondHallTrigger : MonoBehaviour
{
    [SerializeField]
    private Light light1, light2, light3;

    [SerializeField]
    private float lightDelay = 1.0f;

    [SerializeField]
    private Light[] secondHallLights = new Light[8];

    [SerializeField]
    private HauntedMovingObject luggageCart;

    [SerializeField]
    private GameObject luggageCartObject;

    [SerializeField]
    private AudioClip lightsOff, lightsOn;

    private MeshCollider triggerCollider;
    private AudioSource audioSource;
    private bool hasBeenTriggered = false;
    private bool shouldTrigger = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        triggerCollider = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered && shouldTrigger)
        {
            StartCoroutine(TurnLights());
            ActivateFootsteps();
            hasBeenTriggered = true;
        }

        if(other.tag == "Player" && !shouldTrigger)
        {
            shouldTrigger = true;
        }
    }

    private void MoveCart()
    {
        luggageCartObject.SetActive(true);
        luggageCart.objectShouldMove = true;
    }

    private void ActivateFootsteps()
    {

    }

    IEnumerator TurnLights()
    {
        audioSource.PlayOneShot(lightsOff);

        for (int i = 0; i < 8; i++)
        {
            secondHallLights[i].intensity = 0.0f;
        }

        light1.intensity = 0.0f;
        light2.intensity = 0.0f;
        light3.intensity = 0.0f;
        yield return new WaitForSeconds(lightDelay);
        MoveCart();
        light3.intensity = 0.2f;
        audioSource.PlayOneShot(lightsOn);
        yield return new WaitForSeconds(0.2f);
        light3.intensity = 0.0f;
        audioSource.PlayOneShot(lightsOff);
        yield return new WaitForSeconds(0.2f);
        light3.intensity = 0.2f;
        audioSource.PlayOneShot(lightsOn);
    }
}
