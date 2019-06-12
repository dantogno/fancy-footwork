using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private Light light;

    [Tooltip("The maximum time that the light will be left randomly off or on.")]
    [SerializeField]
    private float secondsOnMax = 3.0f, secondsOffMax = 1.0f;

    [SerializeField]
    private AudioClip lightOnClip, lightOffClip;

    private float timer;
    private float startingLightIntensity;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startingLightIntensity = light.intensity;
        StartCoroutine(FlickeringLight());
    }

    IEnumerator FlickeringLight()
    {
        timer = Random.Range(0.1f, secondsOnMax);
        yield return new WaitForSeconds(timer);
        light.intensity = 0.0f;
        audioSource.PlayOneShot(lightOffClip, 0.1f);
        timer = Random.Range(0.1f, secondsOffMax);
        yield return new WaitForSeconds(timer);
        light.intensity = startingLightIntensity;
        audioSource.PlayOneShot(lightOnClip, 0.1f);
        StartCoroutine(FlickeringLight());
    }
}
