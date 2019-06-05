using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    //the intensity of the camera flash
    private float flashIntensity;

    //the amount of time the flash lasts
    public float flashExposureTime = 2;

    //amount of clicks needed to wind
    public float cameraWindClicks = 5;

    //flash cool down
    public float flashWindDown = 3;

    //Amount to decrease flash overtime by
    public float decreaseFlashAmount=.5f;

    //Frequency to turn decrease light, no more than 1
    public float lightFadeTimeFrequence = .5f;

    //the light object for the flash
    public Light flashObject;

    //whether or not to show layer
    private bool showFootSteps = false;

    enum FlashState { Ready, Flash, Fading, Winding};
    private FlashState currentState;

    private void Awake()
    {
        //null check, makes sure the object is found
        if (flashObject == null)
            print("Set Flash Object");
        else if (GameObject.FindGameObjectWithTag("CameraFlash") == null)
            print("Set Tag of Object");
    }

    // Start is called before the first frame update
    void Start()
    {
        //set intial intensity
        flashIntensity = flashObject.intensity;
        flashObject.intensity = 0;
        //if object is not set, then find it
        if (flashObject == null)
            flashObject = GameObject.FindGameObjectWithTag("CameraFlash").GetComponent<Light>();
        //set intial flash state
        currentState = FlashState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the input from the left mouse button
        float leftButton = Input.GetAxis("Fire1");
        //check to see if pressed
        if(leftButton!=0)
        {
            //if the camera is ready to go off again then change state
            //and call appropriate method
            if (currentState == FlashState.Ready)
            {
                currentState = FlashState.Flash;
                showFootSteps = true;
                MakeCameraFlash();
            }
        }
        if(currentState==FlashState.Flash)
        {
            MakeCameraFlash();
        }
        else if(currentState==FlashState.Fading)
        {
            MakeFlashDisipate();
        }
        else if(currentState==FlashState.Winding)
        {
            WindCameraUp();
        }
        //show foot steps
        if (showFootSteps)
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("FootSteps");
    }

    private float secondsPast = 0;
    private float previousTime = 0;
    //Have the camera flash for a specified time
    private void MakeCameraFlash()
    {
        //check how long the flash has lasted
        if (secondsPast <= flashExposureTime)
        {
            //set intensity of light to the specified one
            if (flashObject.intensity != flashIntensity)
                flashObject.intensity = flashIntensity;
            //increase the time passed
            secondsPast += Time.deltaTime; 
        }
        else
        {
            //set to next state and reset the time passed
            currentState = FlashState.Fading;
            secondsPast = 0;
        }
    }

    //decrease the intensity of the light all the way to zero
    private void MakeFlashDisipate()
    {
        //if time past doesn't equal fade time and the intensity isn't already 0
        if (secondsPast != flashWindDown && flashObject.intensity != 0)
        {
            //increase time passed
            secondsPast += Time.deltaTime;
            //if greater than the previous time by set value 
            if (secondsPast >= (previousTime + lightFadeTimeFrequence))
            {
                //set the previous time to the new one
                previousTime = secondsPast;
                //decrease intensity by set amount
                flashObject.intensity -= decreaseFlashAmount;
            }
        }
        else
        {
            //reset everything
            //make the foot steps disappear
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("FootSteps"));
            flashObject.intensity = 0;
            secondsPast = 0;
            previousTime = 0;
            showFootSteps = false;
            //set the next state
            currentState = FlashState.Winding;
        }
    }


    private float clicks = 0;
    private bool clicked = false;
    private void WindCameraUp()
    {
        //get the scroll wheel delta value
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        //if scrolling up increase the amount of clicks
        if(scrollWheel>0)
        {
            //if not clicked and less than set amount
            if(!clicked && clicks<cameraWindClicks)
            {
                //set clicked
                clicked = true;
                //increase click amount
                clicks += 1;
                print(clicks);
            }
        }
        //if no longer scrolling up, reset the values
        else if(scrollWheel<=0)
        {
            if(clicked)
            {
                clicked = false;
            }
        }
        //if wound up then reset
        if(clicks>=cameraWindClicks)
        {
            //To-Do: Play Sound here

            //set the next state
            currentState = FlashState.Ready;
            //reset the values
            clicks = 0;
            clicked = false;
        }
    }
}
