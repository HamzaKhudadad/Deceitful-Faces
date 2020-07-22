﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using UnityEngine.Networking;
using System.Text;
using System.Linq;
using TMPro;


public class Gamecontroller3 : MonoBehaviour
{
    // ============================================================================================================================ CREATE UI ELEMENTS AND GENERAL VARIABLES ===

    // Canvas for the menu and in-game
    Canvas instructions;
    Canvas canvasGame;
    Canvas end;
    string expression = "happy" ;
    string emotion = "nervous";
    string[] expressions = { "angry", "disgust", "fear", "happy", "sad", "surprise", "neutral" };
    System.Random rnd = new System.Random();
    int question = 0;
    bool isRecording = true;
    private AudioSource audioSource;
    private AudioClip clip;
    static WebCamTexture backCam;
    private Texture background;

    public TextMeshProUGUI dialogueBox;
    public Button startbtn;
    public TextMeshProUGUI endnote;
    public Slider mSlider;
    public RawImage image;


    public static int id;
    public static int savedid;
    public static int sscore;
    public static int buttonNumber;
    public static int savedscore;
    private string _SavePath = "E:/snap/";
    int _CaptureCounter = 0;



    private string[] keywords = new string[] { "Tom.", "don't know.", "3rd street north.", "Yes.", "No.", "we werer friends.", "we worked together.",
        "he was a cop.", "there is a separate person for it.","in the safe.","0.9mm.","5mm.","less than a year.","around paradise hotel." };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public string word;
    public KeywordRecognizer recognizer;


    // buttons for the menu
    

    // in-game buttons
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button Mainmenu;
    private Button buttonClueList;
    private Button buttonMainMenu;
    private Button buttonRestart;

    // Main dialogue box, clue list and money available

    private Text dialogueBoxClueList;
    private Text dialogueBoxCash;
    private string[] list=new string[20];



    // variables for various 
   
    private float letterPause = 0.050f;
    private bool displayClueList = false;

    



    // ============================================================================================================================ CREATE NODES ===

    // instance of the player
    PlayerQ steele = new PlayerQ();
    Score score = new Score();



    // holds information about the most recent purchase (used in NodeNormal() to determine 
    Item lastItem = new Item("lastItem", 0);

    // available items to buy


    // current node tracks the player's progress. It's set again every time the player enters a new node
    Node currentNode = new Node();

    // a mandatory 'useless' node. Is not displayed in the game, but serves a function behind the scenes. Required for Start New Game to work properly when used mid-game among other thing.
    Node start = new Node();

    // Mikko's nodes
    Node office01 = new Node();
    Node office02 = new Node();
    Node office = new Node();
    Node nnew = new Node();
   Node work = new Node();
    Node company = new Node();
    Node visit = new Node();
    Node arrest = new Node();
    Node why = new Node();
    Node record = new Node();
    Node who = new Node();
    Node keep = new Node();
    Node gun = new Node();
    Node calliber = new Node();
    Node whyg = new Node();
    Node where = new Node();
    Node finish = new Node();


    // ============================================================================================================================ CREATE NEXTNODE LISTS ===

    private List<Node> nn_start = new List<Node>();

    // Mikko's next node lists
    private List<Node> nn_office01 = new List<Node>();
    private List<Node> nn_office02 = new List<Node>();
    private List<Node> nn_office = new List<Node>();
    private List<Node> nn_new = new List<Node>();
    private List<Node> nwork = new List<Node>();
    private List<Node> ncompany = new List<Node>();
    private List<Node> nvisit = new List<Node>();
    private List<Node> narrest = new List<Node>();
    private List<Node> nwhy= new List<Node>();
    private List<Node> nrecord = new List<Node>();
    private List<Node> nwho = new List<Node>();
    private List<Node> nkeep = new List<Node>();
    private List<Node> ngun = new List<Node>();
    private List<Node> ncalliber = new List<Node>();
    private List<Node> nwhyg = new List<Node>();
    private List<Node> nwhere = new List<Node>();
    private List<Node> nfinish = new List<Node>();



    // ============================================================================================================================ NODE DIALOGUES ===

    private string d_start = "";

    private string d_receipt = "";

    // Mikko's dialogue
    private string d_office01 = "What is the relation between you both?\n" +
        "\n1.\tfreinds.\t \n" +
        "\n2.\tbusiness partner.\t";

    private string d_office02 = "Why did you both came to lighthouse?\n" +
        "\n1.\tjust to visit.\t \n" +
        "\n2.\tto have a fun and relaxing time by yourselves.\t \n" +
        "\n3.\twe were planning it for a long time.\t";

    private string d_new = "For how long have you guys known each other?\n" +
        "\n1.\tless than 5 years.\t \n" +
        "\n2.\tsince childhood.\t  \n" +
        "\n3.\taround 10 years.\t";
    private string d_office = "Who else knew about you people coming here?\n" +
       "\n1.\tme and john only.\t \n" +
       "\n2.\tour families.\t \n" +
       "\n3.\tour some other mutual friend too.\t";
    private string d_work = "don’t you think it could be any of your friends who followed you and now messing with you to break you both up or something?\n" +
      "\n1.\tyes,might be possible.\t \n" +
      "\n2.\tI don’t think so.\t \n" +
      "\n3.\tNo.\t \n" +
      "\n4.\tnot sure.\t";
    private string d_company = "Do your families know each other?\n" +
        "\n1.\tYes.\t \n" +
        "\n2.\tNo.\t";
    private string d_visit = "Are your family members on good terms with each other’s ?\n" +
        "\n1.\tYes.\t \n" +
        "\n2.\tNo.\t";
    private string d_arrest = "Among both of you whose idea was this to come to lighthouse?\n" +
        "\n1.\tjohn’s.\t \n" +
        "\n2.\tMine.\t";
    private string d_why = "Did you guys had a fight before these mysterious events start to happen?\n" +
      "\n1.\tYes.\t \n" +
        "\n2.\tNo.\t";
    private string d_record = "You had this type of situation before?\n" +
    "\n1.\tNever.\t \n" +
    "\n2.\tsometimes I feel threatened by people around me.\t \n" +
    "\n3.\tI never gave it much thought.\t";
    private string d_who = "Are you seeking any medical help for it?\n" +
    "\n1.\tyes.\t \n" +
    "\n2.\tno.\t ";

    private string d_keep = "Have you ever felt threatened by john before?\n" +
        "\n1.\tyes.\t \n" +
        "\n2.\tno.\t";
    private string d_gun = "12.	Why would john want to kill you?\n" +
      "\n1.\the is jealos.\t \n" +
        "\n2.\the doesn’t like me.\t \n" +
        "\n3.\the wants my money.\t";
    private string d_calliber = "Did you had any bad terms with john ever before coming here?\n" +
   "\n1.\tno.\t \n" +
   "\n2.\tsometimes we have an argument.\t \n" +
   "\n3.\the always needed a reason to fight.\t";
    
   
    private string d_finish = "\t \t \t \tGAME OVER\n"+ "\t \t pres Esc key to exit";

    // ============================================================================================================================ DEFINE NODES ===

    private List<Node> Nodes = new List<Node>();

    private void DefineNode()
    {
        Nodes.Add(start);
        Nodes.Add(office01);
        Nodes.Add(office02);
        Nodes.Add(office);
        Nodes.Add(nnew);
        Nodes.Add(work);
        Nodes.Add(company);
        Nodes.Add(visit);
        Nodes.Add(arrest);
        Nodes.Add(why);
        Nodes.Add(record);
        Nodes.Add(who);
        Nodes.Add(arrest);
        Nodes.Add(keep);
        Nodes.Add(gun);
        Nodes.Add(calliber);
        Nodes.Add(whyg);
        Nodes.Add(where);



        // start
        nn_start.Add(office01);
        start.AddNextNodes(nn_start);
        start.SetHasClue(true);
        start.SetId(0);

        // office01
        nn_office01.Add(office02); nn_office01.Add(nnew);
        office01.AddNextNodes(nn_office01);
        office01.SetDialogue(d_office01);
        office01.SetScore(60);
        office01.SetTotal(100);
        office01.SetAnswer("freinds.");
        office01.SetId(1);
        office01.SetExpression("happy");
        office01.SetEmotion("nervous");
       

        // office02
        nn_office02.Add(work); nn_office02.Add(company); nn_office02.Add(visit);
        office02.AddNextNodes(nn_office02);
        office02.SetScore(60);
        office02.SetTotal(100);
        office02.SetAnswer("to have a fun and relaxing time by yourselves.");
        office02.SetExpression("surprised");
        office02.SetEmotion("nervous");
        office02.SetDialogue(d_office02);
       
        office02.SetId(2);

        nn_office.Add(arrest); nn_office.Add(why); nn_office.Add(record);
        office.AddNextNodes(nn_office);
        office.SetScore(60);
        office.SetTotal(100);
        office.SetAnswer("our families.");
        office.SetExpression("surprised");
        office.SetEmotion("nervous");
        office.SetDialogue(d_office);
      
        office.SetId(3);

        // office
        nn_new.Add(office); nn_new.Add(gun); nn_new.Add(arrest);
        nnew.AddNextNodes(nn_new);
        nnew.SetScore(60);
        nnew.SetTotal(100);
        nnew.SetAnswer("around 10 years.");
        nnew.SetExpression("surprised");
        nnew.SetEmotion("nervous");
        nnew.SetDialogue(d_new);
        nnew.SetId(4);

        nwork.Add(who); nwork.Add(keep); nwork.Add(gun); nwork.Add(why);
        work.AddNextNodes(nwork);
        work.SetScore(60);
        work.SetTotal(100);
        work.SetAnswer("yes,might be possible.");
        work.SetExpression("surprised");
        work.SetEmotion("nervous");
        work.SetDialogue(d_work);
        work.SetId(5);

        ncompany.Add(gun); ncompany.Add(calliber);
        company.AddNextNodes(ncompany);
        company.SetScore(60);
        company.SetTotal(100);
        company.SetAnswer("Yes.");
        company.SetExpression("surprised");
        company.SetEmotion("nervous");
        company.SetDialogue(d_company);
        company.SetId(6);

        nvisit.Add(arrest); nvisit.Add(who);
        visit.AddNextNodes(nvisit);
        visit.SetScore(60);
        visit.SetTotal(100);
        visit.SetAnswer("Yes.");
        visit.SetExpression("surprised");
        visit.SetEmotion("nervous");
        visit.SetDialogue(d_visit);
        visit.SetId(7);

        narrest.Add(office02); narrest.Add(who);
        arrest.AddNextNodes(narrest);
        arrest.SetScore(60);
        arrest.SetTotal(100);
        arrest.SetAnswer("Mine.");
        arrest.SetExpression("surprised");
        arrest.SetEmotion("nervous");
        arrest.SetDialogue(d_arrest);
        arrest.SetId(8);


        nwhy.Add(who); nwhy.Add(office);
        why.AddNextNodes(nwhy);
        why.SetScore(60);
        why.SetTotal(100);
        why.SetAnswer("No.");
        why.SetExpression("surprised");
        why.SetEmotion("nervous");
        why.SetDialogue(d_why);
        why.SetId(9);

        nrecord.Add(who); nrecord.Add(why);nrecord.Add(keep);
        record.AddNextNodes(nrecord);
        record.SetScore(60);
        record.SetTotal(100);
        record.SetAnswer("Never.");
        record.SetExpression("surprised");
        record.SetEmotion("nervous");
        record.SetDialogue(d_record);
        record.SetId(10);


        nwho.Add(record); nwho.Add(gun);
        who.AddNextNodes(nwho);
        who.SetScore(60);
        who.SetTotal(100);
        who.SetAnswer("no.");
        who.SetExpression("surprised");
        who.SetEmotion("nervous");
        who.SetDialogue(d_who);
        who.SetId(11);

        nkeep.Add(work);nkeep.Add(who); 
        keep.AddNextNodes(nkeep);
        keep.SetScore(60);
        keep.SetTotal(100);
        keep.SetAnswer("no.");
        keep.SetExpression("surprised");
        keep.SetEmotion("nervous");
        keep.SetDialogue(d_keep);
        keep.SetId(12);

        ngun.Add(keep); ngun.Add(company); ngun.Add(work);
        gun.AddNextNodes(ngun);
        gun.SetScore(60);
        gun.SetTotal(100);
        gun.SetAnswer("he doesn’t like me.");
        gun.SetExpression("surprised");
        gun.SetEmotion("nervous");
        gun.SetDialogue(d_gun);
        gun.SetId(13);


        ncalliber.Add(who); ncalliber.Add(nnew);ncalliber.Add(why);
        calliber.AddNextNodes(ncalliber);
        calliber.SetScore(60);
        calliber.SetTotal(100);
        calliber.SetAnswer("no.");
        calliber.SetExpression("surprised");
        calliber.SetEmotion("nervous");
        calliber.SetDialogue(d_calliber);
        calliber.SetId(14);



        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    }


    // ========================================================================================================================================================= CREATE UI
    private void CreateUI()
    {

        canvasGame = (Canvas)GameObject.Find("canvasGame").GetComponent<Canvas>();
        instructions = (Canvas)GameObject.Find("canvasInstructions").GetComponent<Canvas>();
        end = (Canvas)GameObject.Find("canvasend").GetComponent<Canvas>();

        mSlider = (Slider)GameObject.Find("Suspicionmeter").GetComponent<Slider>();
        button1 = (Button)GameObject.Find("button1").GetComponent<Button>();                        //find gameplay buttons in Unity
        button2 = (Button)GameObject.Find("button2").GetComponent<Button>();
        button3 = (Button)GameObject.Find("button3").GetComponent<Button>();
        button4 = (Button)GameObject.Find("button4").GetComponent<Button>();
        buttonMainMenu = (Button)GameObject.Find("mainmenu").GetComponent<Button>();
        buttonRestart = (Button)GameObject.Find("restart").GetComponent<Button>();

        //find the box where the cluelist is printed in Unity
        dialogueBoxCash = (Text)GameObject.Find("dialogueBoxCash").GetComponent<Text>();            //find the box where the player cash is printed in Unity
                                                                                                    //  this.dialogueBoxClueList.gameObject.SetActive(true);
        this.dialogueBoxCash.gameObject.SetActive(true);
        this.dialogueBoxCash.text = score.GetScore().ToString();

        this.end.gameObject.SetActive(false);
        this.button1.gameObject.SetActive(false);
        this.button2.gameObject.SetActive(false);
        this.button3.gameObject.SetActive(false);
        this.button4.gameObject.SetActive(false);



        button1.onClick.AddListener(delegate { ButtonClicked(button1); EasyAudioUtility.instance.Play("Hover"); });                       //listeners for gameplay buttons
        button2.onClick.AddListener(delegate { ButtonClicked(button2); EasyAudioUtility.instance.Play("Hover"); });
        button3.onClick.AddListener(delegate { ButtonClicked(button3); EasyAudioUtility.instance.Play("Hover"); });
        button4.onClick.AddListener(delegate { ButtonClicked(button4); EasyAudioUtility.instance.Play("Hover"); });
        startbtn.onClick.AddListener(delegate { this.instructions.gameObject.SetActive(false); LoadGame(); EasyAudioUtility.instance.Play("Hover"); startbtn.gameObject.SetActive(false); });
        buttonMainMenu.onClick.AddListener(delegate { EasyAudioUtility.instance.Play("Hover"); SceneManager.LoadScene("MainMenu"); });
        buttonRestart.onClick.AddListener(delegate { NewGame(); EasyAudioUtility.instance.Play("Hover"); });

    }

    // ========================================================================================================================================================= BUTTON FUNCTIONALITY FOR MENUS


    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {

        word = args.text;
        string[] answer = { "" };
        answer = currentNode.GetDialogue().ToString().Split('\t');


        try
        {
            if (word.Equals(answer[1]))
            {
                NodeNormal(button1);
            }
            if (word.Equals(answer[3]))
            {
                NodeNormal(button2);
            }
            if (word.Equals(answer[5]))
            {
                NodeNormal(button3);
            }
            if (word.Equals(answer[4]))
            {
                NodeNormal(button4);
            }
        }
        catch { }



    }


    public void NewGame()
    {

        currentNode = office01;
        score.SetScore(100);
        this.dialogueBoxCash.text = "100";
        this.mSlider.value = -(score.GetScore());
        this.end.gameObject.gameObject.SetActive(false);
        this.canvasGame.gameObject.SetActive(true);
        StartCoroutine(TypeTextAndEnableButtonNormal(currentNode.GetDialogue()));
        Debug.Log("sn");

    }


    public static void getsavedvalues(int idl, int scorel, int btn)
    {
        savedid = idl;
        savedscore = scorel;
        



    }



    public void LoadGame()
    {

        Debug.Log(savedid);


        Node loadnode = new Node();
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i].GetID() == savedid)
            {
                loadnode = Nodes[i];
            }
        }


        if (savedid != 0)
        {

            mSlider.value = -(savedscore);

            this.dialogueBoxCash.text = savedscore.ToString();
            currentNode = loadnode;
           
            StartCoroutine(TypeTextAndEnableButtonNormal(currentNode.GetDialogue()));
        }
        else
        {
            currentNode = office01;
            

            this.canvasGame.gameObject.SetActive(true);
            StartCoroutine(TypeTextAndEnableButtonNormal(currentNode.GetDialogue()));
            Debug.Log("sn");
        }

    }




    // 	 =========================================================================================================================================================  BUTTON FUNCTIONALITY IN-GAME


    private void ButtonClicked(Button b)
    {

        int check = NodeNormal(b);

        if (check == 1)
        {
            this.button1.gameObject.SetActive(false);                   //by default, disable all the gameplay buttons until
            this.button2.gameObject.SetActive(false);                   //we know the possible options of currentNode
            this.button3.gameObject.SetActive(false);
            this.button4.gameObject.SetActive(false);
            this.end.gameObject.SetActive(true);

            endnote.text = "Congratulations!!! \n you were good in your responses \n now you are not our suspect ";
        }
        if (check == 2)
        {
            this.button1.gameObject.SetActive(false);                   //by default, disable all the gameplay buttons until
            this.button2.gameObject.SetActive(false);                   //we know the possible options of currentNode
            this.button3.gameObject.SetActive(false);
            this.button4.gameObject.SetActive(false);
            this.end.gameObject.SetActive(true);

            endnote.text = "Oops!!! \n there were inconsistency in your responses";
        }
    }

    // 	 ========================================================================================================================================================= 	EXECUTING A NODE
    //THE MAIN NODE FUNCTIONALITY IS EXECUTED HERE
    private int NodeNormal(Button b)
    {
        
        this.dialogueBox.text = "";         //clear the old dialogue text from the screen

        if (currentNode.GetNextIsReceipt() == true)
        {    //if the player came from a store node, subtract price of chosen item from player's funds
            steele.SetCash(-(currentNode.GetDic()[b].GetItemPrice()));
            this.lastItem = currentNode.GetDic()[b];
            currentNode.GetNextNodes()[0].SetDialogue("You bought " + lastItem.GetItemName() + " for the low low price of " + lastItem.GetItemPrice() + " moneydollars.");


        }
        question++;


                      //assign a number 1-4 to each of the gameplay buttons

        if (b == button1)
        {
            buttonNumber = 1;
        }
        if (b == button2)
        {
            buttonNumber = 3;
        }
        if (b == button3)
        {
            buttonNumber = 5;
        }
        if (b == button4)
        {
            buttonNumber = 4;
        }
        string[] answer = { "" };
        if (currentNode != start)
        {
            double temp = 0;
            if (currentNode.GetExpression().ToString().Equals(expressions[rnd.Next(1, 7)]))
            {
                temp = temp + (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 35);
            }
            else
            {
                temp = temp - (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 35);
            }
            if (currentNode.GetEmotion().ToString().Equals(expressions[rnd.Next(1, 7)]))
            {
                temp = temp + (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 15);
            }
            else
            {
                temp = temp - (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 15);
            }


            answer = currentNode.GetDialogue().ToString().Split('\t');


            if ((answer[buttonNumber].Equals(currentNode.GetAnswer())) || (word.Equals(currentNode.GetAnswer())))
            {
                Debug.Log("in if statement");
                temp = temp + (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 50);
                score.SetScore(currentNode.GetScore());
            }
            else
            {
                temp = temp - (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 50);
                score.SetScore(-currentNode.GetScore());
            }
            // Debug.Log(temp);
        }



        double percent = (double)score.GetScore() / 100.0;

        if (percent >= 1)
        {
            buttonNumber = 1;
        }
        else if (percent < 1.0 && percent >= 0.5)
        {
            buttonNumber = 2;
        }
        else
        {
            buttonNumber = 3;
        }


        if(buttonNumber==0){
            buttonNumber++;
}Debug.Log(currentNode.GetRepeted());
        if (score.GetScore() < -200)
        {
            return 2;
        }
        if (list.Contains(currentNode.GetDialogue()))
        {
            if (score.GetScore() > 400) {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        currentNode.Setrepeted(true);
        list[question - 1] = currentNode.GetDialogue();
        try
        {
        currentNode = currentNode.GetNextNodes()[buttonNumber - 1]; //set the new currentNode based on number of pressed button and the nextNodes list
          }catch{currentNode = currentNode.GetNextNodes()[buttonNumber - 2];}                                                          //of the previous node
         id = currentNode.GetID();




        this.button1.gameObject.SetActive(false);                   //by default, disable all the gameplay buttons until
        this.button2.gameObject.SetActive(false);                   //we know the possible options of currentNode
        this.button3.gameObject.SetActive(false);
        this.button4.gameObject.SetActive(false);

        StartCoroutine(TypeTextAndEnableButtonNormal(currentNode.GetDialogue())); //start stylized text output
                                                                                  //and enable correct gameplay buttons for the node
        this.dialogueBoxCash.text = score.GetScore().ToString();
        return 0;

    }




    // 	 ========================================================================================================================================================= STYLIZED TEXT OUTPUT & BUTTON ENABLING

    private IEnumerator TypeTextAndEnableButtonNormal(string textToPrint) //takes the dialogue string from currentNode as a parameter
    {
        if (_CaptureCounter > 0)
        {
            spech();
        }
        foreach (char letter in textToPrint.ToCharArray())
        {       //loops through the string and prints out one character on each iteration
                //Debug.Log(letter);
            this.dialogueBox.text += letter;
            yield return new WaitForSeconds(letterPause);
        }

        switch (currentNode.GetNextNodes().Count)
        {                   //activate as many buttons as there are nextNodes in the list

            case 1:
                this.button1.gameObject.SetActive(true);
                break;
            case 2:
                this.button1.gameObject.SetActive(true);
                this.button2.gameObject.SetActive(true);
                break;

            case 3:
                this.button1.gameObject.SetActive(true);
                this.button2.gameObject.SetActive(true);
                this.button3.gameObject.SetActive(true);
                break;

            case 4:
                this.button1.gameObject.SetActive(true);
                this.button2.gameObject.SetActive(true);
                this.button3.gameObject.SetActive(true);
                this.button4.gameObject.SetActive(true);
                break;
        }
       
        yield return new WaitForSeconds(1);
        spech();



    }


   


    // ========================================================================================================================================================= KEYBOARD COMMANDS

    

  

    //THIS METHOD WAS USED DURING TESTING FOR QUICKER GAMEPLAY. NOT INTENDED FOR PLAYERS (DEVELOPERS ONLY, FOOL).
    public void KeyboardButtonClicked()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ButtonClicked(button1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ButtonClicked(button2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ButtonClicked(button3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ButtonClicked(button4);
        }
    }

    // ========================================================================================================================================================= START METHOD

    //THIS SECTION IS EXECUTED WHEN THE GAME IS LAUNCHED. INCLUDES METHOD CALLS FOR ALL VITAL COMPONENTS.

    void Start()
    {

        CreateUI();


        //camera feed
        background = image.texture;
        if (backCam == null)
            backCam = new WebCamTexture();




        if (!backCam.isPlaying)
            backCam.Play();
        image.texture = backCam;


        dialogueBox.text = d_start;

        DefineNode();

        currentNode = start;



        if (question <= 0)
        {
            Debug.Log("recognizer initialized");
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();

            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
            }


            audioSource = GetComponent<AudioSource>();

            if (backCam == null)
                backCam = new WebCamTexture();




            if (!backCam.isPlaying)
                backCam.Play();


        }
    }


    public void Update()
    {
        mSlider.value = -(score.GetScore());
        sscore = score.GetScore();


        if (Input.GetKeyDown("space"))
        {

            isRecording = !isRecording;

            Debug.Log(isRecording == true ? "off" : "recording");

            if (isRecording == false)
            {

                audioSource.clip = Microphone.Start(null, true, 5, 44100);

            }
            else
            {
                int length = Microphone.GetPosition(null);

                Microphone.End(null);
                // clip = SavWav.TrimSilence(audioSource.clip, 10);
                var file = SavWav.Save("myfile", audioSource.clip);
                //clip.Play();
                //Debug.Log(file);
                byte[] bytes = wav.EncodeToWAV(audioSource.clip);
                StartCoroutine(Upload(file, bytes));
                isRecording = !isRecording;

            }

        }


        
    }



    IEnumerator Upload(string ufile, byte[] ubytes)
    {


        //Debug.Log(ufile);
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        WWWForm form = new WWWForm();
        form.AddBinaryData("file_name", ubytes, "myfile.wav", "audio/vnd.wav");
        // formData.Add(new MultipartFormDataSection());
        formData.Add(new MultipartFormFileSection("file_name", ufile));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/pred", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");

            }

            // Print Headers
            //Debug.Log(sb.ToString());


            // Print Body

            string[] lst = www.downloadHandler.text.Split('"');
            string prediction = lst[1];
            emotion = prediction;
            Debug.Log(prediction);
            // emot.text(prediction);

        }
    }

    //image 
    void TakeSnapshot()
    {
        Debug.Log("snap method");
        Texture2D snap = new Texture2D(backCam.width, backCam.height);
        snap.SetPixels(backCam.GetPixels());
        snap.Apply();
        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        var bytess = snap.EncodeToPNG();
        Destroy(snap);
        StartCoroutine(uploadimage(_SavePath + _CaptureCounter.ToString() + ".png", bytess));

            ++_CaptureCounter;
        }

   
    void spech()
    {
        isRecording = !isRecording;

        Debug.Log(isRecording == true ? "off" : "recording");

        if (isRecording == false)
        {

            audioSource.clip = Microphone.Start(null, true, 5, 44100);

        }
        else
        {
            int length = Microphone.GetPosition(null);

            Microphone.End(null);
            // clip = SavWav.TrimSilence(audioSource.clip, 10);
            var file = SavWav.Save("myfile", audioSource.clip);
            //clip.Play();
            //Debug.Log(file);
            byte[] bytes = wav.EncodeToWAV(audioSource.clip);
            StartCoroutine(Upload(file, bytes));
            isRecording = !isRecording;

        }
    }
    IEnumerator uploadimage(string ufile, byte[] ubytes)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        var form = new WWWForm();
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("file", ubytes, ufile, "image/png");
        formData.Add(new MultipartFormFileSection("file_name", ufile));

        UnityWebRequest www = UnityWebRequest.Post("https://emotron.herokuapp.com/pictureapi", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");

            }

            // Print Headers
            //Debug.Log(sb.ToString());


            // Print Body

            string[] lst = www.downloadHandler.text.Split('"');
            string prediction = lst[0];
            expression = prediction;
            Debug.Log(prediction);
        }

    }
}
    
