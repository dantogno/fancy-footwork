using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    //Buttons used in menu
    public Button InstructionsButton;
    public Button StartButton;
    public Button ExitButton;
    public Button MenuButton;
    public Button CreditsButton;
    //public Button RestartButton;
    [SerializeField]
    AudioClip pressed;
    static AudioClip _pressed;
    [SerializeField]
    AudioSource source;
    static AudioSource _source;
    [SerializeField] GameObject loadingUI;
    [SerializeField] GameObject MenuUI;
    [SerializeField] Slider loadingProgbar;
    [SerializeField] Text loadingText;

    //the number of hiders the seeker has found
    public static int hidersFound = 0;

    // Use this for initialization
    void Start()
    {

        //Screen.SetResolution(1920, 1080, true);

        AudioClip _pressed = pressed;
        _source = source;
        //dont destroy this game object

        loadingUI.SetActive(false);

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
        //source.PlayOneShot(pressed);
        SceneManager.LoadScene("Instructions");
    }

    //exits game
    void TaskOnClick1()
    {
        //source.PlayOneShot(pressed);
        Application.Quit();
    }

    //returns to start menu
    void TaskOnClick2()
    {
        //source.PlayOneShot(pressed);
        SceneManager.LoadScene("MainMenu");
    }

    //loads credits
    void TaskOnClick3()
    {
        //source.PlayOneShot(pressed);
        SceneManager.LoadScene("Credits");
    }

    ////loads checkpoint
    //void TaskOnClick4()
    //{
    //    SceneManager.LoadScene("Level" + StatManager.level);
    //    StatManager.hasKey = false;
    //}

    AsyncOperation sceneAO;
    //loads game
    void TaskOnClick5()
    {
        
        //source.PlayOneShot(pressed);
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("GameScene");
    }

    private const float LOAD_READY_PERCENTAGE = 0.9f;
    IEnumerator LoadingSceneRealProgress()
    {
        yield return new WaitForSeconds(1);
        sceneAO = SceneManager.LoadSceneAsync("GameScene");

        // disable scene activation while loading to prevent auto load
        sceneAO.allowSceneActivation = false;

        while (!sceneAO.isDone)
        {
            loadingProgbar.value = sceneAO.progress;

            if (sceneAO.progress >= LOAD_READY_PERCENTAGE)
            {
                loadingProgbar.value = 1f;
                yield return new WaitForSeconds(.5f);
            }
            Debug.Log(sceneAO.progress);
            yield return null;
        }
    }

    //loads win scene
    public static void Win()
    {
        SceneManager.LoadScene("Win");
    }
    //loads gameover scene
    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}