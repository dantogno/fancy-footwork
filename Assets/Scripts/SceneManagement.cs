using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagement : MonoBehaviour
{

    //Buttons used in menu
    public Button InstructionsButton;
    public Button StartButton;
    public Button ExitButton;
    public Button MenuButton;
    public Button CreditsButton;
    //public Button RestartButton;
    //[SerializeField]
    //AudioClip pressed;
    //static AudioClip _pressed;
    //[SerializeField]
    //AudioSource source;
    //static AudioSource _source;
    [SerializeField] Image fillImage;
    [SerializeField] Text loadingText;
    private float value = 0;

    // Use this for initialization
    void Start()
    {

        //Screen.SetResolution(1920, 1080, true);

        //AudioClip _pressed = pressed;
        //_source = source;
        //dont destroy this game object

        if (fillImage != null)
            fillImage.fillAmount = 0f;

        //create code for buttons, buttons only work if there is an object attached to it
        if (InstructionsButton != null)
        {
            Button btn = InstructionsButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }
        if (StartButton != null)
        {
            Button btn = StartButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick5);
        }
        if (ExitButton != null)
        {
            Button btn1 = ExitButton.GetComponent<Button>();
            btn1.onClick.AddListener(TaskOnClick1);
        }
        if (MenuButton != null)
        {
            Button btn2 = MenuButton.GetComponent<Button>();
            btn2.onClick.AddListener(TaskOnClick2);
        }
        if (CreditsButton != null)
        {
            Button btn3 = CreditsButton.GetComponent<Button>();
            btn3.onClick.AddListener(TaskOnClick3);
        }
        //if (RestartButton != null)
        //{
        //    Button btn4 = RestartButton.GetComponent<Button>();
        //    btn4.onClick.AddListener(TaskOnClick4);
        //}
    }

    // Update is called once per frame
    void Update()
    {
    }

    //loads instructions
    void TaskOnClick()
    {
        
        SceneManager.LoadScene("Instructions");
    }

    //exits game
    void TaskOnClick1()
    {
        
        Application.Quit();
    }

    //returns to start menu
    void TaskOnClick2()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    //loads credits
    void TaskOnClick3()
    {
        
        SceneManager.LoadScene("Credits");
    }

    ////loads checkpoint
    //void TaskOnClick4()
    //{
    //    SceneManager.LoadScene("Level" + StatManager.level);
    //    StatManager.hasKey = false;
    //}

    
    private bool loadScene = false;
    public string LoadingSceneName;
    //loads game
    void TaskOnClick5()
    {

        SceneManager.LoadScene("GameScene");
        DontDestroyOnLoad(this);
        
        //StartCoroutine(LoadingSceneRealProgress());
    }

    bool found1 = false;
    bool found2 = false;
    private const float LOAD_READY_PERCENTAGE = 0.9f;
    IEnumerator LoadingSceneRealProgress()
    {
        if(GameObject.FindGameObjectWithTag("fill")!=null && !found1)
        {
            found1 = true;
            fillImage=GameObject.FindGameObjectWithTag("fill").GetComponent<Image>();
        }
        if (GameObject.FindGameObjectWithTag("percent") != null && !found2)
        {
            found2 = true;
            loadingText = GameObject.FindGameObjectWithTag("precent").GetComponent<Text>();
        }
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        //asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            value = Mathf.Clamp01((asyncLoad.progress / .9f));
            if (fillImage != null)
                fillImage.fillAmount = this.value;
            if (loadingText != null)
                loadingText.text = (int)(value * 100) + "%";
            yield return null;
        }
    }

    //loads win scene
    public static void Win()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //loads gameover scene
    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}