using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Phone : MonoBehaviour
{
    //main camera
    private Camera mainCamera;
    private AudioListener mainAudio;
    //camera for phone key pad
    private Camera phoneCamera;
    private AudioListener phoneAudio;
    //interactable script
    private Interactable interact;
    //bool to see if the cameras have already been switched
    private bool setActive_Inactive = false;

    [SerializeField]
    private AudioClip ring, pickUp, hangUp, buttonPress, enterCode, wrongCode, rightCode, floorShift, secondVideo;

    //To trigger the luggage cart after the phone is hung up
    [SerializeField]
    private HauntedMovingObject luggageCart;

    [SerializeField]
    private GameObject luggageCartObject;

    [SerializeField]
    private float tvFlickerDelay = 0.1f, tvFlickerIntensity = 1.0f;

    [SerializeField]
    private Light tvLight;

    //bools for checking if they've triggered the phone ringing and if the phone has ever been picked up
    private bool hasRung = false;
    private bool hasBeenPickedUpAfterRinging = false;
    private bool shouldFlicker = false;
    private bool previousEnableBool;
    

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

    private int[] code= new int[3];
    private int codeIndex = 0;
    public int[] codeKey=new int[3];

    public Canvas phoneCanvas;
    public Canvas playerCanvas;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //get the camera objects
        mainCamera = Camera.main;
        //mainAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        phoneCamera = GameObject.FindGameObjectWithTag("PhonePadCamera").GetComponent<Camera>();
        phoneAudio = GameObject.FindGameObjectWithTag("PhonePadCamera").GetComponent<AudioListener>();
        playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>();
        //set the non-player camera to false
        phoneCamera.enabled = false;
        phoneAudio.enabled = false;
        //phoneAudio.enabled = false;
        //get the interactable component
        interact = GetComponent<Interactable>();
        //Set the text to empty
        numberBox.text = "";
        //set the automatic lock to false
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        //lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //intialize the buttons
        InitializeButtons();
        phoneCanvas.enabled = false;
    }

    private void InitializeButtons()
    {
        //phone pad 1
        Button btn = one.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick1);
        //phone pad 2
        Button btn1 = two.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick2);
        //phone pad 3
        Button btn2 = three.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick3);
        //phone pad 4
        Button btn3 = four.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClick4);
        //phone pad 5
        Button btn4 = five.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClick5);
        //phone pad 6
        Button btn5 = six.GetComponent<Button>();
        btn5.onClick.AddListener(TaskOnClick6);
        //phone pad 7
        Button btn6 = seven.GetComponent<Button>();
        btn6.onClick.AddListener(TaskOnClick7);
        //phone pad 8
        Button btn7 = eight.GetComponent<Button>();
        btn7.onClick.AddListener(TaskOnClick8);
        //phone pad 9
        Button btn8 = nine.GetComponent<Button>();
        btn8.onClick.AddListener(TaskOnClick9);
        //phone pad 10
        Button btn9 = zero.GetComponent<Button>();
        btn9.onClick.AddListener(TaskOnClick0);
        //phone pad exit
        Button btnExit = exitButton.GetComponent<Button>();
        btnExit.onClick.AddListener(TaskOnClickExit);
    }

    private void TaskOnClickExit()
    {
        //if this is the first time they hang up, move the luggage cart
        if (hasRung && !hasBeenPickedUpAfterRinging)
        {
            MoveCart();
            hasBeenPickedUpAfterRinging = true;
        }

        audioSource.Stop();
        audioSource.PlayOneShot(hangUp, 0.5f);
        
        //reset the bool
        setActive_Inactive = false;
        //activate the player again
        
        //disable phone camera
        phoneCamera.enabled = false;
        mainCamera.gameObject.gameObject.SetActive(true);
        //relock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        phoneCanvas.enabled = false;
        if (previousEnableBool)
            playerCanvas.enabled = true;


    }

    private void TaskOnClick0()
    {
        if(codeIndex<3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 0 ");
            code[codeIndex] = 0;
            codeIndex++;
        }
    }

    private void TaskOnClick9()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 9 ");
            code[codeIndex] = 9;
            codeIndex++;
        }
    }

    private void TaskOnClick8()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 8 ");
            code[codeIndex] = 8;
            codeIndex++;
        }
    }

    private void TaskOnClick7()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 7 ");
            code[codeIndex] = 7;
            codeIndex++;
        }
    }

    private void TaskOnClick6()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 6 ");
            code[codeIndex] = 6;
            codeIndex++;
        }
    }

    private void TaskOnClick5()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 5 ");
            code[codeIndex] = 5;
            codeIndex++;
        }
    }

    private void TaskOnClick4()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 4 ");
            code[codeIndex] = 4;
            codeIndex++;
        }
    }

    private void TaskOnClick3()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 3 ");
            code[codeIndex] = 3;
            codeIndex++;
        }
    }

    private void TaskOnClick2()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 2 ");
            code[codeIndex] = 2;
            codeIndex++;
        }
    }

    private void TaskOnClick1()
    {
        if (codeIndex < 3)
        {
            audioSource.PlayOneShot(buttonPress);
            numberBox.text += (" 1 ");
            code[codeIndex] = 1;
            codeIndex++;
        }
    }

    bool completedcode = false;
    bool codeAccepted = false;
    int index = 0;
    // Update is called once per frame
    void Update()
    {
        //if the switch has been triggered then switch camera views
        if(interact.triggered)
        {
              
            //if not switch before then switch
            if (!setActive_Inactive)
            {
                
                audioSource.Stop();
                audioSource.loop = false;
                //make sure if triggered again, not recalled
                setActive_Inactive = true;
                //disable the whole gameobject hierarchy to stop error
                mainCamera.gameObject.gameObject.SetActive(false);
                phoneAudio.enabled = true;
                //enable phone camera
                phoneCamera.enabled = true;
                //unlock cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                previousEnableBool = playerCanvas.enabled;
                if (previousEnableBool)
                    playerCanvas.enabled = false;
                phoneCanvas.enabled = true;
                audioSource.PlayOneShot(pickUp);
                audioSource.PlayOneShot(enterCode, 0.7f);
            }
        }
        if(codeIndex>=3)
        {
            //check if code is correct
            for(int i=0;i<3;i++)
            {
                //if number is correct then set true
                if(codeKey[i]==code[i])
                {
                    audioSource.Stop();
                    completedcode = true;

                }
                //otherwise set to false and break loop
                else
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(wrongCode, 0.7f);
                    completedcode = false;
                    break;
                }
            }
            if (completedcode)
            {

                //activate the player again
                mainCamera.gameObject.gameObject.SetActive(true);
                //disable phone camera
                phoneCamera.enabled = false;
                phoneAudio.enabled = false;
                if (previousEnableBool)
                    playerCanvas.enabled = true;
                phoneCanvas.enabled = false;
                //relock cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                //take away outline
                if (GetComponent<Renderer>().material.GetColor("_EmissionColor") != Color.black)
                    GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
                //no longer interactable
                interact.enabled = false;
                //open secert door
                GameObject door = GameObject.FindGameObjectWithTag("SecretStairsPanel");
                if (door != null)
                    door.GetComponent<HauntedMovingObject>().objectShouldMove = true;
                //if(!codeAccepted && index<2)
                //{
                //    if (index == 0 && !audioSource.isPlaying)
                //    {
                //        audioSource.PlayOneShot(rightCode, 0.7f);
                //        index++;
                //    }
                //    if (index == 1 && !audioSource.isPlaying)
                //    {
                //        audioSource.PlayOneShot(floorShift);
                //        index++;
                //    }
                //}
                //else if(!codeAccepted&& index>=2)
                //{
                //    codeAccepted = true;
                //}


            }
            else
            {
                //reset
                codeIndex = 0;
                numberBox.text = "";
            }
        }
        
    }

    IEnumerator PlaySound()
    {
        audioSource.Stop();
        //audioSource.PlayOneShot(rightCode, 0.7f);
        yield return new WaitForSeconds(2);
        codeAccepted = true;
    }

    private void MoveCart()
    {
        luggageCartObject.SetActive(true);
        luggageCart.objectShouldMove = true;
        StartCoroutine(PlayTV());
    }

    IEnumerator PlayTV()
    {
        shouldFlicker = true;
        StartCoroutine(TVFlicker());
        //audioSource.PlayOneShot(secondVideo);
        yield return new WaitForSeconds(8);
        shouldFlicker = false;
    }

    IEnumerator TVFlicker()
    {
        if (shouldFlicker)
        {
            tvLight.intensity = tvFlickerIntensity;
            yield return new WaitForSeconds(tvFlickerDelay);
            tvLight.intensity = 0.0f;
            yield return new WaitForSeconds(tvFlickerDelay);
            StartCoroutine(TVFlicker());
        }
    }

    private void OnPhoneShouldRing()
    {
        audioSource.loop = true;
        audioSource.Play();
        hasRung = true;
    }

    private void OnEnable()
    {
        EndOfSecondHallTrigger.PhoneShouldRing += OnPhoneShouldRing;
    }

    private void OnDisable()
    {
        EndOfSecondHallTrigger.PhoneShouldRing -= OnPhoneShouldRing;
    }
}
