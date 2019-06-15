using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    private Camera noteCamera;
    private Camera mainCamera;
    public Button exit;
    private Interactable interact;
    private bool setActive_Inactive;
    public Canvas NoteCanvas;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interactable>();
        mainCamera = Camera.main;
        noteCamera = GameObject.FindGameObjectWithTag("NoteCamera").GetComponent<Camera>();
        noteCamera.enabled = false;
        exit.onClick.AddListener(TaskOnClick);
        NoteCanvas.enabled = false;
    }

    private void TaskOnClick()
    {
        //make sure if triggered again, not recalled
        setActive_Inactive = false;
        //disable the whole gameobject hierarchy to stop error
        mainCamera.gameObject.gameObject.SetActive(true);
        //enable phone camera
        noteCamera.enabled = false;
        //unlock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NoteCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact.triggered)
        {
            //if not switch before then switch
            if (!setActive_Inactive)
            {
                //make sure if triggered again, not recalled
                setActive_Inactive = true;
                //disable the whole gameobject hierarchy to stop error
                Camera.main.gameObject.gameObject.SetActive(false);
                //enable phone camera
                noteCamera.enabled = true;
                //unlock cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                NoteCanvas.enabled = true;
            }
        }
    }
}
