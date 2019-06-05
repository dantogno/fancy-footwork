using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepFade : MonoBehaviour
{
    //the material script of the footstep object
    //private GameObject footStepsObject;

    public CameraFlash flashObject;

    Color color;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        //footStepsObject = gameObject.GetComponent<Renderer>();
        color = gameObject.GetComponent<Renderer>().material.color;
        color.a = 1;
        originalColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashObject.currentFlashState == CameraFlash.FlashState.Fading)
        {
            FadeFootSteps();
        }
        else if(color!=originalColor)
        {
            color = originalColor;
            gameObject.GetComponent<Renderer>().material.color = color;
        }
    }

    private float previousIntensity = 0;
    private void FadeFootSteps()
    {
        if (previousIntensity != flashObject.flashObject.intensity && flashObject.flashObject.intensity >= 0)
        {
            previousIntensity = flashObject.flashObject.intensity;
            color.a = color.a - (.05f);
            color.r += .05f;
            if (color.r > 1)
                color.r = 1;
            color.g += .05f;
            if (color.g > 1)
                color.g = 1;
            color.b += .05f;
            if (color.b > 1)
                color.b = 1;
            gameObject.GetComponent<Renderer>().material.color = color;
            if (gameObject.GetComponent<Renderer>().material.color.a < 0)
            {
                color.a = 0;
                gameObject.GetComponent<Renderer>().material.color = color;
            }
            print(gameObject.GetComponent<Renderer>().material.color);
        }
    }
}
