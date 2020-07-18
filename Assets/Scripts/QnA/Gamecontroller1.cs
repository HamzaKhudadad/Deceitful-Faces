using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;


public class Gamecontroller1 : MonoBehaviour
{
    // ============================================================================================================================ CREATE UI ELEMENTS AND GENERAL VARIABLES ===

    // Canvas for the menu and in-game
    Canvas canvasMainMenu;
    Canvas canvasGame;
    string expression = "happy";
    string emotion = "nervous";
    int question = 0;


    private string[] keywords = new string[] { "killer", "blood", "crowed", "Yes", "No", "It was my first time", "I have no idea", "Run away.", "told the police about incident" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public string word;
    public KeywordRecognizer recognizer;


    // buttons for the menu
    private Button buttonResume;
    private Button buttonStartNewGame;
    private Button buttonQuit;

    // in-game buttons
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
   // private Button buttonClueList;
    private Button buttonMainMenu;

    // Main dialogue box, clue list and money available
    private Text dialogueBox;
    private Text dialogueBoxClueList;
    private Text dialogueBoxCash;


    // audio source for buttons, music and sound effects
    public AudioSource buttonSoundSource;
    public AudioSource nodeMusicSource;
    public AudioSource nodeSoundSource;

    // variables for various 
    private bool prevHasMusic;
    private float letterPause = 0.000025f;
    private bool displayClueList = false;

    // ============================================================================================================================ AUDIO AND BACKGROUNDS ===

    // sound effects (one shot sounds in a node)
    public AudioClip a_silence;
    public AudioClip a_buttonClickAudio;
    public AudioClip a_wilhelmScream;
    public AudioClip a_carPassing;
    public AudioClip a_delayedSwitch;
    public AudioClip a_doorOpenClose;
    public AudioClip a_dramaticHit;
    public AudioClip a_manShredded;
    public AudioClip a_pillBottle;
    public AudioClip a_rustleFlipPapers;
    public AudioClip a_strikeMatch;
    public AudioClip a_tryDial;
    public AudioClip a_megaThunder;
    public AudioClip a_phoneRingPickup;
    public AudioClip a_pickup;
    public AudioClip a_ring;

    // music (looping sounds in a node)
    public AudioClip m_prevMusic;

    public AudioClip m_menuMusic;
    public AudioClip m_rainHard;
    public AudioClip m_rainSofter;
    public AudioClip m_dramaticMusic;
    public AudioClip m_dramaticMusic2;
    public AudioClip m_storeMusic;

    // backgrounds for menu and in-game
    private Image backgroundMainMenu;
    private Image b_background;

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


    // ============================================================================================================================ CREATE NEXTNODE LISTS ===

    private List<Node> nn_start = new List<Node>();

    // Mikko's next node lists
    private List<Node> nn_office01 = new List<Node>();
    private List<Node> nn_office02 = new List<Node>();
    private List<Node> nn_office = new List<Node>();
    private List<Node> nn_new = new List<Node>();



    // ============================================================================================================================ NODE DIALOGUES ===

    private string d_start = "jaska jaska";

    private string d_receipt = "asdasd";

    // Mikko's dialogue
    private string d_office01 = "what did you saw when you enterd the room?\n" +
        "\n1.\tkiller\t \n" +
        "\n2.\tblood\t \n" +
        "\n3.\tcrowed\t";

    private string d_office02 = "Do you used to visit xyz store regulary?\n" +
        "\n1.\tYes\t \n" +
        "\n2.\tNo\t \n" +
        "\n3.\tIt was my first time.\t";

    private string d_new = "Did anyone else see it happen?\n" +
        "\n1.\tYes\t \n" +
        "\n2.\tNo\t \n" +
        "\n3.\tI have no idea\t";
    private string d_office = "What was your first reaction to the incident?\n" +
       "\n1.\tRun away\t \n" +
       "\n2.\ttold the police about incident\t \n" +
       "\n3.\tNothing\t";


    public string[] Keywords { get => keywords; set => keywords = value; }




    // ============================================================================================================================ DEFINE NODES ===
    private void DefineNode()
    {

        // start
        nn_start.Add(office01);
        start.AddNextNodes(nn_start);
        start.SetHasAudioClip(true);
        start.SetMyAudioClip(a_silence);
        start.SetHasMusicClip(true);
        start.SetMyMusicClip(m_menuMusic);
        start.SetHasClue(true);


        // office01
        nn_office01.Add(office02); nn_office01.Add(office); nn_office01.Add(nnew);
        office01.AddNextNodes(nn_office01);
        office01.SetDialogue(d_office01);
        office01.SetScore(60);
        office01.SetTotal(100);
        office01.SetAnswer("killer.");


        office01.SetExpression("happy");
        office01.SetEmotion("nervous");
        office01.SetHasAudioClip(true);
        office01.SetMyAudioClip(a_ring);
        office01.SetHasMusicClip(true);
        office01.SetMyMusicClip(m_rainSofter);

        // office02
        nn_office02.Add(office); nn_office02.Add(office01); nn_office02.Add(nnew);
        office02.AddNextNodes(nn_office02);
        office02.SetScore(60);
        office02.SetTotal(100);
        office02.SetAnswer("Yes.");
        office02.SetExpression("surprised");
        office02.SetEmotion("nervous");
        office02.SetDialogue(d_office02);
        office02.SetHasAudioClip(true);
        office02.SetMyAudioClip(a_pickup);
        office02.SetHasMusicClip(true);
        office02.SetMyMusicClip(m_rainSofter);
       

        nn_office.Add(office02); nn_office.Add(nnew); nn_office.Add(office01);
        office.AddNextNodes(nn_office);
        office.SetScore(60);
        office.SetTotal(100);
        office.SetAnswer("Run away.");
        office.SetExpression("surprised");
        office.SetEmotion("nervous");
        office.SetDialogue(d_office);
        office.SetHasAudioClip(true);
        office.SetMyAudioClip(a_pickup);
        office.SetHasMusicClip(true);
        office.SetMyMusicClip(m_rainSofter);


        // office
        nn_new.Add(office01); nn_new.Add(office02); nn_new.Add(office);
        nnew.AddNextNodes(nn_new);
        nnew.SetScore(60);
        nnew.SetTotal(100);
        nnew.SetAnswer("No.");
        nnew.SetExpression("surprised");
        nnew.SetEmotion("nervous");
        nnew.SetDialogue(d_new);
        office.SetHasAudioClip(true);
        office.SetMyAudioClip(a_pickup);
        office.SetHasMusicClip(true);
        office.SetMyMusicClip(m_rainSofter);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    }


    // ========================================================================================================================================================= CREATE UI
    private void CreateUI()
    {
        
        canvasGame = (Canvas)GameObject.Find("canvasGame").GetComponent<Canvas>();


        

        button1 = (Button)GameObject.Find("button1").GetComponent<Button>();                        //find gameplay buttons in Unity
        button2 = (Button)GameObject.Find("button2").GetComponent<Button>();
        button3 = (Button)GameObject.Find("button3").GetComponent<Button>();
        button4 = (Button)GameObject.Find("button4").GetComponent<Button>();
        //buttonClueList = (Button)GameObject.Find("buttonClueList").GetComponent<Button>();      //find the button to toggle cluelist and wallet in Unity


        dialogueBox = (Text)GameObject.Find("dialogueBox").GetComponent<Text>();                    //find the box where the node dialogue is printed in Unity
        dialogueBoxClueList = (Text)GameObject.Find("dialogueBoxClueList").GetComponent<Text>();    //find the box where the cluelist is printed in Unity
        dialogueBoxCash = (Text)GameObject.Find("dialogueBoxCash").GetComponent<Text>();            //find the box where the player cash is printed in Unity
        this.dialogueBoxClueList.gameObject.SetActive(true);
        this.dialogueBoxCash.gameObject.SetActive(true);
        this.dialogueBoxCash.text = score.GetScore().ToString();


        //  b_background = (Image)GameObject.Find("b_background").GetComponent<Image>();
        /*
		backgroundMainMenu = (Image)GameObject.Find("backgroundMainMenu").GetComponent<Image>();
		backgroundGame = (Image)GameObject.Find("backgroundGame").GetComponent<Image>();
		*/

        //  buttonSoundSource = (AudioSource)GameObject.Find("buttonSoundSource").GetComponent<AudioSource>();      //separate AudioSources for music and other audio
        //  nodeMusicSource = (AudioSource)GameObject.Find("nodeMusicSource").GetComponent<AudioSource>();
        //  nodeSoundSource = (AudioSource)GameObject.Find("nodeSoundSource").GetComponent<AudioSource>();

      

        button1.onClick.AddListener(delegate { ButtonClicked(button1); });                       //listeners for gameplay buttons
        button2.onClick.AddListener(delegate { ButtonClicked(button2); });
        button3.onClick.AddListener(delegate { ButtonClicked(button3); });
        button4.onClick.AddListener(delegate { ButtonClicked(button4); });

        //buttonClueList.onClick.AddListener(delegate { ToggleClueListAndCash(); });
    }

    // ========================================================================================================================================================= BUTTON FUNCTIONALITY FOR MENUS


    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        string[] answer = { "" };
        answer = currentNode.GetDialogue().ToString().Split('\t');

        
        Debug.Log(answer[1]);
        Debug.Log(answer[3]);
        Debug.Log(answer[5]);
        Debug.Log(word);
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



    public void StartNewGame()                              //is called when Start New Game is clicked in the menu
    {
        currentNode = start;
        dialogueBox.text = d_start;
        steele.GetClueList().Clear();

        if (steele.GetClueList().Count == 0)
        {
            dialogueBoxClueList.text = "";
        }

        steele.ResetCash();
        UpdateCash();
        //this.canvasMainMenu.gameObject.SetActive(false);
        this.canvasGame.gameObject.SetActive(true);
        StartNewAdventureAutoKey();
    }

   

   

    


    // 	 =========================================================================================================================================================  BUTTON FUNCTIONALITY IN-GAME


    private void ButtonClicked(Button b)
    {

        NodeNormal(b);
    }

    // 	 ========================================================================================================================================================= 	EXECUTING A NODE
    //THE MAIN NODE FUNCTIONALITY IS EXECUTED HERE
    private void NodeNormal(Button b)
    {
      
        this.dialogueBox.text = "";         //clear the old dialogue text from the screen

        if (currentNode.GetNextIsReceipt() == true)
        {    //if the player came from a store node, subtract price of chosen item from player's funds
            steele.SetCash(-(currentNode.GetDic()[b].GetItemPrice()));
            this.lastItem = currentNode.GetDic()[b];
            currentNode.GetNextNodes()[0].SetDialogue("You bought " + lastItem.GetItemName() + " for the low low price of " + lastItem.GetItemPrice() + " moneydollars.");

            UpdateCash();

        }
        question++;


        int buttonNumber = 0;                   //assign a number 1-4 to each of the gameplay buttons

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
            if (currentNode.GetExpression().ToString().Equals(expression))
            {
                temp = temp + (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 35);
            }
            else
            {
                temp = temp - (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 35);
            }
            if (currentNode.GetEmotion().ToString().Equals(emotion))
            {
                temp = temp + (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 15);
            }
            else
            {
                temp = temp - (((double)currentNode.GetScore() / (double)currentNode.GetTotal()) * 15);
            }


            answer = currentNode.GetDialogue().ToString().Split('\t');

    
            if ((answer[buttonNumber].Equals(currentNode.GetAnswer())) || (word.Equals(currentNode.GetAnswer())) )
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


        this.m_prevMusic = currentNode.GetMyMusicClip();        //these are set here in case two subsequent nodes have the same music clip
        this.prevHasMusic = currentNode.GetHasMusicClip();      //in that case, the music continues without interruption (see below)
      

        currentNode = currentNode.GetNextNodes()[buttonNumber - 1]; //set the new currentNode based on number of pressed button and the nextNodes list
                                                                    //of the previous node


        if (currentNode.GetHasMusicClip())
        {
            if (prevHasMusic == false)
            {
                PlayMusic();
            }
            else
            {
                if (!(this.m_prevMusic.Equals(currentNode.GetMyMusicClip())))
                {
                    nodeMusicSource.Stop();
                    PlayMusic();

                }
            }

        }
        else
        {
            //nodeMusicSource.Stop();
        }

        PlayClip();                                                 //if the current node has an audio clip, play it


        this.button1.gameObject.SetActive(false);                   //by default, disable all the gameplay buttons until
        this.button2.gameObject.SetActive(false);                   //we know the possible options of currentNode
        this.button3.gameObject.SetActive(false);
        this.button4.gameObject.SetActive(false);
       
        StartCoroutine(TypeTextAndEnableButtonNormal(currentNode.GetDialogue())); //start stylized text output
                                                                                  //and enable correct gameplay buttons for the node
        this.dialogueBoxCash.text = score.GetScore().ToString();




    }


    private void PlayClip()                                 //the method used for playing an audioclip if the node has one
    {
        if (currentNode.GetHasAudioClip() == true)
        {
            //nodeSoundSource.PlayOneShot(currentNode.GetMyAudioClip());
        }
    }

    private void PlayMusic()                                //...and the same for music 
    {
        /* if (currentNode.GetHasMusicClip() == true)
        {
            nodeMusicSource.clip = currentNode.GetMyMusicClip();
            nodeMusicSource.Play();
        }*/
    }

    // 	 ========================================================================================================================================================= STYLIZED TEXT OUTPUT & BUTTON ENABLING

    private IEnumerator TypeTextAndEnableButtonNormal(string textToPrint) //takes the dialogue string from currentNode as a parameter
    {

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

      

    }


    private void UpdateCash()                                               //write the new cash amount to the screen
    {
        this.dialogueBoxCash.text = score.GetScore().ToString();
    }



    private void ToggleClueListAndCash()
    {
        if (this.displayClueList == false)
        {
            UpdateCash();
            this.dialogueBoxClueList.gameObject.SetActive(true);
            this.dialogueBoxCash.gameObject.SetActive(true);
            this.displayClueList = true;
        }
        else
        {
            this.dialogueBoxClueList.gameObject.SetActive(false);
            this.dialogueBoxCash.gameObject.SetActive(false);
            this.displayClueList = false;
        }
    }

    // ========================================================================================================================================================= KEYBOARD COMMANDS

    

    public void StartNewAdventureAutoKey()
    {
        ButtonClicked(button1);
    }

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

        dialogueBox.text = d_start;

        DefineNode();

        currentNode = start;


      
        // currentNode.SetMyMusicClip(m_menuMusic);
        // nodeMusicSource.clip = currentNode.GetMyMusicClip();
        // nodeMusicSource.Play();

        Debug.Log("recognizer initialized");
        recognizer = new KeywordRecognizer(keywords, confidence);
        recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        recognizer.Start();
        StartNewGame();

    }


    public void Update()
    {
       
        //KeyboardButtonClicked();					// uncomment if you want to ski
    }

}