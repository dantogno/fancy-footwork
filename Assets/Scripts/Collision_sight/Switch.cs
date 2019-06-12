using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public SwitchTypes typeOfSwitch;
    public GameObject objectToEffect;
    public GameObject player;
    private Interactable interact;
    public float ifLight_Intensity = 2;
    Light lightObject;
    bool hasBeenActiviated = false;
    bool hasBeenCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interactable>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(typeOfSwitch==SwitchTypes.Light)
        {
            lightObject = objectToEffect.GetComponent<Light>();
            if (lightObject != null)
                lightObject.intensity = 0;
        }
        else if(typeOfSwitch == SwitchTypes.UnLockedDoor)
        {
            objectToEffect.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.triggered && !hasBeenCalled)
        {
            hasBeenCalled = true;
            SwitchAction();
        }
        else if(!interact.triggered&&hasBeenCalled)
        {
            hasBeenCalled = false;
        }
    }

    private void SwitchAction()
    {
        switch(typeOfSwitch)
        {
            case SwitchTypes.UnLockedDoor:
                OpenUnLockedDoor();
                break;
            case SwitchTypes.Light:
                TurnLightOn_Off();
                break;
            case SwitchTypes.LockedDoor:
                UnLockLockedDoor();
                break;
            case SwitchTypes.SecretDoor:
                UnLockLockedDoor();
                break;
        }
    }

    private void TurnLightOn_Off()
    {
        if (lightObject != null)
        {
            if (!hasBeenActiviated)
            {
                lightObject.intensity = ifLight_Intensity;
                hasBeenActiviated = true;
            }
            else
            {
                lightObject.intensity = 0;
                hasBeenActiviated = false;
            }
        }
    }

    private void UnLockLockedDoor()
    {
        ObjectToPutInInventory keyItem = player.GetComponent<PlayerInventory>().inventory.Find(i => i.objectType == PickUpObjectEnum.Key);
        if(keyItem.objectType== PickUpObjectEnum.Key)
        {
            player.GetComponent<PlayerInventory>().inventory.Remove(keyItem);
            typeOfSwitch = SwitchTypes.UnLockedDoor;
        }

    }

    Vector3 previousPosition=Vector3.zero;
    private void OpenUnLockedDoor()
    {
        //objectToEffect.GetComponent<HingeJoint>().limits=;
        JointLimits limits = objectToEffect.GetComponent<HingeJoint>().limits;
        limits.min = -10;
        limits.max = 0;
        objectToEffect.GetComponent<HingeJoint>().limits = limits;

        objectToEffect.GetComponent<Rigidbody>().freezeRotation = false;
        objectToEffect.GetComponent<Rigidbody>().AddForce(10,0,0);
        StartCoroutine(ChangeMin());
    }

    IEnumerator ChangeMin()
    {
        while (objectToEffect.transform.position!= previousPosition)
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

public enum SwitchTypes
{
    Light,
    UnLockedDoor,
    LockedDoor,
    SecretDoor
}
