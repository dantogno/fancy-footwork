using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    [SerializeField]
    private Light light1, light2, backLight, spotLight, tvLight;

    [SerializeField]
    private AudioClip lightsOff, lightsOn, laugh, firstVideo;

    [SerializeField]
    private float delay1 = 1.0f, delay2 = 1.0f, stutterDelay = 0.1f, tvFlickerDelay = 0.2f, tvFlickerIntensity = 1.0f;

    [SerializeField]
    private GameObject objectToActivate, ghostFigure;

    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;
    private bool shouldFlicker = false;
    private AudioSource audioSource;
    private float tvClipLength;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        triggerCollider = GetComponent<MeshCollider>();
        tvClipLength = firstVideo.length;
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
        ghostFigure.SetActive(true);
        audioSource.PlayOneShot(lightsOn, 1.0f);
        backLight.intensity = 1.0f;
        yield return new WaitForSeconds(stutterDelay);
        audioSource.PlayOneShot(lightsOff, 1.0f);
        backLight.intensity = 0.0f;
        yield return new WaitForSeconds(stutterDelay);
        backLight.intensity = 1.0f;
        audioSource.PlayOneShot(lightsOn, 1.0f);
        objectToActivate.SetActive(true);
        yield return new WaitForSeconds(stutterDelay);
        audioSource.PlayOneShot(lightsOff, 1.0f);
        backLight.intensity = 0.0f;
        ghostFigure.SetActive(false);
        yield return new WaitForSeconds(delay2);
        spotLight.intensity = 3.0f;
        light1.intensity = 0.2f;
        audioSource.PlayOneShot(lightsOn, 1.0f);
        audioSource.PlayOneShot(laugh, 1.0f);
        yield return new WaitForSeconds(delay1);
        audioSource.PlayOneShot(firstVideo);
        shouldFlicker = true;
        StartCoroutine(TVFlicker());
        yield return new WaitForSeconds(tvClipLength);
        shouldFlicker = false;

    }

    IEnumerator TVFlicker()
    {
        if (shouldFlicker)
        {
            tvLight.intensity = tvFlickerIntensity;
            yield return new WaitForSeconds(tvFlickerDelay);
            tvLight.intensity = 0.0f;
            yield return new WaitForSeconds(tvFlickerDelay);
            StartCoroutine(TVFlicker());
        }
    }
}
