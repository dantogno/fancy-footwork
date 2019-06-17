using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    //the intensity of the camera flash
    private float flashIntensity;

    //the amount of time the flash lasts
    public float flashExposureTime = 2;

    [Tooltip("Amount of time between when camera flash is finished fading out and hidden objects fully dissapear.")]
    public float hiddenObjectFadeDelay = 1;

    //amount of clicks needed to wind
    public float cameraWindClicks = 5;

    //Amount to decrease flash overtime by
    public float decreaseFlashAmount=.5f;

    //Frequency to turn decrease light, no more than 1
    public float lightFadeTimeFrequence = .5f;

    //the light object for the flash
    public Light flashObject = null;

    public AudioSource audioSource;

    public AudioClip wind;
    public AudioClip flash;
    public AudioClip finishWind;
    private bool playedFlash = false;

    //whether or not to show layer
    public bool showFootSteps = false;

    public enum FlashState { Ready, Flash, Fading, Winding};
    public FlashState currentFlashState;

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
        currentFlashState = FlashState.Ready;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ////Get the input from the left mouse button
        //float leftButton = Input.GetAxis("Fire1");
        ////check to see if pressed
        //if(leftButton!=0)
        //{
        //    //if the camera is ready to go off again then change state
        //    //and call appropriate method
        //    if (currentFlashState == FlashState.Ready)
        //    {
        //        currentFlashState = FlashState.Flash;
        //        showFootSteps = true;
        //        MakeCameraFlash();
        //    }
        //}
        if(currentFlashState==FlashState.Flash)
        {
            MakeCameraFlash();
        }
        else if(currentFlashState==FlashState.Fading)
        {
            MakeFlashDisipate();
        }
        else if(currentFlashState==FlashState.Winding)
        {
            WindCameraUp();
        }
        //show foot steps
        if (showFootSteps)
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("FootSteps");
    }

    private float secondsPast = 0;
    //Have the camera flash for a specified time
    public void MakeCameraFlash()
    {
        //check how long the flash has lasted
        if (secondsPast <= flashExposureTime)
        {
            if(!playedFlash)
            {
                playedFlash = true;
                audioSource.PlayOneShot(flash);
            }
            //set intensity of light to the specified one
            if (flashObject.intensity != flashIntensity)
                flashObject.intensity = flashIntensity;
            //increase the time passed
            secondsPast += Time.deltaTime; 
        }
        else
        {
            //set to next state and reset the time passed
            currentFlashState = FlashState.Fading;
            playedFlash = false;
            secondsPast = 0;
        }
    }

    //decrease the intensity of the light all the way to zero
    private void MakeFlashDisipate()
     {
        if (secondsPast < flashExposureTime)
        {
            // Reduce light intensity based on time that has passed.
            flashObject.intensity = Mathf.Lerp(flashIntensity, 0, secondsPast / flashExposureTime);
        }
        else
            flashObject.intensity = 0;
            
        // Use the hiddenObjectFadeDelay to give our hidden objects a bit of extra visible time
        // after the light goes out.
        if (secondsPast > flashExposureTime + hiddenObjectFadeDelay)
        {
            //reset everything
            //make the foot steps disappear
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("FootSteps"));
            secondsPast = 0;
            showFootSteps = false;
            //set the next state
            currentFlashState = FlashState.Winding;
        }
        else
            secondsPast += Time.deltaTime;
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
                audioSource.PlayOneShot(wind);
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
            currentFlashState = FlashState.Ready;
            //reset the values
            audioSource.PlayOneShot(finishWind);
            clicks = 0;
            clicked = false;
        }
    }
}
