using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private GameObject door1, door2;

    [SerializeField]
    private AudioClip elevatorUp, ding, doorsOpen;

    [SerializeField]
    private Vector3 door1EndingLocation, door2EndingLocation;

    [SerializeField]
    private float openTimeLength = 2.0f;

    private Vector3 door1StartingLocation, door2StartingLocation;
    private AudioSource audioSource;
    private float currentLerpTime;
    private bool shouldOpen = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        door1StartingLocation = door1.transform.position;
        door2StartingLocation = door2.transform.position;
    }

    void Start()
    {
        StartCoroutine(ElevatorAnimation());
    }

    private void Update()
    {
        if (shouldOpen)
        {
            OpenDoors();
        }
    }

    IEnumerator ElevatorAnimation()
    {
        audioSource.PlayOneShot(elevatorUp);
        yield return new WaitForSeconds(elevatorUp.length - 1.0f);
        audioSource.PlayOneShot(ding);
        shouldOpen = true;
        audioSource.PlayOneShot(doorsOpen);
    }

    private void OpenDoors()
    {
        currentLerpTime += Time.deltaTime;

        if (currentLerpTime >= openTimeLength)
        {
            currentLerpTime = openTimeLength;
            shouldOpen = false;
        }

        float percentage = currentLerpTime / openTimeLength;
        door1.transform.position = Vector3.Lerp(door1StartingLocation, door1EndingLocation, percentage);
        door2.transform.position = Vector3.Lerp(door2StartingLocation, door2EndingLocation, percentage);
    }
}
