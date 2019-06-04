using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    //the intensity of the camera flash
    private float flashIntensity;
    //the light object for the flash
    public Light flashObject;

    //the amount of time the flash lasts
    public float flashExposureTime = 2;

    //amount of clicks needed to wind
    public float cameraWindClicks = 5;

    //flash cool down
    public float flashWindDown = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
