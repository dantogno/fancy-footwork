using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    //main camera
    private Camera mainCamera;
    //camera for phone key pad
    private Camera phoneCamera;
    //interactable script
    public Interactable interact;
    //bool to see if the cameras have already been switched
    private bool setActive_Inactive = false;
    //The number buttons
    public Button one;
    public Button two;
    public Button three;
    public Button four;
    public Button five;
    public Button six;
    public Button seven;
    public Button eight;
    public Button nine;
    public Button zero;
    //text to show numbers clicked
    public Text numberBox;
    //button to exit the camera view
    public Button exitButton;


    // Start is called before the first frame update
    void Start()
    {
        //get the camea objects
        mainCamera = Camera.main;
        phoneCamera = GameObject.FindGameObjectWithTag("PhonePadCamera").GetComponent<Camera>();
        //set the non-player camera to false
        phoneCamera.enabled = false;
        //get the interactable component
        interact = GetComponent<Interactable>();
        numberBox.text = "";
    }

    private void InitializeButtons()
    {
        Button btn = one.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick1);
        Button btn1 = two.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick2);
        Button btn2 = three.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick3);
        Button btn3 = four.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClick4);
        Button btn4 = five.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClick5);
        Button btn5 = six.GetComponent<Button>();
        btn5.onClick.AddListener(TaskOnClick6);
        Button btn6 = seven.GetComponent<Button>();
        btn6.onClick.AddListener(TaskOnClick7);
        Button btn7 = seven.GetComponent<Button>();
        btn6.onClick.AddListener(TaskOnClick8);
        Button btn8 = seven.GetComponent<Button>();
        btn6.onClick.AddListener(TaskOnClick9);
        Button btn9 = seven.GetComponent<Button>();
        btn6.onClick.AddListener(TaskOnClick0);
        Button btnExit = seven.GetComponent<Button>();
        btnExit.onClick.AddListener(TaskOnClickExit);
    }

    private void TaskOnClickExit()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick0()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick9()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick8()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick7()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick6()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick5()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick4()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick3()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick2()
    {
        throw new NotImplementedException();
    }

    private void TaskOnClick1()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        //if the switch has been triggered then switch camera views
        if(interact.triggered)
        {
            //if not switch before then switch
            if (!setActive_Inactive)
            {
                setActive_Inactive = true;
                //disable the whole gameobject hierarchy to stop error
                mainCamera.gameObject.gameObject.SetActive(false);
                phoneCamera.enabled = true;
            }
        }
    }
}
