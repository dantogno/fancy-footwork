using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlashUI : MonoBehaviour
{
    bool hasClicked = false;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    public Image cameraClickImage;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float leftButton = Input.GetAxis("Fire1");
        if (player.transform.position == lastPosition && !hasClicked)
        {
            if (!cameraClickImage.enabled)
                cameraClickImage.enabled = true;
        }
        else if(!hasClicked)
        {
            if (cameraClickImage.enabled)
                cameraClickImage.enabled = false;
        }
        if(leftButton != 0)
        {
            hasClicked = true;
            cameraClickImage.enabled = false;
        }

        lastPosition = player.transform.position;
    }
}
