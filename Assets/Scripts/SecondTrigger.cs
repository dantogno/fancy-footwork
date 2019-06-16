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

    private float[] secondHallLightsIntensity = new float[8];
    private MeshCollider triggerCollider;
    private bool hasBeenTriggered = false;
    private AudioSource audioSource;

    private void Awake()
    {
        triggerCollider = GetComponent<MeshCollider>();
        audioSource = GetComponent<AudioSource>();
        GetLightIntensity();
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
            hasBeenTriggered = true;
        }
    }

    private void TurnLights()
    {
        audioSource.Play();

        for (int i = 0; i < 6; i++)
        {
            firstHallLights[i].intensity = 0.0f;
        }

        for (int j = 0; j < 8; j++)
        {
            secondHallLights[j].intensity = secondHallLightsIntensity[j];
        }
    }
}
