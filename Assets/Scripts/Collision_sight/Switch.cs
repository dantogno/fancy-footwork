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
            case SwitchTypes.playerUnLockedDoor:
                OpenDoor();
                break;
            case SwitchTypes.Light:
                TurnLightOn_Off();
                break;
            case SwitchTypes.Button:
                ToggleButton();
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

    private void ToggleButton()
    {

    }

    private void OpenDoor()
    {
        //objectToEffect.GetComponent<HingeJoint>().limits=;
        objectToEffect.GetComponent<Rigidbody>().AddForce(10,0,0);
    }

}

public enum SwitchTypes
{
    Light,
    Button,
    playerUnLockedDoor,
}
