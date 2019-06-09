using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepFade : MonoBehaviour
{
    //the flash game object
    public CameraFlash flashObject;

    //the amount to dissolve the foot steps
    public float amountToDissolve = .01f;

    //the renderer of the game object
    Renderer rnd;

    // Start is called before the first frame update
    void Start()
    {
        //set the renderer
        rnd = GetComponent<Renderer>();
        //set the inital value
        rnd.material.SetFloat("_Dissolve",1);
    }

    // Update is called once per frame
    void Update()
    {
        //if the light is fading then fade the steps
        if (flashObject.currentFlashState == CameraFlash.FlashState.Fading)
        {
            FadeFootSteps();
        }
        else
        {
            //if the dissolve value isn't the intial one then reset it
            if (rnd.material.GetFloat("_Dissolve") != 1)
                rnd.material.SetFloat("_Dissolve", 1);
        }
    }

    //the previous light intensity
    private float previousIntensity = 0;
    private void FadeFootSteps()
    {
        //check to see if the light intensity is greater than 0 and if it has changed
        if (previousIntensity != flashObject.flashObject.intensity && flashObject.flashObject.intensity >= 0)
        {
            //reset the previous intensity to the current one
            previousIntensity = flashObject.flashObject.intensity;
            //if the dissolve amount is great than 0 then decrease
            if (rnd.material.GetFloat("_Dissolve") > 0)
            {
                //calculate the new amount with decrease
                float dissolveAmount = rnd.material.GetFloat("_Dissolve") - amountToDissolve;
                //set it to new amount
                rnd.material.SetFloat("_Dissolve",dissolveAmount);
            }
           
        }
    }
}
