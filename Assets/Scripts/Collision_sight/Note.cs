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

    [SerializeField]
    private AudioClip notePickUp, notePutDown;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interact = GetComponent<Interactable>();
        mainCamera = Camera.main;
        noteCamera = GameObject.FindGameObjectWithTag("NoteCamera").GetComponent<Camera>();
        GameObject.FindGameObjectWithTag("NoteCamera").GetComponent<AudioListener>().enabled=false;
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
        GameObject.FindGameObjectWithTag("NoteCamera").GetComponent<AudioListener>().enabled = false;
        audioSource.PlayOneShot(notePutDown);
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
                GameObject.FindGameObjectWithTag("NoteCamera").GetComponent<AudioListener>().enabled = true;
                audioSource.PlayOneShot(notePickUp, 2f);
            }
        }
    }
}
