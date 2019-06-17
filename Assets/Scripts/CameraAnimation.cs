using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Vector3 intialPosition;
    private CameraFlash flash;
    public Vector2 cameraRaiseAmount = new Vector2(0, 10);
    public enum AnimationStates { down, moving, up };
    private AnimationStates currentState;
    public float speedOfRise = 1;
    // Start is called before the first frame update
    void Start()
    {
        intialPosition = transform.position;
        flash = GameObject.FindGameObjectWithTag("CameraFlash").GetComponent<CameraFlash>();
        currentState = AnimationStates.down;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the input from the left mouse button
        float leftButton = Input.GetAxis("Fire1");
        //check to see if pressed
        if (leftButton != 0)
        {
            //if the camera is ready to go off again then change state
            //and call appropriate method
            if (flash.currentFlashState == CameraFlash.FlashState.Ready
                && currentState == AnimationStates.down)
            {

                currentState = AnimationStates.moving;
                MoveCameraUpAndFlash();
            }
        }
        if(flash.currentFlashState == CameraFlash.FlashState.Winding 
            && currentState == AnimationStates.up)
        {
            currentState = AnimationStates.moving;
            MoveCameraDownAndFlash();
        }
    }

    void MoveCameraUpAndFlash()
    {
        intialPosition = transform.position;
        Vector3 upPostion = new Vector3(intialPosition.x + cameraRaiseAmount.x, intialPosition.y + cameraRaiseAmount.y, intialPosition.z);
        transform.position = Vector3.Slerp(intialPosition, upPostion, speedOfRise);
        flash.currentFlashState = CameraFlash.FlashState.Flash;
        flash.showFootSteps = true;
        flash.MakeCameraFlash();
        currentState = AnimationStates.up;
    }

    void MoveCameraDownAndFlash()
    {
        intialPosition = transform.position;
        Vector3 downPostion = new Vector3(intialPosition.x - cameraRaiseAmount.x, intialPosition.y - cameraRaiseAmount.y, intialPosition.z);
        transform.position = Vector3.Slerp(intialPosition, downPostion, speedOfRise);
        currentState = AnimationStates.down;
    }
}
