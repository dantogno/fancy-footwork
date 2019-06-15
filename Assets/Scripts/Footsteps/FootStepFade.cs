using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepFade : MonoBehaviour
{
    //the flash game object
    public CameraFlash flashObject;
    private float dissolveTime;

    //the renderer of the game object
    Renderer rnd;

    // Start is called before the first frame update
    void Start()
    {
        //set the renderer
        rnd = GetComponent<Renderer>();
        //set the inital value
        rnd.material.SetFloat("_Dissolve",0);
        dissolveTime = flashObject.flashExposureTime;
    }

    float secondsPast = 0;
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
            if (rnd.material.GetFloat("_Dissolve") != 0)
                rnd.material.SetFloat("_Dissolve", 0);
            secondsPast = 0;
        }
    }

    //the previous light intensity
    private float previousIntensity = 0;
    private void FadeFootSteps()
    {
        if (secondsPast > flashObject.hiddenObjectFadeDelay)
        {
            if (rnd.material.GetFloat("_Dissolve") < 1)
            {
                //calculate the new amount with decrease
                // Changing this to lerp to appear smoother and ensure transition all the way to 1.
                float dissolveAmount = Mathf.Lerp(0, 1, (secondsPast - flashObject.hiddenObjectFadeDelay) / dissolveTime);
                //set it to new amount
                rnd.material.SetFloat("_Dissolve", dissolveAmount);
            }     
        }
        secondsPast += Time.deltaTime;
    }
}
