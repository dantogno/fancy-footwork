using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SecondTrigger : MonoBehaviour
{
    [SerializeField]
    private Light[] firstHallLights = new Light[6];

    [SerializeField]
    private Light[] secondHallLights = new Light[8];

    [SerializeField]
    private GameObject doorToSlam;

    [SerializeField]
    private Transform doorEndingRotation;

    [SerializeField]
    private float slamTime = 0.5f, slamDelay = 1.0f;

    [SerializeField]
    private AudioClip lightsOff, doorSlam;

    private float[] secondHallLightsIntensity = new float[8];
    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;
    private AudioSource audioSource;
    private Transform doorStartingRotation;
    private float currentLerpTime;
    private bool shouldSlam = false;

    private void Awake()
    {
        doorStartingRotation = doorToSlam.transform;
        triggerCollider = GetComponent<MeshCollider>();
        audioSource = GetComponent<AudioSource>();
        GetLightIntensity();
    }

    private void Update()
    {
        if (shouldSlam)
        {
            SlamDoor();
        }
    }

    private void GetLightIntensity()
    {
        for (int k = 0; k < 8; k++)
        {
            secondHallLightsIntensity[k] = secondHallLights[k].intensity;
            secondHallLights[k].intensity = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenTriggered)
        {
            TurnLights();
            StartCoroutine(DelaySlamDoor());
            hasBeenTriggered = true;
        }
    }

    private void TurnLights()
    {
        audioSource.PlayOneShot(lightsOff);

        for (int i = 0; i < 6; i++)
        {
            firstHallLights[i].intensity = 0.0f;
        }

        for (int j = 0; j < 8; j++)
        {
            secondHallLights[j].intensity = secondHallLightsIntensity[j];
        }
    }

    private void SlamDoor()
    {
        currentLerpTime += Time.deltaTime;

        if (currentLerpTime >= slamTime)
        {
            currentLerpTime = slamTime;
            shouldSlam = false;
        }

        float percentage = currentLerpTime / slamTime;
        doorToSlam.transform.rotation = Quaternion.Lerp(doorStartingRotation.rotation, doorEndingRotation.rotation, percentage);
    }

    IEnumerator DelaySlamDoor()
    {
        yield return new WaitForSeconds(slamDelay);
        audioSource.PlayOneShot(doorSlam);
        shouldSlam = true;
    }
}
