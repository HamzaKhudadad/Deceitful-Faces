using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [Tooltip("Usee Blur in Pause Menu?")]
    public bool useBlur;

    [Header("Both UI Panels")]
    public GameObject saveMenu;
    public GameObject pauseMenu;
    public GameObject avatar;
    public GameObject cam;
    Fader fader;
    [HideInInspector]
    public bool isOpen;
    public bool avatarOpen;
    public bool camOpen;
    Canvas[] allUI;

    public List<LoadSlotIdentifier> loadSlots;

    [HideInInspector]
    public bool usingUFPS = false;
    private Button Escbutton;
    private Button avatarbutton;
    private Button cambutton;

    // Use this for initialization
    IEnumerator Start () {
        avatarOpen=true;
        camOpen = false;
        Debug.Log(avatarOpen);
        Escbutton = (Button)GameObject.Find("Escbutton").GetComponent<Button>();
        Escbutton.onClick.AddListener(delegate { btnclicked(); });
        avatarbutton = (Button)GameObject.Find("disableAvatar").GetComponent<Button>();
        avatarbutton.onClick.AddListener(delegate { avtarbtnclicked(); });
        cambutton = (Button)GameObject.Find("disablefeed").GetComponent<Button>();
        cambutton.onClick.AddListener(delegate { cambtnclicked(); });
        //find fader

        fader = FindObjectOfType<Fader>();

        yield return new WaitForSeconds(0.5f);

    }

    // Update is called once per frame
    void Update () {
        
        //if using UFPS
        if (usingUFPS)
            return; //exit

        //if save menu is not opened
        if (!saveMenu.active && canOpen())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isOpen)
                    openPauseMenu();
                else
                    closePauseMenu();
            }
        }
	}

    public void btnclicked()
    {
        EasyAudioUtility.instance.Play("Hover");
       

        if (!isOpen)
            openPauseMenu();
        else
            closePauseMenu();

    }

    public void cambtnclicked()
    {
        EasyAudioUtility.instance.Play("Hover");
        
        if (camOpen)
        {
            cam.SetActive(false);
            camOpen = false;
        }
        else
        {
            cam.SetActive(true);
            camOpen = true;
        }

    }

    public void avtarbtnclicked()
    {
        EasyAudioUtility.instance.Play("Hover");

        if (avatarOpen)
        {
            avatar.SetActive(false);
            avatarOpen = false;
        }
        else
        {
            avatar.SetActive(true);
            avatarOpen = true;
        }

    }

    public void openPauseMenu() {

        allUI = FindObjectsOfType<Canvas>();
        //disable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
            if (allUI[i].name != "Fader")
                allUI[i].gameObject.SetActive(false);
        }

        saveMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //play sound
        GetComponent<SaveGameUI>().playClickSound();

        //play anim
        GetComponent<Animator>().Play("OpenPauseMenu");

        //time = almost 0
        if(!usingUFPS)
            Time.timeScale = 0.0001f;

        isOpen = true;

        //init pause menu options
        GetComponent<PauseMenuOptions>().Init();

        //enable blur
        if (useBlur)
        {
            if (Camera.main.GetComponent<Animator>())
                Camera.main.GetComponent<Animator>().Play("BlurOff");

        }


    }


    public void closePauseMenu() {

        //enable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
           allUI[i].gameObject.SetActive(true);
        }

        //time = 1
        if(!usingUFPS)
            Time.timeScale = 1;

        //play sound
        GetComponent<SaveGameUI>().playClickSound();

        //play anim
        GetComponent<Animator>().Play("ClosePauseMenu");
        // hideMenus();

        isOpen = false;

        //enable blur
        if (useBlur)
        {
            if(Camera.main.GetComponent<Animator>())
            Camera.main.GetComponent<Animator>().Play("BlurOff");

        }

    }

    public void hideMenus() {
        saveMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void goToMainMenu() {
        //delete player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);

        //restore time scale (important)
        Time.timeScale = 1f;

        //load main menu
        #if !EMM_ES2
        PlayerPrefs.SetString("sceneToLoad", "");
        #else
        PlayerPrefs.SetString("sceneToLoad", "");
        ES2.Save("", "sceneToLoad");
        #endif

        //hide all menus
        hideMenus();

        //load level via fader
        fader.FadeIntoLevel("LoadingScreen");
    }

    public void openLoadGame() {
        GetComponent<Animator>().Play("loadGameOpen");
        initLoadGameMenu();
    }

    public void closeLoadGame() {
        GetComponent<Animator>().Play("loadGameClose");
    }

    void initLoadGameMenu() {
        if (loadSlots.Count > 0)
        {
            foreach (LoadSlotIdentifier lsi in loadSlots)
            {
                lsi.Init();
            }
        }

    }

    [HideInInspector]
    public bool openPMenu = true;
    public bool canOpen()
    {
        return openPMenu;
    }


}
