﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public DoorTypes typeOfDoor;
    private GameObject objectToEffect;
    public GameObject player;
    private Interactable interact;
    //public float ifLightSwitch_Intensity = 2;
    //Light lightObject;
    //bool hasBeenActiviated = false;
    bool hasBeenCalled = false;
    bool isRotating = false;
    bool isUnlocked = true;
    public GameObject paintingDoor;

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interactable>();
        if (typeOfDoor != DoorTypes.PaintingLock)
            objectToEffect = gameObject;
        else
            objectToEffect = paintingDoor;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (typeOfDoor == DoorTypes.PaintingDoor)
            isUnlocked = false;
        //if(typeOfDoor==DoorTypes.Light)
        //{
        //    lightObject = objectToEffect.GetComponent<Light>();
        //    if (lightObject != null)
        //        lightObject.intensity = 0;
        //}

        objectToEffect.GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.triggered && !hasBeenCalled)
        {
            hasBeenCalled = true;
            SwitchAction();
        }
        else if (!interact.triggered && hasBeenCalled)
        {
            hasBeenCalled = false;
        }

        if (isRotating &&!hasRotated)
            RotatePainting();
        
    }

    private void SwitchAction()
    {
        switch (typeOfDoor)
        {
            case DoorTypes.UnLockedDoor:
                OpenUnLockedDoor();
                break;
            //case DoorTypes.Light:
            //    TurnLightOn_Off();
            //    break;
            case DoorTypes.LockedFloorDoor:
                UnLockLockedDoor();
                break;
            case DoorTypes.PaintingDoor:
                if (isUnlocked)
                    OpenUnLockedDoor();
                break;
            case DoorTypes.PaintingLock:
                if (!isRotating && !hasRotated)
                {
                    isRotating = true;
                    RotatePainting();
                }
                break;
        }
    }

    //private void TurnLightOn_Off()
    //{
    //    if (lightObject != null)
    //    {
    //        if (!hasBeenActiviated)
    //        {
    //            lightObject.intensity = ifLight_Intensity;
    //            hasBeenActiviated = true;
    //        }
    //        else
    //        {
    //            lightObject.intensity = 0;
    //            hasBeenActiviated = false;
    //        }
    //    }
    //}



    private float timeForRotation = .1f;
    private bool hasRotated = false;
    private void RotatePainting()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, (transform.rotation.ToEuler().y * (180 / Mathf.PI)), 0), timeForRotation);
        if (Mathf.RoundToInt(transform.rotation.ToEuler().x * (180 / Mathf.PI)) == 0 && Mathf.RoundToInt(transform.rotation.ToEuler().z * (180 / Mathf.PI)) == 0)
        {
            hasRotated = true;
            objectToEffect = paintingDoor;
            OpenUnLockedDoor();
        }
        //Vector3 goToPosition = transform.position;
        //goToPosition.y= GameObject.FindGameObjectWithTag("Floor").transform.position.y+1;

        //transform.position = Vector3.Slerp(transform.position, goToPosition, 1);
    }
    private void UnLockLockedDoor()
    {

    }

    Vector3 previousPosition = Vector3.zero;
    private void OpenUnLockedDoor()
    {
        //objectToEffect.GetComponent<HingeJoint>().limits=;
        JointLimits limits = objectToEffect.GetComponent<HingeJoint>().limits;
        limits.min = -20;
        limits.max = 0;
        objectToEffect.GetComponent<HingeJoint>().limits = limits;

        objectToEffect.GetComponent<Rigidbody>().freezeRotation = false;
        objectToEffect.GetComponent<Rigidbody>().AddForce(50, 0, 0);
        StartCoroutine(ChangeMin());
    }

    IEnumerator ChangeMin()
    {
        while (objectToEffect.transform.position != previousPosition)
        {
            previousPosition = objectToEffect.transform.position;
            yield return new WaitForSeconds(.25f);
        }
        JointLimits limits = objectToEffect.GetComponent<HingeJoint>().limits;
        limits.min = -70;
        limits.max = 0;
        objectToEffect.GetComponent<HingeJoint>().limits = limits;
    }
}

public enum DoorTypes
{
    //Light,
    UnLockedDoor,
    LockedFloorDoor,
    PaintingDoor,
    PaintingLock,
}