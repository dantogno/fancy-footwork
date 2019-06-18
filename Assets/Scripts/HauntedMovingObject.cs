using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HauntedMovingObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 endingLocation;

    [SerializeField]
    private GameObject objectToMove;

    [Tooltip("The amount of time in seconds it will take for the object to cover the distance.")]
    [SerializeField]
    private float lerpTime = 10.0f;

    [SerializeField]
    private AudioClip ring;

    private Vector3 startingLocation;
    private float currentLerpTime;
    private bool hasMoved = false;
    private bool audioIsPlaying = false;
    private AudioSource audioSource;

    public bool objectShouldMove = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startingLocation = objectToMove.transform.position;
    }

    private void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            objectShouldMove = true;
        }

        if (objectShouldMove && !hasMoved)
        {
            MoveObject();
            PlayAudio();
        }
    }

    private void PlayAudio()
    {
        if (!audioIsPlaying)
        {
            audioSource.Play();
            audioIsPlaying = true;
        }

        if(hasMoved == true)
        {
            audioSource.Stop();
            audioIsPlaying = false;
            audioSource.PlayOneShot(ring);
        }
    }

    private void MoveObject()
    {
        currentLerpTime += Time.deltaTime;

        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
            hasMoved = true;
        }

        float percentage = currentLerpTime / lerpTime;
        objectToMove.transform.position = Vector3.Lerp(startingLocation, endingLocation, percentage);
    }
}
