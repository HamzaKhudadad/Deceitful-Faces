
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // ============================================================================================================================ CREATE UI ELEMENTS AND GENERAL VARIABLES ===

    // Canvas for the menu and in-game
    Canvas canvasMainMenu;
    Canvas canvasGame;

    // buttons for the menu
    private Button buttonResume;
    private Button buttonStartNewGame;
    private Button buttonQuit;

    // in-game buttons
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button buttonClueList;
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
    private float letterPause = 0.00025f;
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

    Node receipt = new Node();

    // holds information about the most recent purchase (used in NodeNormal() to determine 
    Item lastItem = new Item("lastItem", 0);

    // available items to buy
    Item jimBeam = new Item("a bottle of Jim Beam", 30);
    Item balvenie = new Item("a bottle of Balvenie 30yo", 80);
    Item vodka = new Item("a bottle of the cheapest vodka", 10);
    Item nothing = new Item("nothing", 0);

    // current node tracks the player's progress. It's set again every time the player enters a new node
    Node currentNode = new Node();

    // a mandatory 'useless' node. Is not displayed in the game, but serves a function behind the scenes. Required for Start New Game to work properly when used mid-game among other thing.
    Node start = new Node();

    // Mikko's nodes
    Node office01 = new Node();
    Node office02 = new Node();
    Node office = new Node();
    Node f_officeWindow = new Node();
    Node f_officeDigging = new Node();
    Node f_officeFiles = new Node();
    Node f_officePhone = new Node();
    Node f_victimStreets = new Node();
    Node f_victimOutsideBuilding = new Node();
    Node f_victimStairs = new Node();
    Node f_victimStairs2 = new Node();
    Node f_victimElevator = new Node();
    Node f_victimElevator2 = new Node();
    Node f_victimCarry = new Node();
    Node f_victimCarry2 = new Node();
    Node f_victimMeetCops = new Node();
    Node f_victimFakeBadge = new Node();
    Node f_victimFakeBadge2 = new Node();
    Node f_victimFriendly = new Node();
    Node f_victimPoliceQuestions = new Node();
    Node f_victimPoliceQuestionsVictim = new Node();
    Node f_victimPoliceQuestionsWhen = new Node();
    Node f_victimPoliceQuestionsScene = new Node();
    Node f_victimPoliceQuestionsScene2 = new Node();
    Node f_victimPoliceQuestionsWhyHere = new Node();
    Node f_victimApartment = new Node();
    Node f_victimApartmentBathroom = new Node();
    Node f_victimApartmentBathroomApartment = new Node();
    Node f_victimApartmentBathroom2 = new Node();
    Node f_victimApartmentBedroom = new Node();
    Node f_victimApartmentBedroomPhoto = new Node();
    Node f_victimApartmentBedroomPhoto2 = new Node();
    Node f_victimApartmentBedroomDrawer = new Node();
    Node f_victimApartmentBedroomDrawer2 = new Node();
    Node f_victimApartmentBedroomConclusion = new Node();
    Node f_victimApartmentBedroomLiving = new Node();
    Node f_victimApartmentLivingroom = new Node();
    Node f_victimApartmentLivingroomBed = new Node();
    Node f_victimApartmentLivingroomBedPhoto = new Node();
    Node f_victimApartmentLivingroomBedPhoto2 = new Node();
    Node f_victimApartmentLivingroomBedDrawer = new Node();
    Node f_victimApartmentLivingroomBedDrawer2 = new Node();
    Node f_victimApartmentLivingroomBedConclusion = new Node();
    Node f_victimApartmentWindow = new Node();

    // Tuomas' nodes
    Node c_officeStreet = new Node();
    Node c_liquorStoreEnter = new Node();
    Node c_liquorStore = new Node();
    Node c_liquorStoreReceipt = new Node();
    Node c_streetAfterStore = new Node();
    Node c_policeStationLobby = new Node();
    Node c_policeStationClerkStart = new Node();
    Node c_policeStationClerkReyes = new Node();
    Node c_policeStationWalkOut = new Node();
    Node c_policeStationEavesdrop = new Node();
    Node c_policeStationExitStreet = new Node();
    Node c_policeStationClerkReyesRaiseVoice = new Node();
    Node c_policeStationClerkReyesOthers = new Node();
    Node c_policeStationClerkReyesOthersLeavesDesk = new Node();
    Node c_policeStationCommissionerStart = new Node();
    Node c_policeStationCommissionerGreet = new Node();
    Node c_policeStationCommissionerCorbin = new Node();
    Node c_policeStationCommissionerPush = new Node();
    Node c_policeStationClerkAnybody = new Node();
    Node c_policeStationClerkAnybodyLie = new Node();
    Node c_bar = new Node();
    Node c_CorbinsOutside = new Node();
    Node c_CorbinsHallway = new Node();
    Node c_CorbinsApartmentEnter = new Node();
    Node c_CorbinsApartment = new Node();
    Node c_CorbinsApartmentKitchen = new Node();
    Node c_CorbinsApartmentNightstand = new Node();
    Node c_CorbinsApartmentDiary = new Node();
    Node c_CorbinsApartmentDiary2 = new Node();
    Node c_CorbinsApartmentDresser = new Node();
    Node c_CorbinsGreyStart = new Node();
    Node c_CorbinsGreyStraightToCarver = new Node();
    Node c_CorbinsGreyGentlyToSubject = new Node();
    Node c_CorbinsGreyHintBar = new Node();
    Node c_CorbinsGreyExitStreet = new Node();
    Node c_CorbinsApartmentExitStreet = new Node();
    Node c_fatAngelosEnter = new Node();
    Node c_FatAngelosBartenderStart = new Node();
    Node c_FatAngelosBartenderNotInterested = new Node();
    Node c_FatAngelosTakeSeatStart = new Node();
    Node c_FatAngelosTakeSeatCarver = new Node();
    Node c_FatAngelosTakeSeatCarver2 = new Node();
    Node c_FatAngelosTakeSeatCarver3 = new Node();
    Node c_FatAngelosExitStreet = new Node();

    Node payUsMoney = new Node();

    // ============================================================================================================================ CREATE NEXTNODE LISTS ===

    private List<Node> nn_start = new List<Node>();

    // Mikko's next node lists
    private List<Node> nn_office01 = new List<Node>();
    private List<Node> nn_office02 = new List<Node>();
    private List<Node> nn_office = new List<Node>();
    private List<Node> nn_f_officeWindow = new List<Node>();
    private List<Node> nn_f_officeDigging = new List<Node>();
    private List<Node> nn_f_officeFiles = new List<Node>();
    private List<Node> nn_f_officePhone = new List<Node>();
    private List<Node> nn_f_victimStreets = new List<Node>();
    private List<Node> nn_f_victimOutsideBuilding = new List<Node>();
    private List<Node> nn_f_victimStairs = new List<Node>();
    private List<Node> nn_f_victimStairs2 = new List<Node>();
    private List<Node> nn_f_victimElevator = new List<Node>();
    private List<Node> nn_f_victimElevator2 = new List<Node>();
    private List<Node> nn_f_victimCarry = new List<Node>();
    private List<Node> nn_f_victimCarry2 = new List<Node>();
    private List<Node> nn_f_victimMeetCops = new List<Node>();
    private List<Node> nn_f_victimFakeBadge = new List<Node>();
    private List<Node> nn_f_victimFakeBadge2 = new List<Node>();
    private List<Node> nn_f_victimFriendly = new List<Node>();
    private List<Node> nn_f_victimPoliceQuestions = new List<Node>();
    private List<Node> nn_f_victimPoliceQuestionsVictim = new List<Node>();
    private List<Node> nn_f_victimPoliceQuestionsWhen = new List<Node>();
    private List<Node> nn_f_victimPoliceQuestionsScene = new List<Node>();
    private List<Node> nn_f_victimPoliceQuestionsScene2 = new List<Node>();
    private List<Node> nn_f_victimPoliceQuestionsWhyHere = new List<Node>();
    private List<Node> nn_f_victimApartment = new List<Node>();
    private List<Node> nn_f_victimApartmentBathroom = new List<Node>();
    private List<Node> nn_f_victimApartmentBathroomApartment = new List<Node>();
    private List<Node> nn_f_victimApartmentBathroom2 = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroom = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroomPhoto = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroomPhoto2 = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroomDrawer = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroomDrawer2 = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroomConclusion = new List<Node>();
    private List<Node> nn_f_victimApartmentBedroomLiving = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroom = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroomBed = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroomBedPhoto = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroomBedPhoto2 = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroomBedDrawer = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroomBedDrawer2 = new List<Node>();
    private List<Node> nn_f_victimApartmentLivingroomBedConclusion = new List<Node>();
    private List<Node> nn_f_victimApartmentWindow = new List<Node>();

    // Tuomas' next node lists
    private List<Node> nn_c_officeStreet = new List<Node>();
    private List<Node> nn_c_liquorStoreEnter = new List<Node>();
    private List<Node> nn_c_liquorStore = new List<Node>();
    private List<Node> nn_c_liquorStoreReceipt = new List<Node>();
    private List<Node> nn_c_streetAfterStore = new List<Node>();
    private List<Node> nn_c_policeStationLobby = new List<Node>();
    private List<Node> nn_c_policeStationClerkStart = new List<Node>();
    private List<Node> nn_c_policeStationClerkReyes = new List<Node>();
    private List<Node> nn_c_policeStationClerkReyesRaiseVoice = new List<Node>();
    private List<Node> nn_c_policeStationClerkReyesOthers = new List<Node>();
    private List<Node> nn_c_policeStationClerkReyesOthersLeavesDesk = new List<Node>();
    private List<Node> nn_c_policeStationClerkAnybody = new List<Node>();
    private List<Node> nn_c_policeStationClerkAnybodyLie = new List<Node>();
    private List<Node> nn_c_policeStationCommissionerStart = new List<Node>();
    private List<Node> nn_c_policeStationCommissionerGreet = new List<Node>();
    private List<Node> nn_c_policeStationCommissionerCorbin = new List<Node>();
    private List<Node> nn_c_policeStationCommissionerPush = new List<Node>();
    private List<Node> nn_c_policeStationWalkOut = new List<Node>();
    private List<Node> nn_c_policeStationEavesdrop = new List<Node>();
    private List<Node> nn_c_policeStationExitStreet = new List<Node>();
    private List<Node> nn_c_bar = new List<Node>();
    private List<Node> nn_c_CorbinsOutside = new List<Node>();
    private List<Node> nn_c_CorbinsHallway = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentEnter = new List<Node>();
    private List<Node> nn_c_CorbinsApartment = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentKitchen = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentNightstand = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentDresser = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentDiary = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentDiary2 = new List<Node>();
    private List<Node> nn_c_CorbinsGreyStart = new List<Node>();
    private List<Node> nn_c_CorbinsGreyStraightToCarver = new List<Node>();
    private List<Node> nn_c_CorbinsGreyGentlyToSubject = new List<Node>();
    private List<Node> nn_c_CorbinsGreyHintBar = new List<Node>();
    private List<Node> nn_c_CorbinsGreyExitStreet = new List<Node>();
    private List<Node> nn_c_CorbinsApartmentExitStreet = new List<Node>();
    private List<Node> nn_c_fatAngelosEnter = new List<Node>();
    private List<Node> nn_c_FatAngelosBartenderStart = new List<Node>();
    private List<Node> nn_c_FatAngelosBartenderNotInterested = new List<Node>();
    private List<Node> nn_c_FatAngelosTakeSeatStart = new List<Node>();
    private List<Node> nn_c_FatAngelosTakeSeatCarver = new List<Node>();
    private List<Node> nn_c_FatAngelosTakeSeatCarver2 = new List<Node>();
    private List<Node> nn_c_FatAngelosTakeSeatCarver3 = new List<Node>();
    private List<Node> nn_c_FatAngelosExitStreet = new List<Node>();


    // ============================================================================================================================ NODE DIALOGUES ===

    private string d_start = "jaska jaska";

    private string d_receipt = "asdasd";

    // Mikko's dialogue
    private string d_office01 = "Just another Tuesday afternoon. I’m sitting in my office, trying to kill time by playing chess against myself, since business " +
        "has been dead for months. Although one would imagine there was some demand for a private eye in this town... Nothing is blossoming here except for crime. " +
        "I should have pursued a career as a hot dog salesman.\n" +
        "\n1. Pick up the phone." +
        "\n2. not Pick up the phone.";

    private string d_office02 = "It's Reyes, my old partner from the force \"I’m sure you’ve read about the case of Stephanie Corbin by now? The hooker that was found dead in her apartment a couple days " +
        "ago? Anyway, I’m not so sure it was a suicide no more. We might be dealing with a homicide here." +
        "\n\nI thought that you might want to look into this a bit further. I’m pretty " +
        "sure the higher ups are gonna pull the plug on this one real soon. Not really top priority stuff if you catch my drift.\n" +
        "\n1. Tell Lt. Reyes that you will think about it and thank him for the tip.";

    private string d_office = "A suicide that isn’t a suicide. Nothing is like it seems in the windy city. It’ll take you for all you’ve got if you ain’t holding on to what’s yours." +
        "\n\nI have a bad feeling about this.  My feelings serve me well, but I won’t let the could my judgement. What is my plan, P. Steele?\n" +
        "\n1. Head over to my office's window. I've gotta think things through." +
        "\n2. Grab the umbrella from the corner and leave the office to take a stroll in the rain.";

    private string d_f_officeWindow = "I stalked over to the window. Like most autumn days, it was pouring. While staring outside I got a feeling that, just like the rain, " +
        "this case was going to go a whole lot worse before it got any better...\n" +
        "\n1. Do some digging about the victim. Search your files and make a few calls." +
        "\n2. Head out to the victim's apartment. Ought to find something to go on from there.";

    private string d_f_officeDigging = "I stare at my desk. It looks woefully unprofessional with towering stack of papers, unorganized files and an old telephone from the 1970's.\n" +
        "\n1. Search through my files and other records in hope of finding some links to the case." +
        "\n2. Pick up the phone and start calling up your list of contacts for information. You'd be surprised how much of my time is spent of the phone.";

    private string d_f_officeFiles = "The desk is an mess. Although I've grown accustomed to navigating its peaks and depths, specific information seems to be hard to find. As far as " +
        "I can tell, I've got no prior information relating to the case at hand. On my way out I pick up my jacket, some cash and my rusty old .38, just in case.\n" +
        "\n1. Head on out to the victim's apartment. Ought to find something useful there.";

    private string d_f_officePhone = "I pick up the phone to start dialing through my contact list, but I find it to be dead. The storm must be wrecking havoc with the phone lines. " +
        "It's gonna be one of those nights... On my way out I pick up my jacket, some cash and my rusty old .38, just in case.\n" +
        "\n1. Head on out to the victim's apartment. Ought to find something useful there.";

    private string d_f_victimStreets = "I drudge over to Stephanie's apartment taking the long way around, sticking to illuminated streets. I could have made it there in half the time" +
        "by taking the city's twisting alleyways, but I don't feel like gambling with my life so early in the evening. I get a sinking feeling that's gonna happen any way later on. " +
        "\n\n A single beat up squad car sits on the curb in front of the apartment complex.\n" +
        "\n1. Enter the lobby.";

    private string d_f_victimOutsideBuilding = "The building isn't especially fancy. From the appearance of the scruffy lobby you'd place it fairly low on the \"dirt poor to rich " +
        "as Sheik Ali Hassan\" scale. Peculiarily, there's a security guard reading a magazine near the stairwell. Stephanie's apartment number is 714. The seventh floor.\n" +
        "\n1. Take the stairs. Only losers take the lift. And besides, after skipping leg day for three years in a row I could probably use the exercise." +
        "\n2. Use the elevator. Only losers exhaust themselves for nothing and exercise isn't really my thing, after all." +
        "\n3. Ask the guard to carry me. He should earn his wage by doing something useful anyway.";

    private string d_f_victimStairs = "As I head for the stairs, the guard is giving me the eyeball something fierce. I guess a dead body in your building puts you a little on edge. Who'd have " +
        "thought?\n" +
        "\n1. Start climbing the stairs.";

    private string d_f_victimStairs2 = "After three floors I really started to hate myself for choosing the stairs. Once or twice I thought about taking the lift crossed my" +
        "mind, but being stubborn really gets in the way of rational thought. After dragging my exhausted, sweaty self upward I finally reach the seventh floor." +
        "\n\nTwo cops are having a quiet conversation by, presumably, Stephanie's apartment.\n" +
        "\n1. Approach the cops.";

    private string d_f_victimElevator = "Approaching the elevator I get an accute feeling of being watched or sized up. I try sneak a casual glance of the guard, but he's staring at " +
        "me intently. Our gazes lock, and a casual glance quickly turns into a staring match." +
        "\n\n\"Can I help you..?\", I ask him, but he remains silent. His gaze seems just a tiny bit angrier. Maybe he's just the Hollywood thug type... Not a man of many words.\n" +
        "\n1. Turn and enter the lift.";

    private string d_f_victimElevator2 = "The lift looks like it was built before annoying things like \"safety regulations\" " +
        "were a thing." +
        "\n\nI curse under my breath and plead to any and every god who'll listen not to let me die in this miserable little deathtrap of an elevator." +
        "\n\nAfter crossing my fingers and holding my breath the entire way up, I scramble out of the death cube. Two cops are having a quiet conversation by, presumably, Stephanie's apartment.\n" +
        "\n1. Approach the cops.";

    private string d_f_victimCarry = "I boldly approach the security guard. \"Hey, man! Time to earn your dime, you lazy bastard. I need to get to the seventh floor, pronto! No time for " +
        "dilly dallying!\"." +
        "\n\nI could see his face kinda twitch, him removing a stun rod from his belt, and I got a sudden urge for some exercise.\n" +
        "\n1. Well, it seems I might break the world record of stair climbing...";

    private string d_f_victimCarry2 = "After fleeing from the pissed off guard, who apparently wasn't in the habit of resident transportation, I crumble on to the seventh floor. " +
        "Two cops are having a quiet conversation by, presumably, Stephanie's apartment.\n" +
        "\n1. Approach the cops.";

    private string d_f_victimMeetCops = "Even though I oocasionally work as a consultant for the force, some cops have given me trouble before while working a case.\n" +
        "\n1. I'll approach the cops and flash them the \"detective's badge\" I made out of cardboard and some crayons. Should fool them, right?." +
        "\n2. I'll tell them who I am and why I'm here. There are some perfectly reasonable cops out there, after all.";

    private string d_f_victimFakeBadge = "The door is being watched by a balding plump officer, an actual incarnation of the archetypical Donut Cop, and his sharp featured youger sidekick. " +
        "\"Evening, boys.\", I say, \"Special detective Dick Richardson, here to inspect the crime scene\", and with a quick flash of my artfully constructed badge I " +
        "swagger confidently towards the door." +
        "\n\nMr. Plump McCopperson growls and steps toward me \"You gotta be kiddin' me. Look, pal, I sugges' ya start walkin' real quicklike or me and my pardna gon' find you a cold cell " +
        "floor ta sleep on tonight. Try dat shit again and you'll regret it.\"\n" +
        "\n1. Err... yeah, umm. Let's try that again. I'm a P.I. working on the case. I've done some consulting for the department before.";

    private string d_f_victimFakeBadge2 = "The older officer spits and grunts \"Yeah well you shoulda just said so. Lemme check ya papers, and no I don't mean that sorry excuse of a badge.\"" +
        "\n\nI hand him my P.I. licence and ID. He seems to really scrutinize them until he finally hands them back over." +
        "\n\n\"Yeah errthang seems ta be in order.. *grubmle grumble* Why'd ya gotta pull that shit with the badge anyway? Someone not as nice might have just taken you down to the station. " +
        "Not us, though, not Flanders and Kuzco here\"\n" +
        "\n1. Ask them for information about the case.";

    private string d_f_victimFriendly = "Evening. I'm the P.I. working on the case. Reyes might have notified you that I'm coming." +
        "\n\nThe younger officer answers \"Yeah we got word someone was coming to have a poke around the scene. I remember seeing you around cases like these occasionally. I'm Kuzco, by the way." +
        "\n\n\"Nice to meet you, Kuzco.\" I expectantly turn to look at the senior officer." +
        "\n\n\"Flanders.\"" +
        "\n\nYou nod.\n" +
        "\n1. Ask them for information about the case.";

    private string d_f_victimPoliceQuestions = "\"I've got a couple of questions about the case.\"\n" +
        "\n1. Can you tell me more about the victim?" +
        "\n2. What did forensics find out about the crime scene?" +
        "\n3. Do you have an estimate for the time of death?";

    private string d_f_victimPoliceQuestionsVictim = "Flanders replies \"Not much.. Name's Stephanie Corbin, stripper or dancer or hooker or whatever over at the Marilyn. 31, lives alone in " +
        "this shithole. Not much else to say. Did herself in, poor bastard.\"\n" +
        "\n1. What did forensics find out about the crime scene?" +
        "\n2. Do you have an estimate for the time of death?";

    private string d_f_victimPoliceQuestionsWhen = "They both shrug slightly. Kuzco sighs and says \"We've got a rough estimate, but nothing concrete yet. The coroner would know when he's done with the body. " +
        "Our best estimate is some time last night.\"\n" +
        "\n1. Can you tell me more about the victim?" +
        "\n2. What did forensics find out about the crime scene?";

    private string d_f_victimPoliceQuestionsScene = "Flanders fills you in about the details gathered from the scene, which isn't very much. Stephanie was found in the bathroom next " +
        "to a razor and her arms cut up. The place is a mess 'but what else could you expect from a junkie'.\n" +
        "\n1. Has the place been touched? Are things where they were?" +
        "\n2. May I have a look around?" +
        "\n3. Why are you still here if it's been ruled as a suicide?";

    private string d_f_victimPoliceQuestionsScene2 = "\"T'was a suicide. Ain't no reason to tiptoe aroun' a dead person's home when there be nothing to disturb. Nothin's been taken or nothin', " +
        "but it's not like we were extra careful in there.\"\n" +
        "\n1. May I have a look around?" +
        "\n2. Why are you still here if it's been ruled as a suicide?";

    private string d_f_victimPoliceQuestionsWhyHere = "Flanders sighs \"Fuckin' paperwork and regulations. Gettin' in da way of real police work, ya know?\"\n" +
        "\n1. Has the place been touched? Are things where they were?" +
            "\n2. May I have a look around?";

    private string d_f_victimApartment = "\"Knock yourself out, man. Even after they removed the body it stinks like all hell. Just don't take nothin'. That's stealin', ya know.\"" +
        "\n\nI step into the dingy appartment. The smell hits me before I've even got time to register the general gloom of the place.\n" +
        "\n1. Step into the bathroom. That's where the cops found Stephanie's body." +
        "\n2. Search the bedroom. You can tell a lot about a person by their bedroom." +
        "\n3. Check out the living room.";

    private string d_f_victimApartmentBathroom = "I step into the bathroom and fumble for the light." +
        "\n\n*FLICK*" +
        "\n\nBlood. So much blood. It takes me a while to notice I've stepped into the thickened puddle. Think water with cornstarch. As I turn to leave I notice a collection of " +
        "medicine bottles on the shelf.\n" +
        "\n1. Leave the bathroom.";

    private string d_f_victimApartmentBathroomApartment = "Continue your investigation.\n" +
        "\n1. Step back into bathroom. Maybe I missed something." +
        "\n2. Search the bedroom. You can tell a lot about a person by their bedroom." +
        "\n3. Check out the living room.";

    private string d_f_victimApartmentBathroom2 = "The lights are as I left them. I take a closer look at the medicine cabinet." +
        "\n\nIt seems she had quite an arsenal of pills, but nothing too out of the ordinary. A little complaining to morally bankrupt doctor would get you this kinda stuff.\n" +
        "\n1. Leave the bathroom.";

    private string d_f_victimApartmentBedroom = "The bedroom houses a dirty mattress without sheeting and a small nightstand. A framed photograph sits on the nightstand.\n" +
        "\n1. Inspect the photo." +
        "\n2. Search the drawer of the nightstand.";

    private string d_f_victimApartmentBedroomPhoto = "The photo shows a  young woman and a girl roughly three years old. There's a description on the back:" +
        "\n\nMommy and Carla, 1989\n" +
        "\n1. Search the drawer of the nightstand.";

    private string d_f_victimApartmentBedroomPhoto2 = "I found a pack of cigs, a lighter, and a thin stack of letters, almsost all of them having a short description of what Carla's " +
        "been up to and crayon picture obviously drawn by a kid.\n" +
        "\n1. Cross reference the stuff I found.";

    private string d_f_victimApartmentBedroomDrawer = "I found a pack of cigs, a lighter, and a thin stack of letters, almsost all of them having a short description of what Carla's " +
        "been up to and crayon picture obviously drawn by a kid.\n" +
        "\n1. Inspect the photo.";

    private string d_f_victimApartmentBedroomDrawer2 = "The photo shows a  young woman and a girl roughly three years old. There's a description on the back:" +
        "\n\nMommy and Carla, 1989\n" +
        "\n1. Cross reference the stuff I found.";

    private string d_f_victimApartmentBedroomConclusion = "After reading a few of the letters  and checking the photo I start to get the picture. Carla is Stephanie's daughter, " +
        "Jack's her brother. Apparently Stephanie sent Carla away to live with her brother. Maybe she didn't want her kid to grow up like... this. No wonder, the place is " +
        "hardly a good environment to bring up a child." +
        "\n\nMaybe her brother knows something... I keep one of the letters with Jack Corbin's address on it. I'd better check the living room before I leave.\n" +
        "\n1. Go over to the living room";

    private string d_f_victimApartmentBedroomLiving = "The living room looks just like the rest of the apartment. Small, dim and dirty. The room reeks of cigarettes and the overflowing " +
        "ashtray merely confirms that." +
        "\n\nThere's a matchbook with \"The Marilyn\" written on it on the coffee table. Maybe someone from the club could shine some light on this." +
        "\n\nThe window suddenly slamming shut startles me. I turn towards it.\n" +
        "\n1. Walk over to the window";

    private string d_f_victimApartmentLivingroom = "The living room looks just like the rest of the apartment. Small, dim and dirty. The room reeks of cigarettes and the overflowing " +
        "ashtray merely confirms that." +
        "\n\nThere's a matchbook with \"The Marilyn\" written on it on the coffee table. Maybe someone from the club could shine some light on this. Before I leave I should probably check " +
        "the bedroom for anything useful.\n" +
        "\n1. Search the bedroom.";

    private string d_f_victimApartmentLivingroomBed = "The bedroom houses a dirty mattress without sheeting and a small nightstand. A framed photograph sits on the nightstand.\n" +
        "\n1. Inspect the photo." +
        "\n2. Search the drawer of the nightstand.";

    private string d_f_victimApartmentLivingroomBedPhoto = "The photo shows a  young woman and a girl roughly three years old. There's a description on the back:" +
        "\n\nMommy and Carla, 1989\n" +
        "\n1. Search the drawer of the nightstand.";

    private string d_f_victimApartmentLivingroomBedPhoto2 = "I found a pack of cigs, a lighter, and a thin stack of letters, almsost all of them having a short description of what Carla's " +
        "been up to and crayon picture obviously drawn by a kid.\n" +
        "\n1. Cross reference the stuff I found.";

    private string d_f_victimApartmentLivingroomBedDrawer = "I found a pack of cigs, a lighter, and a thin stack of letters, almsost all of them having a short description of what Carla's " +
        "been up to and crayon picture obviously drawn by a kid.\n" +
        "\n1. Inspect the photo.";

    private string d_f_victimApartmentLivingroomBedDrawer2 = "The photo shows a  young woman and a girl roughly three years old. There's a description on the back:" +
        "\n\nMommy and Carla, 1989\n" +
        "\n1. Cross reference the stuff I found.";

    private string d_f_victimApartmentLivingroomBedConclusion = "After reading a few of the letters  and checking the photo I start to get the picture. Carla is Stephanie's daughter, " +
        "Jack's her brother. Apparently Stephanie sent Carla away to live with her brother. Maybe she didn't want her kid to grow up like... this. No wonder, the place is " +
        "hardly a good environment to bring up a child." +
        "\n\nMaybe her brother knows something... I keep one of the letters with Jack Corbin's address on it." +
        "\n\nThe bedroom window suddenly slams shut startling me.\n" +
        "\n1. Walk over to the window";

    private string d_f_victimApartmentWindow = "The window was open... I wonder. Maybe the police open it to ventilate this sorry excuse of an apartment. Or maybe someone " +
        "used it to make a stealthy escape out of the building? One could evade the CCTV on the streets by using the abundance alleyways of the city." +
        "\n\nI was tipped off that this wasn't a simple suicide and someone has to know something.\n" +
        "\n1. Track down the brother, Jack. Maybe he's got some up-to-date information that could prove useful." +
        "\n2. Go to Stephanie's place of work, the \"gentlemen's\" club known as The Marilyn.";


    // Tuomas' dialogue
    private string d_c_officeStreet = "\"Dammit! It's literally pouring out here. Maybe I didn't think this all the way through... " +
        "\n\nWell, maybe a good old walk in the rain will help get my thoughts in order. \"\n" +
        "\n1. Head to the police station. Maybe the former colleagues there will know something." +
        "\n2. Go to the liquor store. It's only two blocks away and a little chestwarmer certainly wouldn't hurt.";

    private string d_c_liquorStoreEnter = "I step inside Larry's Liquor Barn, look around and nod to Larry the shopkeeper. Looks like I'm the only customer..." +
        "\n1. Walk to the shelves";

    private string d_c_liquorStore = "The shelves clearly need stocking. I'm browsing through the selection...\n" +
        "\n1. Buy a bottle of Jim Beam for $30" +
        "\n2. Buy a bottle of Balvenie 30yo for $80" +
        "\n3. Buy a bottle of the cheapest vodka for $10" +
        "\n4. Leave without buying anything";

    private string d_c_streetAfterStore = "\"OK, I'm all set. I really should get cracking on this case. The precinct would probably be a good place to start.\"\n" +
        "\n1. Walk to the police station";

    private string d_c_policeStationLobby = "I walked into the lobby of the 51st precinct HQ from the rain looking like a soaked labrador. Might as well have left the umbrella at home... " +
        "\"There's hardly anybody in here. Certainly no one of the old pals from my squad. I wonder if Lieutenant Reyes is still working here. " +
        "I definitely left some bad blood behind me when I was suspended from the force, but maybe I'll be able to reason with him...\n\n" +
        "Oh, great. Now the clerk at the reception is staring at me. I should probably go talk to him.\"\n" +
        "\n1. Walk to the reception and talk to the clerk";

    private string d_c_policeStationClerkStart = "\"Good afternoon sir. What can I do for you?\"\n" +
        "\n1. Ask if ltn. Reyes from homicide is available" +
        "\n2. Ask if there is anybody available who knows about the case";

    private string d_c_policeStationClerkReyes = "\"Unfortunately mr. Reyes is rather busy right now. The precinct has been quite understaffed after the cutbacks they made last year. " +
        "He has a lot on his desk right now so he doesn't take appointments at the moment.\"\n" +
        "\n1. Tell the clerk that you understand the situation. You'll return some other time." +
        "\n2. Firmly tell the clerk that you don't have time to play games. You absolutely need an appointment" +
        "\n3. Ask if there are any other investigators you could talk to about the suicide case of ms. Stephanie Corbin";

    private string d_c_policeStationWalkOut = "\"Okay, this wasn't as easy as I thought... Well, I guess there's no use hanging around anymore. I'll just grab my umbrella and get the hell out of here.\n\n" +
        "But wait a second! That office door down the corridor seems to be ajar. Someone's talking in there but I can't make sense of it from this far...\"\n" +
        "\n1. Step closer towards the office door to eavesdrop";

    private string d_c_policeStationEavesdrop = "\"Well goddamn! That's ltn. Reyes in there. He's on the phone. Doesn't seem that busy to me..." +
        "I think he's speaking with the precinct commissioner about the Corbin case. It might not be a suicide? But who the hell is Danny Carver...\n\n" +
        "Well, at least I have a name now. Something to start with.\"\n" +
        "\n1. Silently sneak out from the corridor and exit the station";

    private string d_c_policeStationClerkReyesRaiseVoice = "\"All right, I understand. Let me see what I can do...\n\n" +
        "The precinct commisioner seems to have a window in his schedule right now. Maybe you would like to talk to him?\"\n" +
        "\n1. Follow the clerk into commissioner's office" +
        "\n2. Tell him you'd rather talk to ltn. Reyes. You'll return some other time.";

    private string d_c_policeStationClerkReyesOthers = "\"Well allright, let me see if someone of the agents is available. Please wait here, I won't take long.\"\n" +
        "\n1. Agree to wait in the lobby";

    private string d_c_policeStationClerkReyesOthersLeavesDesk = "\"Look at that. The idiot left me alone at his desk. Is that a copy of the Corbin case file on the floor? " +
        "He must have dropped it when he left... And the name of one of the suspects is written right on the first page. This is almost too easy. But who the hell is Danny Carver? Well, at least I have a name now.\n\n" +
        "Oh shit, he's coming back! I better scram!\"\n" +
        "\n1. Quickly leave the lobby and exit the station to the street";

    private string d_c_policeStationCommissionerStart = "\"Well, look at this guys office! This is more like a living room. Pinball machine, pool table, everything." +
        "They're out of their minds letting him spend our tax money that way in this economy...\"\n" +
        "\n1. Greet the commissioner";

    private string d_c_policeStationCommissionerGreet = "\"He looks at me with a frown on his face. \"Mr. Steele. I wish I could say it's a delight to see you again. You didn't think I had" +
        "forgotten about your little gimmicks back then, did you?\n\nWhat the hell do you want? I'm sure you can see that I have some actual policework to do here.\"\n" +
        "\n1. Ask if he's familiar with the Corbin suicide case" +
        "\n2. Storm out of the office. That man is unbelievable!";

    private string d_c_policeStationCommissionerCorbin = "\"Am I familiar with it? What do you think, Steele, does the police chief of an entire precinct know what his guys are working on?\n\n" +
        "What about the case? You're not telling me that you're looking into it as well, are you? I don't need any P.I's going solo on my cases! Just forget about it.\n" +
        "\n1. Keep pushing the commissioner about the case" +
        "\n2. Tell him to enjoy the rest of his life and leave the office";

    private string d_c_policeStationCommissionerPush = "\"Jesus Christ, Steele. Do you really want to keep testing my patience? I don't need your help with this!.\n\n" +
        "But if it gets you out of my office, were looking into a mr. Danny Carver. I can't tell you more than that if I wanted to.\n\n" +
        "Now get the hell out of my office!\"\n" +
        "\n1. Exit the station to the street";

    private string d_c_policeStationClerkAnybody = "\"Oh, so you're investigating ms Corbin's case? Are you with the PD?\"\n" +
        "\n1. Tell the clerk that you are a P.I. operating independently on the case" +
        "\n2. Lie to the clerk and tell him that you are agent Steele from 30th precinct vice";

    private string d_c_policeStationClerkAnybodyLie = "\"Okay, mr. Steele, I just need to see your badge before I give you the staff security clearance.\n\n" +
        "You know, it's just protocol.\"\n" +
        "\n1. Admit that you were lying and agree to exit. No need to call security!";

    private string d_c_policeStationExitStreet = "I'm standing on the street contemplating my next move. At least the rain has stopped.\n\n" +
        "\"Would be nice to sit down somewhere and get this damn wet jacket off my shoulders. I could really pour down a beer or two." +
        "On the other hand I really don't have any time to spare. Maybe I'll find out something at Ms. Corbin's place.\"\n" +
        "\n1. Head to the bar. All in good time..." +
        "\n2. Take a cab to ms. Corbin's apartment.";

    private string d_c_bar = "I saunter around the block towards the Fainting Goat, already imagining the well deserved cold pint of Blue Ribbon in my hand. Only to get there and find out " +
        "that the damn bar has been closed early.\n\n\"Son of a bitch! No beer for me I guess...\"\n" +
        "\n1. Might as well take the cab to ms. Corbin's apartment";

    private string d_c_CorbinsOutside = "I arrive to the front door of the dormitory where Stephanie's flat is located. The building is a depressing sight, likely built decades ago and in a real bad shape. " +
        "On top of all it started to rain again. I should go inside.\n" +
        "\n1. Open the front door and enter the dormitory";

    private string d_c_CorbinsHallway = "\"I can barely see anything in this damn hallway! I guess the bulbs need replacing.\"" +
        "\n\nI start walking forward scanning the names on the doors with my eyes.\n\n\"There! Corbin. That has to be her room.\"\n" +
        "\n1. Enter ms. Corbin's apartment. The door seems to be open." +
            "\n2. Knock on her nextdoor neighbour's door. The sign on the door says \"GREY\"";

    private string d_c_CorbinsApartmentEnter = "I take a moment to look around the tiny apartment.\n\n\"She must not have been a very organized person... " +
        "This place is a mess! Even worse than mine. And of course the lights don't work " +
        "here either.\"\n\nI grab my flashlight.\n" +
        "\n1. Go take a look on the kitchen" +
        "\n2. Walk to her nightstand and pick up a notebook" +
        "\n3. Go to her dresser to see if there's anything in the drawers" +
        "\n4. Go back to the hallway";

    private string d_c_CorbinsApartment = "\"Okay. Where should I look into next...\"\n" +
        "\n1. Go take a look on the kitchen" +
        "\n2. Walk to her nightstand and pick up a notebook" +
        "\n3. Go to her dresser to see if there's anything in the drawers" +
        "\n4. Go back to the hallway";

    private string d_c_CorbinsApartmentKitchen = "The kitchen is surprisingly tidy compared to the rest of the place. There's nothing there worth looking into though." +
        "The cops must have confiscated everything when they were here earlier...\n" +
        "\n1. Continue looking around in the apartment";

    private string d_c_CorbinsApartmentNightstand = "\"What's this? Looks like her diary... And there's an entry from just 2 weeks ago. That's 2 nights before her murder. This might lead to something!\"\n" +
        "\n1. Read the diary entry";

    private string d_c_CorbinsApartmentDiary = "\"Tuesday, September 14th: Some guy followed me today on my way to Wendy's place. I noticed him right after I left home, even though " +
        "he tried to hide from me and kept a distance. That completely freaked me out, considering all the recent occurrences. I think they're finally after me... Wendy was waiting for me" +
        "outside and saw him too.\"\n" +
        "\n1. Keep reading";

    private string d_c_CorbinsApartmentDiary2 = "\"She said that she recognised him from before. Apparently his name is Danny Carson. Or might have been Carver, I'm not sure. He's a " +
        "former regular of Wendy's. From what I heard from her, not a nice guy either... She told me he works at the docks as a stevedore. I get chills just thinking about him! " +
        "I wish all of this was over and I could go back to normal. I never should have gotten into this whole thing.\"\n" +
        "\n1. Put the diary down and leave the apartment. Already got what I was looking for.";

    private string d_c_CorbinsApartmentDresser = "I walk to the dresser and pull the drawers open." +
        "\n\n\"Eww... Only dirty underwear here! Not the classiest of ladies, for sure.\"\n" +
        "\n1. Continue looking around in the apartment";

    private string d_c_CorbinsGreyStart = "I firmly but politely knock on the door. Nothing happens for maybe 30 seconds. Then, suddenly I hear quiet footsteps that stop right behind the door." +
        "I realize someone's staring right at me through the peephole so I try to look as formal and matter-of-fact as I can. The door opens and an old lady in her pyjamas stands in the doorway." +
        "\"Good evening young man. What kind of an hour is this to knock on people's doors? Can I help you?\"\n" +
        "\n1. Tell that you're investigating ms. Corbin's case and ask if she knows something of a mr. Danny Carver" +
        "\n2. Introduce yourself and kindly ask if you could take a little of her time to speak about her neighbour" +
        "\n3. Apologize, wish the old lady good night, close the door and enter ms. Corbin's apartment";

    private string d_c_CorbinsGreyStraightToCarver = "Mrs. Grey looks clearly frustrated.\n\n\"What kind of a ruse are you pulling on an elderly person? " +
        "I don't even know you, mister, much less anyone called Carver! If that's all, I'd like to go back to my knitting now. Good Night.\"\n\nShe closes the " +
        "door leaving me standing in the hallway.\n" +
        "\n1. Enter ms. Corbin's apartment";

    private string d_c_CorbinsGreyGentlyToSubject = "Mrs. Grey seems pleased by my politeness." +
        "\n\n\"Well allright. Come in, mister Steele.\"" +
        "\n\nI tell her that I'm looking into ms. Corbin's murder." +
        "\n\n\"Well, what would you like to know mr. Steele?\" " +
        "\nshe asks curiously\n" +
        "\n1. Ask if she knew anybody named Danny Carver" +
        "\n2. Ask if she knows anything about the people her neighbour was hanging with";

    private string d_c_CorbinsGreyHintBar = "She looks out of the window as if her mind is drifting somewhere else.\n\n\"I can't say I really know anybody anymore. I've been so lonely " +
        "and isolated from everything ever since my husband passed away. But I think Stephanie mentioned something about her male acquaintences spending a lot of time in the Fat Angelo's. " +
        "That's a pub not far from here.\"\n" +
        "\n1. Thank mrs. Grey and exit her apartment to the street";

    private string d_c_CorbinsGreyExitStreet = "I close her door behind me and walk through the hallway and out of the dorm doors to the street. It's raining again." +
        "\n\n\"I should probably check that bar she was talking about.\"\n" +
        "\n1. Head to Fat Angelo's";

    private string d_c_CorbinsApartmentExitStreet = "It's raining again. I walk out of the dorm to the street, still furious about what I just read inside. \n\n\"That son of a bitch " +
        "Carver is clearly the murderer! But what the hell did Stephanie mean by 'them'? Who was after her? This is about something bigger than an average hate crime...\"\n" +
        "\n1. Call your cabbie back and head home. Better sleep on this.";

    private string d_c_fatAngelosEnter = "I open the door and walk into the sleeziest, most questionable boozer I have ever seen. And I've seen a lot of them. The place is full of cigarette smoke " +
        "and I can barely see across the room to the bar. Three bulky, tattooed guys are drinking their beers in the corner, staring at me suspiciously.\n" +
        "\n1. Go talk to the bartender" +
        "\n2. Take a seat at a table close to the hulks in the corner";

    private string d_c_FatAngelosBartenderStart = "The barmaid barely acknowledges me when I take a seat at the bar." +
        "\n\n\"What can I get you\"\n\nshe mutters, not even lifting her eyes from her " +
        "crossword puzzle.\n" +
        "\n1. Ask her if she knows a Danny Carver" +
        "\n2. Take a seat next to the men in the corner. She's clearly not interested.";

    private string d_c_FatAngelosBartenderNotInterested = "\"Danny who? Are you going to order something or not? Can't you see I'm in the middle of something here?\"\n\nShe dives right back into her crosswords.\n" +
        "\n1. Buy a beer and go take a seat. She definitely doesn't want to be bothered";

    private string d_c_FatAngelosTakeSeatStart = "I walk across the room and take a seat. The floor is all sticky from spilled drinks. Somebody has left an old issue of " +
        "Guns and Ammo on the table. The guys in the corner table are back to their conversation, not even noticing me anymore. They've lowered their voices but I can still hear what their saying.\n" +
        "\n1. Start sipping your beer and pretend to read the magazine";

    private string d_c_FatAngelosTakeSeatCarver = "\"You know the hooker that was found dead in her dorm a week back? Not a suicide.\"\n\none of the guys whispers.\n\n" +
        "Danny told me it was him who took care of the poor girl! Who would have thought...\"\n\nI can't believe what I'm hearing. I can almost hear my heart pounding through my chest.\n" +
        "\n1. Keep listening.";

    private string d_c_FatAngelosTakeSeatCarver2 = "I ran into him at the docks the other day while he was working. The man is a mess. He's already thinking of turning himself in! Of course I told him to sleep on it." +
        " What kind of an idiot turns himself in on a murder! That's just crazy.\"\n" +
        "\n1. Keep listening";

    private string d_c_FatAngelosTakeSeatCarver3 = "\"I don't know who gave him the job, but have you seen the brand new truck he was driving? They must have paid him pretty well...\"\n" +
        "\n1. Pull yourself together and sneak out of the bar";

    private string d_c_FatAngelosExitStreet = "I'm standing on the street and my head is spinning. \n\n\"That son of a bitch Carver did it after all!\"\n\nBut this is clearly about something bigger " +
        "than an everyday hate crime. Stephanie was involved in something. Maybe she knew too much and somebody wanted to get rid of her...\"\n" +
        "\n1. Go home. It's already late. Maybe it's better to sleep on this.";

    private string d_payUsMoney = "To be continued..." +
        "\n\n\n" +
        "This game was brought to you by Field & Cabins Productions." +
        "\n\nPre-order the sequel for 2015's best game for the low price of $9.99! Order now and be disappointed." +
        "\n\n\n\n\n\n";


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
        start.SetClue("Clue list:");

        // office01
        nn_office01.Add(office02); nn_office01.Add(office02);
        office01.AddNextNodes(nn_office01);
        office01.SetDialogue(d_office01);
        office01.SetHasAudioClip(true);
        office01.SetMyAudioClip(a_ring);
        office01.SetHasMusicClip(true);
        office01.SetMyMusicClip(m_rainSofter);

        // office02
        nn_office02.Add(office);
        office02.AddNextNodes(nn_office02);
        office02.SetDialogue(d_office02);
        office02.SetHasAudioClip(true);
        office02.SetMyAudioClip(a_pickup);
        office02.SetHasMusicClip(true);
        office02.SetMyMusicClip(m_rainSofter);

        // office
        nn_office.Add(f_officeWindow);
        nn_office.Add(c_officeStreet);
        office.AddNextNodes(nn_office);
        office.SetDialogue(d_office);
        office.SetHasMusicClip(true);
        office.SetMyMusicClip(m_rainSofter);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_officeWindow
        nn_f_officeWindow.Add(f_officeDigging);
        nn_f_officeWindow.Add(f_victimStreets);
        f_officeWindow.AddNextNodes(nn_f_officeWindow);
        f_officeWindow.SetDialogue(d_f_officeWindow);
        f_officeWindow.SetHasAudioClip(true);
        f_officeWindow.SetMyAudioClip(a_megaThunder);
        f_officeWindow.SetHasMusicClip(true);
        f_officeWindow.SetMyMusicClip(m_rainSofter);

        // f_officeDigging
        nn_f_officeDigging.Add(f_officeFiles);
        nn_f_officeDigging.Add(f_officePhone);
        f_officeDigging.AddNextNodes(nn_f_officeDigging);
        f_officeDigging.SetDialogue(d_f_officeDigging);
        f_officeDigging.SetHasMusicClip(true);
        f_officeDigging.SetMyMusicClip(m_rainSofter);

        // f_officeFiles
        nn_f_officeFiles.Add(f_victimStreets);
        f_officeFiles.AddNextNodes(nn_f_officeFiles);
        f_officeFiles.SetDialogue(d_f_officeFiles);
        f_officeFiles.SetHasAudioClip(true);
        f_officeFiles.SetMyAudioClip(a_rustleFlipPapers);
        f_officeFiles.SetHasMusicClip(true);
        f_officeFiles.SetMyMusicClip(m_rainSofter);

        // f_officePhone
        nn_f_officePhone.Add(f_victimStreets);
        f_officePhone.AddNextNodes(nn_f_officePhone);
        f_officePhone.SetDialogue(d_f_officePhone);
        f_officePhone.SetHasAudioClip(true);
        f_officePhone.SetMyAudioClip(a_tryDial);
        f_officePhone.SetHasMusicClip(true);
        f_officePhone.SetMyMusicClip(m_rainSofter);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_victimStreets
        nn_f_victimStreets.Add(f_victimOutsideBuilding);
        f_victimStreets.AddNextNodes(nn_f_victimStreets);
        f_victimStreets.SetDialogue(d_f_victimStreets);
        f_victimStreets.SetHasAudioClip(true);
        f_victimStreets.SetMyAudioClip(a_carPassing);
        f_victimStreets.SetHasMusicClip(true);
        f_victimStreets.SetMyMusicClip(m_rainHard);

        // f_victimOutsideBuildings
        nn_f_victimOutsideBuilding.Add(f_victimStairs);
        nn_f_victimOutsideBuilding.Add(f_victimElevator);
        nn_f_victimOutsideBuilding.Add(f_victimCarry);
        f_victimOutsideBuilding.AddNextNodes(nn_f_victimOutsideBuilding);
        f_victimOutsideBuilding.SetDialogue(d_f_victimOutsideBuilding);


        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_victimStairs
        nn_f_victimStairs.Add(f_victimStairs2);
        f_victimStairs.AddNextNodes(nn_f_victimStairs);
        f_victimStairs.SetDialogue(d_f_victimStairs);

        // f_victimStairs2
        nn_f_victimStairs2.Add(f_victimMeetCops);
        f_victimStairs2.AddNextNodes(nn_f_victimStairs2);
        f_victimStairs2.SetDialogue(d_f_victimStairs2);

        // f_victimElevator
        nn_f_victimElevator.Add(f_victimElevator2);
        f_victimElevator.AddNextNodes(nn_f_victimElevator);
        f_victimElevator.SetDialogue(d_f_victimElevator);
        f_victimElevator.SetHasMusicClip(true);
        f_victimElevator.SetMyMusicClip(m_storeMusic);

        // f_victimElevator2
        nn_f_victimElevator2.Add(f_victimMeetCops);
        f_victimElevator2.AddNextNodes(nn_f_victimElevator2);
        f_victimElevator2.SetDialogue(d_f_victimElevator2);

        // f_victimCarry
        nn_f_victimCarry.Add(f_victimCarry2);
        f_victimCarry.AddNextNodes(nn_f_victimCarry);
        f_victimCarry.SetDialogue(d_f_victimCarry);

        // f_victimCarry2
        nn_f_victimCarry2.Add(f_victimMeetCops);
        f_victimCarry2.AddNextNodes(nn_f_victimCarry2);
        f_victimCarry2.SetDialogue(d_f_victimCarry2);
        f_victimCarry2.SetHasAudioClip(true);
        f_victimCarry2.SetMyAudioClip(a_wilhelmScream);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_victimMeetCops
        nn_f_victimMeetCops.Add(f_victimFakeBadge);
        nn_f_victimMeetCops.Add(f_victimFriendly);
        f_victimMeetCops.AddNextNodes(nn_f_victimMeetCops);
        f_victimMeetCops.SetDialogue(d_f_victimMeetCops);

        // f_victimFakeBadge
        nn_f_victimFakeBadge.Add(f_victimFakeBadge2);
        f_victimFakeBadge.AddNextNodes(nn_f_victimFakeBadge);
        f_victimFakeBadge.SetDialogue(d_f_victimFakeBadge);

        // f_victimFakeBadge2
        nn_f_victimFakeBadge2.Add(f_victimPoliceQuestions);
        f_victimFakeBadge2.AddNextNodes(nn_f_victimFakeBadge2);
        f_victimFakeBadge2.SetDialogue(d_f_victimFakeBadge2);

        // f_victimFriendly
        nn_f_victimFriendly.Add(f_victimPoliceQuestions);
        f_victimFriendly.AddNextNodes(nn_f_victimFriendly);
        f_victimFriendly.SetDialogue(d_f_victimFriendly);

        // f_victimPoliceQuestions
        nn_f_victimPoliceQuestions.Add(f_victimPoliceQuestionsVictim);
        nn_f_victimPoliceQuestions.Add(f_victimPoliceQuestionsScene);
        nn_f_victimPoliceQuestions.Add(f_victimPoliceQuestionsWhen);
        f_victimPoliceQuestions.AddNextNodes(nn_f_victimPoliceQuestions);
        f_victimPoliceQuestions.SetDialogue(d_f_victimPoliceQuestions);

        // f_victimPoliceQuestionsVictim
        nn_f_victimPoliceQuestionsVictim.Add(f_victimPoliceQuestionsScene);
        nn_f_victimPoliceQuestionsVictim.Add(f_victimPoliceQuestionsWhen);
        f_victimPoliceQuestionsVictim.AddNextNodes(nn_f_victimPoliceQuestionsVictim);
        f_victimPoliceQuestionsVictim.SetDialogue(d_f_victimPoliceQuestionsVictim);

        // f_victimPoliceQuestionsWhen
        nn_f_victimPoliceQuestionsWhen.Add(f_victimPoliceQuestionsVictim);
        nn_f_victimPoliceQuestionsWhen.Add(f_victimPoliceQuestionsScene);
        f_victimPoliceQuestionsWhen.AddNextNodes(nn_f_victimPoliceQuestionsWhen);
        f_victimPoliceQuestionsWhen.SetDialogue(d_f_victimPoliceQuestionsWhen);

        // f_victimPoliceQuestionsScene
        nn_f_victimPoliceQuestionsScene.Add(f_victimPoliceQuestionsScene2);
        nn_f_victimPoliceQuestionsScene.Add(f_victimApartment);
        nn_f_victimPoliceQuestionsScene.Add(f_victimPoliceQuestionsWhyHere);
        f_victimPoliceQuestionsScene.AddNextNodes(nn_f_victimPoliceQuestionsScene);
        f_victimPoliceQuestionsScene.SetDialogue(d_f_victimPoliceQuestionsScene);

        // f_victimPoliceQuestionsScene2
        nn_f_victimPoliceQuestionsScene2.Add(f_victimApartment);
        nn_f_victimPoliceQuestionsScene2.Add(f_victimPoliceQuestionsWhyHere);
        f_victimPoliceQuestionsScene2.AddNextNodes(nn_f_victimPoliceQuestionsScene2);
        f_victimPoliceQuestionsScene2.SetDialogue(d_f_victimPoliceQuestionsScene2);

        // f_victimPoliceQuestionsWhyHere
        nn_f_victimPoliceQuestionsWhyHere.Add(f_victimPoliceQuestionsScene2);
        nn_f_victimPoliceQuestionsWhyHere.Add(f_victimApartment);
        f_victimPoliceQuestionsWhyHere.AddNextNodes(nn_f_victimPoliceQuestionsWhyHere);
        f_victimPoliceQuestionsWhyHere.SetDialogue(d_f_victimPoliceQuestionsWhyHere);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_victimApartment
        nn_f_victimApartment.Add(f_victimApartmentBathroom);
        nn_f_victimApartment.Add(f_victimApartmentBedroom);
        nn_f_victimApartment.Add(f_victimApartmentLivingroom);
        f_victimApartment.AddNextNodes(nn_f_victimApartment);
        f_victimApartment.SetDialogue(d_f_victimApartment);
        f_victimApartment.SetHasAudioClip(true);
        f_victimApartment.SetMyAudioClip(a_doorOpenClose);
        f_victimApartment.SetHasMusicClip(true);
        f_victimApartment.SetMyMusicClip(m_dramaticMusic2);


        // f_victimApartmentBathroom
        nn_f_victimApartmentBathroom.Add(f_victimApartmentBathroomApartment);
        f_victimApartmentBathroom.AddNextNodes(nn_f_victimApartmentBathroom);
        f_victimApartmentBathroom.SetDialogue(d_f_victimApartmentBathroom);
        f_victimApartmentBathroom.SetHasAudioClip(true);
        f_victimApartmentBathroom.SetMyAudioClip(a_delayedSwitch);
        f_victimApartmentBathroom.SetHasMusicClip(true);
        f_victimApartmentBathroom.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBathroom2
        nn_f_victimApartmentBathroom2.Add(f_victimApartmentBathroomApartment);
        f_victimApartmentBathroom2.AddNextNodes(nn_f_victimApartmentBathroom2);
        f_victimApartmentBathroom2.SetDialogue(d_f_victimApartmentBathroom2);
        f_victimApartmentBathroom2.SetHasAudioClip(true);
        f_victimApartmentBathroom2.SetMyAudioClip(a_pillBottle);
        f_victimApartmentBathroom2.SetHasMusicClip(true);
        f_victimApartmentBathroom2.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBathroomApartment
        nn_f_victimApartmentBathroomApartment.Add(f_victimApartmentBathroom2);
        nn_f_victimApartmentBathroomApartment.Add(f_victimApartmentBedroom);
        nn_f_victimApartmentBathroomApartment.Add(f_victimApartmentLivingroom);
        f_victimApartmentBathroomApartment.AddNextNodes(nn_f_victimApartmentBathroomApartment);
        f_victimApartmentBathroomApartment.SetDialogue(d_f_victimApartmentBathroomApartment);
        f_victimApartmentBathroomApartment.SetHasMusicClip(true);
        f_victimApartmentBathroomApartment.SetMyMusicClip(m_dramaticMusic2);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_victimApartmentBedroom
        nn_f_victimApartmentBedroom.Add(f_victimApartmentBedroomPhoto);
        nn_f_victimApartmentBedroom.Add(f_victimApartmentBedroomDrawer);
        f_victimApartmentBedroom.AddNextNodes(nn_f_victimApartmentBedroom);
        f_victimApartmentBedroom.SetDialogue(d_f_victimApartmentBedroom);
        f_victimApartmentBedroom.SetHasMusicClip(true);
        f_victimApartmentBedroom.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBedroomPhoto
        nn_f_victimApartmentBedroomPhoto.Add(f_victimApartmentBedroomPhoto2);
        f_victimApartmentBedroomPhoto.AddNextNodes(nn_f_victimApartmentBedroomPhoto);
        f_victimApartmentBedroomPhoto.SetDialogue(d_f_victimApartmentBedroomPhoto);
        f_victimApartmentBedroomPhoto.SetHasMusicClip(true);
        f_victimApartmentBedroomPhoto.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBedroomPhoto2
        nn_f_victimApartmentBedroomPhoto2.Add(f_victimApartmentBedroomConclusion);
        f_victimApartmentBedroomPhoto2.AddNextNodes(nn_f_victimApartmentBedroomPhoto2);
        f_victimApartmentBedroomPhoto2.SetDialogue(d_f_victimApartmentBedroomPhoto2);
        f_victimApartmentBedroomPhoto2.SetHasAudioClip(true);
        f_victimApartmentBedroomPhoto2.SetMyAudioClip(a_rustleFlipPapers);
        f_victimApartmentBedroomPhoto2.SetHasMusicClip(true);
        f_victimApartmentBedroomPhoto2.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBedroomDrawer
        nn_f_victimApartmentBedroomDrawer.Add(f_victimApartmentBedroomDrawer2);
        f_victimApartmentBedroomDrawer.AddNextNodes(nn_f_victimApartmentBedroomDrawer);
        f_victimApartmentBedroomDrawer.SetDialogue(d_f_victimApartmentBedroomDrawer);
        f_victimApartmentBedroomDrawer.SetHasAudioClip(true);
        f_victimApartmentBedroomDrawer.SetMyAudioClip(a_rustleFlipPapers);
        f_victimApartmentBedroomDrawer.SetHasMusicClip(true);
        f_victimApartmentBedroomDrawer.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBedroomDrawer2
        nn_f_victimApartmentBedroomDrawer2.Add(f_victimApartmentBedroomConclusion);
        f_victimApartmentBedroomDrawer2.AddNextNodes(nn_f_victimApartmentBedroomDrawer2);
        f_victimApartmentBedroomDrawer2.SetDialogue(d_f_victimApartmentBedroomDrawer2);
        f_victimApartmentBedroomDrawer2.SetHasMusicClip(true);
        f_victimApartmentBedroomDrawer2.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentBedroomConclusion
        nn_f_victimApartmentBedroomConclusion.Add(f_victimApartmentBedroomLiving);
        f_victimApartmentBedroomConclusion.AddNextNodes(nn_f_victimApartmentBedroomConclusion);
        f_victimApartmentBedroomConclusion.SetDialogue(d_f_victimApartmentBedroomConclusion);
        f_victimApartmentBedroomConclusion.SetHasMusicClip(true);
        f_victimApartmentBedroomConclusion.SetMyMusicClip(m_dramaticMusic2);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // f_victimApartmentBedroomLiving
        nn_f_victimApartmentBedroomLiving.Add(f_victimApartmentWindow);
        f_victimApartmentBedroomLiving.AddNextNodes(nn_f_victimApartmentBedroomLiving);
        f_victimApartmentBedroomLiving.SetDialogue(d_f_victimApartmentBedroomLiving);
        f_victimApartmentBedroomLiving.SetHasMusicClip(true);
        f_victimApartmentBedroomLiving.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroom
        nn_f_victimApartmentLivingroom.Add(f_victimApartmentLivingroomBed);
        f_victimApartmentLivingroom.AddNextNodes(nn_f_victimApartmentLivingroom);
        f_victimApartmentLivingroom.SetDialogue(d_f_victimApartmentLivingroom);
        f_victimApartmentLivingroom.SetHasMusicClip(true);
        f_victimApartmentLivingroom.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroomBed
        nn_f_victimApartmentLivingroomBed.Add(f_victimApartmentLivingroomBedPhoto);
        nn_f_victimApartmentLivingroomBed.Add(f_victimApartmentLivingroomBedDrawer);
        f_victimApartmentLivingroomBed.AddNextNodes(nn_f_victimApartmentLivingroomBed);
        f_victimApartmentLivingroomBed.SetDialogue(d_f_victimApartmentLivingroomBed);
        f_victimApartmentLivingroomBed.SetHasMusicClip(true);
        f_victimApartmentLivingroomBed.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroomBedPhoto
        nn_f_victimApartmentLivingroomBedPhoto.Add(f_victimApartmentLivingroomBedPhoto2);
        f_victimApartmentLivingroomBedPhoto.AddNextNodes(nn_f_victimApartmentLivingroomBedPhoto);
        f_victimApartmentLivingroomBedPhoto.SetDialogue(d_f_victimApartmentLivingroomBedPhoto);
        f_victimApartmentLivingroomBedPhoto.SetHasMusicClip(true);
        f_victimApartmentLivingroomBedPhoto.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroomBedPhoto2
        nn_f_victimApartmentLivingroomBedPhoto2.Add(f_victimApartmentLivingroomBedConclusion);
        f_victimApartmentLivingroomBedPhoto2.AddNextNodes(nn_f_victimApartmentLivingroomBedPhoto2);
        f_victimApartmentLivingroomBedPhoto2.SetDialogue(d_f_victimApartmentLivingroomBedPhoto2);
        f_victimApartmentLivingroomBedPhoto2.SetHasAudioClip(true);
        f_victimApartmentLivingroomBedPhoto2.SetMyAudioClip(a_rustleFlipPapers);
        f_victimApartmentLivingroomBedPhoto2.SetHasMusicClip(true);
        f_victimApartmentLivingroomBedPhoto2.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroomBedDrawer
        nn_f_victimApartmentLivingroomBedDrawer.Add(f_victimApartmentLivingroomBedDrawer2);
        f_victimApartmentLivingroomBedDrawer.AddNextNodes(nn_f_victimApartmentLivingroomBedDrawer);
        f_victimApartmentLivingroomBedDrawer.SetDialogue(d_f_victimApartmentLivingroomBedDrawer);
        f_victimApartmentLivingroomBedDrawer.SetHasAudioClip(true);
        f_victimApartmentLivingroomBedDrawer.SetMyAudioClip(a_rustleFlipPapers);
        f_victimApartmentLivingroomBedDrawer.SetHasMusicClip(true);
        f_victimApartmentLivingroomBedDrawer.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroomBedDrawer2
        nn_f_victimApartmentLivingroomBedDrawer2.Add(f_victimApartmentLivingroomBedConclusion);
        f_victimApartmentLivingroomBedDrawer2.AddNextNodes(nn_f_victimApartmentLivingroomBedDrawer2);
        f_victimApartmentLivingroomBedDrawer2.SetDialogue(d_f_victimApartmentLivingroomBedDrawer2);
        f_victimApartmentLivingroomBedDrawer2.SetHasMusicClip(true);
        f_victimApartmentLivingroomBedDrawer2.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentLivingroomBedConclusion
        nn_f_victimApartmentLivingroomBedConclusion.Add(f_victimApartmentWindow);
        f_victimApartmentLivingroomBedConclusion.AddNextNodes(nn_f_victimApartmentLivingroomBedConclusion);
        f_victimApartmentLivingroomBedConclusion.SetDialogue(d_f_victimApartmentLivingroomBedConclusion);
        f_victimApartmentLivingroomBedConclusion.SetHasMusicClip(true);
        f_victimApartmentLivingroomBedConclusion.SetMyMusicClip(m_dramaticMusic2);

        // f_victimApartmentWindow
        nn_f_victimApartmentWindow.Add(payUsMoney);
        nn_f_victimApartmentWindow.Add(payUsMoney);
        f_victimApartmentWindow.AddNextNodes(nn_f_victimApartmentWindow);
        f_victimApartmentWindow.SetDialogue(d_f_victimApartmentWindow);
        f_victimApartmentWindow.SetHasMusicClip(true);
        f_victimApartmentWindow.SetMyMusicClip(m_dramaticMusic2);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // c_officeStreet
        nn_c_officeStreet.Add(c_policeStationLobby);
        nn_c_officeStreet.Add(c_liquorStoreEnter);
        c_officeStreet.AddNextNodes(nn_c_officeStreet);
        c_officeStreet.SetDialogue(d_c_officeStreet);
        c_officeStreet.SetHasMusicClip(true);
        c_officeStreet.SetMyMusicClip(m_rainHard);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // c_liquorStoreEnter
        nn_c_liquorStoreEnter.Add(c_liquorStore);
        c_liquorStoreEnter.AddNextNodes(nn_c_liquorStoreEnter);
        c_liquorStoreEnter.SetDialogue(d_c_liquorStoreEnter);
        c_liquorStoreEnter.SetNextIsStore(true);
        c_liquorStoreEnter.SetHasMusicClip(true);
        c_liquorStoreEnter.SetMyMusicClip(m_storeMusic);

        // c_liquorStore
        nn_c_liquorStore.Add(c_liquorStoreReceipt);
        nn_c_liquorStore.Add(c_liquorStoreReceipt);
        nn_c_liquorStore.Add(c_liquorStoreReceipt);
        nn_c_liquorStore.Add(c_liquorStoreReceipt);
        c_liquorStore.AddNextNodes(nn_c_liquorStore);
        c_liquorStore.SetDialogue(d_c_liquorStore);
        c_liquorStore.SetHasMusicClip(true);
        c_liquorStore.SetMyMusicClip(m_storeMusic);
        c_liquorStore.SetStoreItems(button1, jimBeam);
        c_liquorStore.SetStoreItems(button2, balvenie);
        c_liquorStore.SetStoreItems(button3, vodka);
        c_liquorStore.SetStoreItems(button4, nothing);
        c_liquorStore.SetNextIsReceipt(true);

        // c_liquorStoreReceipt
        nn_c_liquorStoreReceipt.Add(c_streetAfterStore);
        c_liquorStoreReceipt.AddNextNodes(nn_c_liquorStoreReceipt);
        c_liquorStoreReceipt.SetDialogue(d_receipt);

        // c_streetAfterStore
        nn_c_streetAfterStore.Add(c_policeStationLobby);
        c_streetAfterStore.AddNextNodes(nn_c_streetAfterStore);
        c_streetAfterStore.SetDialogue(d_c_streetAfterStore);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        //c_policeStationLobby
        nn_c_policeStationLobby.Add(c_policeStationClerkStart);
        c_policeStationLobby.AddNextNodes(nn_c_policeStationLobby);
        c_policeStationLobby.SetDialogue(d_c_policeStationLobby);

        // c_policeStationClerkStart
        nn_c_policeStationClerkStart.Add(c_policeStationClerkReyes);
        nn_c_policeStationClerkStart.Add(c_policeStationClerkAnybody);
        c_policeStationClerkStart.AddNextNodes(nn_c_policeStationClerkStart);
        c_policeStationClerkStart.SetDialogue(d_c_policeStationClerkStart);

        // c_policeStationClerkReyes
        nn_c_policeStationClerkReyes.Add(c_policeStationWalkOut);
        nn_c_policeStationClerkReyes.Add(c_policeStationClerkReyesRaiseVoice);
        nn_c_policeStationClerkReyes.Add(c_policeStationClerkReyesOthers);
        c_policeStationClerkReyes.AddNextNodes(nn_c_policeStationClerkReyes);
        c_policeStationClerkReyes.SetDialogue(d_c_policeStationClerkReyes);

        // c_policeStationClerkReyesRaiseVoice
        nn_c_policeStationClerkReyesRaiseVoice.Add(c_policeStationCommissionerStart);
        nn_c_policeStationClerkReyesRaiseVoice.Add(c_policeStationWalkOut);
        c_policeStationClerkReyesRaiseVoice.AddNextNodes(nn_c_policeStationClerkReyesRaiseVoice);
        c_policeStationClerkReyesRaiseVoice.SetDialogue(d_c_policeStationClerkReyesRaiseVoice);


        // c_policeStationClerkReyesOthers
        nn_c_policeStationClerkReyesOthers.Add(c_policeStationClerkReyesOthersLeavesDesk);
        c_policeStationClerkReyesOthers.AddNextNodes(nn_c_policeStationClerkReyesOthers);
        c_policeStationClerkReyesOthers.SetDialogue(d_c_policeStationClerkReyesOthers);

        // c_policeStationClerkReyesOthersLeavesDesk
        nn_c_policeStationClerkReyesOthersLeavesDesk.Add(c_policeStationExitStreet);
        c_policeStationClerkReyesOthersLeavesDesk.AddNextNodes(nn_c_policeStationClerkReyesOthersLeavesDesk);
        c_policeStationClerkReyesOthersLeavesDesk.SetDialogue(d_c_policeStationClerkReyesOthersLeavesDesk);
        c_policeStationClerkReyesOthersLeavesDesk.SetHasMusicClip(true);
        c_policeStationClerkReyesOthersLeavesDesk.SetMyMusicClip(m_dramaticMusic);

        // c_policeStationClerkAnybody
        nn_c_policeStationClerkAnybody.Add(c_policeStationClerkReyesOthers);
        nn_c_policeStationClerkAnybody.Add(c_policeStationClerkAnybodyLie);
        c_policeStationClerkAnybody.SetDialogue(d_c_policeStationClerkAnybody);
        c_policeStationClerkAnybody.AddNextNodes(nn_c_policeStationClerkAnybody);

        // c_policeStationClerkAnybodyLie
        nn_c_policeStationClerkAnybodyLie.Add(c_policeStationWalkOut);
        c_policeStationClerkAnybodyLie.AddNextNodes(nn_c_policeStationClerkAnybodyLie);
        c_policeStationClerkAnybodyLie.SetDialogue(d_c_policeStationClerkAnybodyLie);
        c_policeStationClerkAnybodyLie.SetHasAudioClip(true);
        c_policeStationClerkAnybodyLie.SetMyAudioClip(a_dramaticHit);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // c_policeStationCommissionerStart
        nn_c_policeStationCommissionerStart.Add(c_policeStationCommissionerGreet);
        c_policeStationCommissionerStart.AddNextNodes(nn_c_policeStationCommissionerStart);
        c_policeStationCommissionerStart.SetDialogue(d_c_policeStationCommissionerStart);

        // c_policeStationCommissionerGreet
        nn_c_policeStationCommissionerGreet.Add(c_policeStationCommissionerCorbin);
        nn_c_policeStationCommissionerGreet.Add(c_policeStationWalkOut);
        c_policeStationCommissionerGreet.AddNextNodes(nn_c_policeStationCommissionerGreet);
        c_policeStationCommissionerGreet.SetDialogue(d_c_policeStationCommissionerGreet);

        // c_policeStationCommissionerCorbin
        nn_c_policeStationCommissionerCorbin.Add(c_policeStationCommissionerPush);
        nn_c_policeStationCommissionerCorbin.Add(c_policeStationWalkOut);
        c_policeStationCommissionerCorbin.AddNextNodes(nn_c_policeStationCommissionerCorbin);
        c_policeStationCommissionerCorbin.SetDialogue(d_c_policeStationCommissionerCorbin);

        // c_policeStationCommissionerPush
        nn_c_policeStationCommissionerPush.Add(c_policeStationExitStreet);
        c_policeStationCommissionerPush.AddNextNodes(nn_c_policeStationCommissionerPush);
        c_policeStationCommissionerPush.SetDialogue(d_c_policeStationCommissionerPush);
        c_policeStationCommissionerPush.SetHasClue(true);
        c_policeStationCommissionerPush.SetClue("- The cops are looking into a Danny Carver");

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // c_policeStationWalkOut
        nn_c_policeStationWalkOut.Add(c_policeStationEavesdrop);
        c_policeStationWalkOut.AddNextNodes(nn_c_policeStationWalkOut);
        c_policeStationWalkOut.SetDialogue(d_c_policeStationWalkOut);

        // c_policeStationEavesdrop
        nn_c_policeStationEavesdrop.Add(c_policeStationExitStreet);
        c_policeStationEavesdrop.AddNextNodes(nn_c_policeStationEavesdrop);
        c_policeStationEavesdrop.SetDialogue(d_c_policeStationEavesdrop);
        c_policeStationEavesdrop.SetHasMusicClip(true);
        c_policeStationEavesdrop.SetMyMusicClip(m_dramaticMusic);
        c_policeStationEavesdrop.SetHasClue(true);
        c_policeStationEavesdrop.SetClue("- The cops are looking into a Danny Carver");

        // c_policeStationExitStreet
        nn_c_policeStationExitStreet.Add(c_bar);
        nn_c_policeStationExitStreet.Add(c_CorbinsOutside);
        c_policeStationExitStreet.AddNextNodes(nn_c_policeStationExitStreet);
        c_policeStationExitStreet.SetDialogue(d_c_policeStationExitStreet);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // c_bar
        nn_c_bar.Add(c_CorbinsOutside);
        c_bar.AddNextNodes(nn_c_bar);
        c_bar.SetDialogue(d_c_bar);

        // c_CorbinsOutside
        nn_c_CorbinsOutside.Add(c_CorbinsHallway);
        c_CorbinsOutside.AddNextNodes(nn_c_CorbinsOutside);
        c_CorbinsOutside.SetDialogue(d_c_CorbinsOutside);

        // c_CorbinsHallway
        nn_c_CorbinsHallway.Add(c_CorbinsApartment);
        nn_c_CorbinsHallway.Add(c_CorbinsGreyStart);
        c_CorbinsHallway.AddNextNodes(nn_c_CorbinsHallway);
        c_CorbinsHallway.SetDialogue(d_c_CorbinsHallway);

        // c_CorbinsApartmentEnter
        nn_c_CorbinsApartmentEnter.Add(c_CorbinsApartmentKitchen);
        nn_c_CorbinsApartmentEnter.Add(c_CorbinsApartmentNightstand);
        nn_c_CorbinsApartmentEnter.Add(c_CorbinsApartmentDresser);
        nn_c_CorbinsApartmentEnter.Add(c_CorbinsHallway);
        c_CorbinsApartmentEnter.AddNextNodes(nn_c_CorbinsApartmentEnter);
        c_CorbinsApartmentEnter.SetDialogue(d_c_CorbinsApartmentEnter);
        c_CorbinsApartmentEnter.SetHasAudioClip(true);
        c_CorbinsApartmentEnter.SetMyAudioClip(a_doorOpenClose);

        // c_CorbinsApartment
        nn_c_CorbinsApartment.Add(c_CorbinsApartmentKitchen);
        nn_c_CorbinsApartment.Add(c_CorbinsApartmentNightstand);
        nn_c_CorbinsApartment.Add(c_CorbinsApartmentDresser);
        nn_c_CorbinsApartment.Add(c_CorbinsHallway);
        c_CorbinsApartment.AddNextNodes(nn_c_CorbinsApartment);
        c_CorbinsApartment.SetDialogue(d_c_CorbinsApartment);

        // c_CorbinsApartmentKitchen
        nn_c_CorbinsApartmentKitchen.Add(c_CorbinsApartment);
        c_CorbinsApartmentKitchen.AddNextNodes(nn_c_CorbinsApartmentKitchen);
        c_CorbinsApartmentKitchen.SetDialogue(d_c_CorbinsApartmentKitchen);

        // c_CorbinsApartmentNightstand
        nn_c_CorbinsApartmentNightstand.Add(c_CorbinsApartmentDiary);
        c_CorbinsApartmentNightstand.AddNextNodes(nn_c_CorbinsApartmentNightstand);
        c_CorbinsApartmentNightstand.SetDialogue(d_c_CorbinsApartmentNightstand);

        // c_CorbinsApartmentDresser
        nn_c_CorbinsApartmentDresser.Add(c_CorbinsApartment);
        c_CorbinsApartmentDresser.AddNextNodes(nn_c_CorbinsApartmentDresser);
        c_CorbinsApartmentDresser.SetDialogue(d_c_CorbinsApartmentDresser);

        // c_CorbinsApartmentDiary
        nn_c_CorbinsApartmentDiary.Add(c_CorbinsApartmentDiary2);
        c_CorbinsApartmentDiary.AddNextNodes(nn_c_CorbinsApartmentDiary);
        c_CorbinsApartmentDiary.SetDialogue(d_c_CorbinsApartmentDiary);
        c_CorbinsApartmentDiary.SetHasMusicClip(true);
        c_CorbinsApartmentDiary.SetMyMusicClip(m_dramaticMusic2);

        // c_CorbinsApartmentDiary2
        nn_c_CorbinsApartmentDiary2.Add(c_CorbinsApartmentExitStreet);
        c_CorbinsApartmentDiary2.AddNextNodes(nn_c_CorbinsApartmentDiary2);
        c_CorbinsApartmentDiary2.SetDialogue(d_c_CorbinsApartmentDiary2);
        c_CorbinsApartmentDiary2.SetHasMusicClip(true);
        c_CorbinsApartmentDiary2.SetMyMusicClip(m_dramaticMusic2);
        c_CorbinsApartmentDiary2.SetHasClue(true);
        c_CorbinsApartmentDiary2.SetClue("- Danny Carver, the dockworker was clearly the murderer.");
        c_CorbinsApartmentDiary2.SetHasAudioClip(true);
        c_CorbinsApartmentDiary2.SetMyAudioClip(a_rustleFlipPapers);

        // c_CorbinsGreyStart
        nn_c_CorbinsGreyStart.Add(c_CorbinsGreyStraightToCarver);
        nn_c_CorbinsGreyStart.Add(c_CorbinsGreyGentlyToSubject);
        nn_c_CorbinsGreyStart.Add(c_CorbinsApartmentEnter);
        c_CorbinsGreyStart.AddNextNodes(nn_c_CorbinsGreyStart);
        c_CorbinsGreyStart.SetDialogue(d_c_CorbinsGreyStart);

        // c_CorbinsGreyStraightToCarver
        nn_c_CorbinsGreyStraightToCarver.Add(c_CorbinsApartmentEnter);
        c_CorbinsGreyStraightToCarver.AddNextNodes(nn_c_CorbinsGreyStraightToCarver);
        c_CorbinsGreyStraightToCarver.SetDialogue(d_c_CorbinsGreyStraightToCarver);

        // c_CorbinsGreyGentlyToSubject
        nn_c_CorbinsGreyGentlyToSubject.Add(c_CorbinsGreyHintBar);
        nn_c_CorbinsGreyGentlyToSubject.Add(c_CorbinsGreyHintBar);
        c_CorbinsGreyGentlyToSubject.AddNextNodes(nn_c_CorbinsGreyGentlyToSubject);
        c_CorbinsGreyGentlyToSubject.SetDialogue(d_c_CorbinsGreyGentlyToSubject);

        // c_CorbinsGreyHintBar
        nn_c_CorbinsGreyHintBar.Add(c_CorbinsGreyExitStreet);
        c_CorbinsGreyHintBar.AddNextNodes(nn_c_CorbinsGreyHintBar);
        c_CorbinsGreyHintBar.SetDialogue(d_c_CorbinsGreyHintBar);
        c_CorbinsGreyHintBar.SetHasClue(true);
        c_CorbinsGreyHintBar.SetClue("- I should check Fat Angelo's bar for clues...");

        // c_CorbinsGreyExitStreet
        nn_c_CorbinsGreyExitStreet.Add(c_fatAngelosEnter);
        c_CorbinsGreyExitStreet.AddNextNodes(nn_c_CorbinsGreyExitStreet);
        c_CorbinsGreyExitStreet.SetDialogue(d_c_CorbinsGreyExitStreet);
        c_CorbinsGreyExitStreet.SetHasMusicClip(true);
        c_CorbinsGreyExitStreet.SetMyMusicClip(m_rainHard);

        // c_CorbinsApartmentExitStreet
        nn_c_CorbinsApartmentExitStreet.Add(payUsMoney);
        c_CorbinsApartmentExitStreet.AddNextNodes(nn_c_CorbinsApartmentExitStreet);
        c_CorbinsApartmentExitStreet.SetDialogue(d_c_CorbinsApartmentExitStreet);
        c_CorbinsApartmentExitStreet.SetHasClue(true);
        c_CorbinsApartmentExitStreet.SetClue("- This case is clearly about something bigger than a regular homicide.");
        c_CorbinsApartmentExitStreet.SetHasMusicClip(true);
        c_CorbinsApartmentExitStreet.SetMyMusicClip(m_rainHard);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // c_FatAngelosTakeSeatStart
        nn_c_fatAngelosEnter.Add(c_FatAngelosBartenderStart);
        nn_c_fatAngelosEnter.Add(c_FatAngelosTakeSeatStart);
        c_FatAngelosTakeSeatStart.AddNextNodes(nn_c_fatAngelosEnter);
        c_FatAngelosTakeSeatStart.SetDialogue(d_c_FatAngelosTakeSeatStart);

        // c_FatAngelosBartenderStart
        nn_c_FatAngelosBartenderStart.Add(c_FatAngelosBartenderNotInterested);
        c_FatAngelosBartenderStart.AddNextNodes(nn_c_FatAngelosBartenderStart);
        c_FatAngelosBartenderStart.SetDialogue(d_c_FatAngelosBartenderStart);

        // c_FatAngelosBartenderNotInterested
        nn_c_FatAngelosBartenderNotInterested.Add(c_FatAngelosTakeSeatStart);
        c_FatAngelosBartenderNotInterested.AddNextNodes(nn_c_FatAngelosBartenderNotInterested);
        c_FatAngelosBartenderNotInterested.SetDialogue(d_c_FatAngelosBartenderNotInterested);

        // c_FatAngelosTakeSeatStart
        nn_c_FatAngelosTakeSeatStart.Add(c_FatAngelosTakeSeatCarver);
        c_FatAngelosTakeSeatStart.AddNextNodes(nn_c_FatAngelosTakeSeatStart);
        c_FatAngelosTakeSeatStart.SetDialogue(d_c_FatAngelosTakeSeatStart);

        // c_FatAngelosTakeSeatCarver	
        nn_c_FatAngelosTakeSeatCarver.Add(c_FatAngelosTakeSeatCarver2);
        c_FatAngelosTakeSeatCarver.AddNextNodes(nn_c_FatAngelosTakeSeatCarver);
        c_FatAngelosTakeSeatCarver.SetDialogue(d_c_FatAngelosTakeSeatCarver);

        // c_FatAngelosTakeSeatCarver2
        nn_c_FatAngelosTakeSeatCarver2.Add(c_FatAngelosTakeSeatCarver3);
        c_FatAngelosTakeSeatCarver2.AddNextNodes(nn_c_FatAngelosTakeSeatCarver2);
        c_FatAngelosTakeSeatCarver2.SetDialogue(d_c_FatAngelosTakeSeatCarver2);
        c_FatAngelosTakeSeatCarver2.SetHasClue(true);
        c_FatAngelosTakeSeatCarver2.SetClue("- Dockworker Danny Carver murdered Ms. Corbin.");

        // c_FatAngelosTakeSeatCarver3
        nn_c_FatAngelosTakeSeatCarver2.Add(c_FatAngelosExitStreet);
        c_FatAngelosTakeSeatCarver3.AddNextNodes(nn_c_FatAngelosTakeSeatCarver3);
        c_FatAngelosTakeSeatCarver3.SetDialogue(d_c_FatAngelosTakeSeatCarver3);
        c_FatAngelosTakeSeatCarver3.SetHasClue(true);
        c_FatAngelosTakeSeatCarver3.SetClue("- Carver was probably only a hired gun. This is about something bigger...");

        // c_FatAngelosExitStreet
        nn_c_FatAngelosExitStreet.Add(payUsMoney);
        c_FatAngelosExitStreet.AddNextNodes(nn_c_CorbinsGreyExitStreet);
        c_FatAngelosExitStreet.SetDialogue(d_c_CorbinsGreyExitStreet);
        c_FatAngelosExitStreet.SetHasMusicClip(true);
        c_FatAngelosExitStreet.SetMyMusicClip(m_rainHard);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // payUsMoney
        payUsMoney.SetDialogue(d_payUsMoney);
        payUsMoney.SetHasMusicClip(true);
        payUsMoney.SetMyMusicClip(m_menuMusic);
    }


    // ========================================================================================================================================================= CREATE UI
    private void CreateUI()
    {
        canvasMainMenu = (Canvas)GameObject.Find("canvasMainMenu").GetComponent<Canvas>();
        canvasGame = (Canvas)GameObject.Find("canvasGame").GetComponent<Canvas>();


        buttonMainMenu = (Button)GameObject.Find("buttonMainMenu").GetComponent<Button>();      //find menu buttons in Unity
        buttonResume = (Button)GameObject.Find("buttonResume").GetComponent<Button>();
        buttonStartNewGame = (Button)GameObject.Find("buttonStartNewGame").GetComponent<Button>();
        buttonQuit = (Button)GameObject.Find("buttonQuit").GetComponent<Button>();

        button1 = (Button)GameObject.Find("button1").GetComponent<Button>();                        //find gameplay buttons in Unity
        button2 = (Button)GameObject.Find("button2").GetComponent<Button>();
        button3 = (Button)GameObject.Find("button3").GetComponent<Button>();
        button4 = (Button)GameObject.Find("button4").GetComponent<Button>();
        buttonClueList = (Button)GameObject.Find("buttonClueList").GetComponent<Button>();      //find the button to toggle cluelist and wallet in Unity


        dialogueBox = (Text)GameObject.Find("dialogueBox").GetComponent<Text>();                    //find the box where the node dialogue is printed in Unity
        dialogueBoxClueList = (Text)GameObject.Find("dialogueBoxClueList").GetComponent<Text>();    //find the box where the cluelist is printed in Unity
        dialogueBoxCash = (Text)GameObject.Find("dialogueBoxCash").GetComponent<Text>();            //find the box where the player cash is printed in Unity
        this.dialogueBoxClueList.gameObject.SetActive(false);
        this.dialogueBoxCash.gameObject.SetActive(false);

        //  b_background = (Image)GameObject.Find("b_background").GetComponent<Image>();
        /*
		backgroundMainMenu = (Image)GameObject.Find("backgroundMainMenu").GetComponent<Image>();
		backgroundGame = (Image)GameObject.Find("backgroundGame").GetComponent<Image>();
		*/

        //  buttonSoundSource = (AudioSource)GameObject.Find("buttonSoundSource").GetComponent<AudioSource>();      //separate AudioSources for music and other audio
        //  nodeMusicSource = (AudioSource)GameObject.Find("nodeMusicSource").GetComponent<AudioSource>();
        //  nodeSoundSource = (AudioSource)GameObject.Find("nodeSoundSource").GetComponent<AudioSource>();

        buttonResume.onClick.AddListener(delegate { ResumeGame(); });                         //listeners for menu buttons
        buttonMainMenu.onClick.AddListener(delegate { DisplayMainMenu(); });
        buttonStartNewGame.onClick.AddListener(delegate { StartNewGame(); });
        buttonQuit.onClick.AddListener(delegate { QuitGame(); });

        button1.onClick.AddListener(delegate { ButtonClicked(button1); });                       //listeners for gameplay buttons
        button2.onClick.AddListener(delegate { ButtonClicked(button2); });
        button3.onClick.AddListener(delegate { ButtonClicked(button3); });
        button4.onClick.AddListener(delegate { ButtonClicked(button4); });

        buttonClueList.onClick.AddListener(delegate { ToggleClueListAndCash(); });
    }

    // ========================================================================================================================================================= BUTTON FUNCTIONALITY FOR MENUS

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

    public void ResumeGame()                                //is called when Resume is clicked in the menu
    {
       // buttonSoundSource.PlayOneShot(a_buttonClickAudio);

        this.canvasMainMenu.gameObject.SetActive(false);
        this.canvasGame.gameObject.SetActive(true);
    }

    public void QuitGame()                                  //is called when Quit is clicked in the menu
    {
       // buttonSoundSource.PlayOneShot(a_buttonClickAudio);

        Application.Quit();
    }

    public void DisplayMainMenu()                           //is called when Menu button is clicked during gameplay
    {
       // buttonSoundSource.PlayOneShot(a_buttonClickAudio);

        this.buttonResume.gameObject.SetActive(true);
        this.canvasMainMenu.gameObject.SetActive(true);
        this.canvasGame.gameObject.SetActive(false);
    }


    // 	 =========================================================================================================================================================  BUTTON FUNCTIONALITY IN-GAME


    private void ButtonClicked(Button b)                    //is called when any of the gameplay buttons (1,2,3,4) is pressed in a node
    {
        //		nodeMusicSource.Stop();
        // nodeSoundSource.Stop();

        // buttonSoundSource.PlayOneShot(a_buttonClickAudio); //plays the button click sound

        if (currentNode.GetNextIsStore() == true)           //checks if the next node is a store. Is so, launches a separate method for store functionalities
        {
            NodeStore(b);
        }
        else                                                //otherwise normal node functionality is used
        {
            //NodeNormalTest(b);							// uncomment this & KeyboardButtonClicked() in Update() to enter developer more(no slowtyper, keyboard input 1-4.) 
            NodeNormal(b);                                  // comment this if you enable developer mode
        }
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

            Debug.Log(lastItem.GetItemPrice());
            Debug.Log(lastItem.GetItemName());
        }

        int buttonNumber = 0;                   //assign a number 1-4 to each of the gameplay buttons

        if (b == button1)
        {
            buttonNumber = 1;
        }
        if (b == button2)
        {
            buttonNumber = 2;
        }
        if (b == button3)
        {
            buttonNumber = 3;
        }
        if (b == button4)
        {
            buttonNumber = 4;
        }

        this.m_prevMusic = currentNode.GetMyMusicClip();        //these are set here in case two subsequent nodes have the same music clip
        this.prevHasMusic = currentNode.GetHasMusicClip();      //in that case, the music continues without interruption (see below)

        currentNode = currentNode.GetNextNodes()[buttonNumber - 1]; //set the new currentNode based on number of pressed button and the nextNodes list
                                                                    //of the previous node


        if (currentNode.GetHasMusicClip())
        {
            if (prevHasMusic == false)
            {
                //PlayMusic();
            }
            else
            {
                if (!(this.m_prevMusic.Equals(currentNode.GetMyMusicClip())))
                {
                    //nodeMusicSource.Stop();
                   // PlayMusic();

                }
            }

        }
        else
        {
            nodeMusicSource.Stop();
        }

       // PlayClip();                                                 //if the current node has an audio clip, play it


        this.button1.gameObject.SetActive(false);                   //by default, disable all the gameplay buttons until
        this.button2.gameObject.SetActive(false);                   //we know the possible options of currentNode
        this.button3.gameObject.SetActive(false);
        this.button4.gameObject.SetActive(false);

        StartCoroutine(TypeTextAndEnableButtonNormal(currentNode.GetDialogue())); //start stylized text output
                                                                                  //and enable correct gameplay buttons for the node

        if (currentNode.GetHasClue() == true)
        {                       //write to cluelist
            steele.AddToClueList(currentNode.GetClue());
            WriteToClueList();

        }
    }

    private void NodeStore(Button b)
    {
        int buttonNumber = 0;

        if (b == button1)
        {
            buttonNumber = 1;
        }
        if (b == button2)
        {
            buttonNumber = 2;
        }
        if (b == button3)
        {
            buttonNumber = 3;
        }
        if (b == button4)
        {
            buttonNumber = 4;
        }

        this.m_prevMusic = currentNode.GetMyMusicClip();
        this.prevHasMusic = currentNode.GetHasMusicClip();

        currentNode = currentNode.GetNextNodes()[buttonNumber - 1];

        if (currentNode.GetHasMusicClip())
        {
            if (prevHasMusic == false)
            {
               // PlayMusic();
            }
            else
            {
                if (!(this.m_prevMusic.Equals(currentNode.GetMyMusicClip())))
                {
                    nodeMusicSource.Stop();
                   // PlayMusic();

                }
            }

        }
        else
        {
            //nodeMusicSource.Stop();
        }

       // PlayClip();

        this.button1.gameObject.SetActive(false);
        this.button2.gameObject.SetActive(false);
        this.button3.gameObject.SetActive(false);
        this.button4.gameObject.SetActive(false);

        this.dialogueBox.text = "";

        StartCoroutine(TypeTextAndEnableButtonStore(currentNode.GetDialogue()));

        if (currentNode.GetHasClue() == true)
        {
            steele.AddToClueList(currentNode.GetClue());
            WriteToClueList();
        }
    }

    // 	 ========================================================================================================================================================= EXECUTING A NODE (TESTING)
    //	THIS IS A TEST VERSION OF NodeNormal METHOD USED DURING TESTING&DEBUGGING. BASICALLY THE SAME AS ABOVE.
    private void NodeNormalTest(Button b)
    {
        this.dialogueBox.text = "";

        if (currentNode.GetNextIsReceipt() == true)
        {
            steele.SetCash(-(currentNode.GetDic()[b].GetItemPrice()));
            this.lastItem = currentNode.GetDic()[b];
            currentNode.GetNextNodes()[0].SetDialogue("You bought " + lastItem.GetItemName() + " for the low low price of " + lastItem.GetItemPrice() + " moneydollars.");

            UpdateCash();

            Debug.Log(lastItem.GetItemPrice());
            Debug.Log(lastItem.GetItemName());
        }

        int buttonNumber = 0;

        if (b == button1)
        {
            buttonNumber = 1;
        }
        if (b == button2)
        {
            buttonNumber = 2;
        }
        if (b == button3)
        {
            buttonNumber = 3;
        }
        if (b == button4)
        {
            buttonNumber = 4;
        }

        this.m_prevMusic = currentNode.GetMyMusicClip();
        this.prevHasMusic = currentNode.GetHasMusicClip();

        currentNode = currentNode.GetNextNodes()[buttonNumber - 1];

        if (currentNode.GetHasMusicClip())
        {
            if (prevHasMusic == false)
            {
               // PlayMusic();
            }
            else
            {
                if (!(this.m_prevMusic.Equals(currentNode.GetMyMusicClip())))
                {
                   // nodeMusicSource.Stop();
                    //PlayMusic();

                }
            }

        }
        else
        {
            nodeMusicSource.Stop();
        }

        //PlayClip();

        this.button1.gameObject.SetActive(false);
        this.button2.gameObject.SetActive(false);
        this.button3.gameObject.SetActive(false);
        this.button4.gameObject.SetActive(false);

        switch (currentNode.GetNextNodes().Count)
        {

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
        dialogueBox.text = currentNode.GetDialogue();


        if (currentNode.GetHasClue() == true)
        {
            steele.AddToClueList(currentNode.GetClue());
            WriteToClueList();
        }
    }

    // 	 ========================================================================================================================================================= PLAYING AUDIO AND MUSIC

  /*  private void PlayClip()                                 //the method used for playing an audioclip if the node has one
    {
        if (currentNode.GetHasAudioClip() == true)
        {
            //nodeSoundSource.PlayOneShot(currentNode.GetMyAudioClip());
        }
    }

    private void PlayMusic()                                //...and the same for music 
    {
        if (currentNode.GetHasMusicClip() == true)
        {
            nodeMusicSource.clip = currentNode.GetMyMusicClip();
            nodeMusicSource.Play();
        }
    }*/

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

    private IEnumerator TypeTextAndEnableButtonStore(string textToPrint)    //separate method for printing text in a store node
    {

        foreach (char letter in textToPrint.ToCharArray())
        {
            //Debug.Log(letter);
            this.dialogueBox.text += letter;
            yield return new WaitForSeconds(letterPause);
        }

        foreach (KeyValuePair<Button, Item> _item in currentNode.GetDic())
        {   //get the product catalog (Dictionary) from the store node
            if (_item.Value.GetItemPrice() <= steele.GetCash())
            {           //activate buttons only for items that the player can afford

                _item.Key.gameObject.SetActive(true);
            }
        }
    }

    private void UpdateCash()                                               //write the new cash amount to the screen
    {
        this.dialogueBoxCash.text = "Moneydollars: " + steele.GetCash().ToString();
    }

    private void WriteToClueList()
    {                                       //write the last entry on the cluelist to screen
        this.dialogueBoxClueList.text += steele.GetClueList()[steele.GetClueList().Count - 1] + "\n";
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

    public void KeyboardEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.canvasGame.isActiveAndEnabled)
            {
                DisplayMainMenu();
            }
            else
            {
                Application.Quit();
            }
        }
    }

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

        this.buttonResume.gameObject.SetActive(false);
        this.canvasMainMenu.gameObject.SetActive(true);
        this.canvasGame.gameObject.SetActive(false);

        // currentNode.SetMyMusicClip(m_menuMusic);
        // nodeMusicSource.clip = currentNode.GetMyMusicClip();
        // nodeMusicSource.Play();
    }

    public void Update()
    {
        KeyboardEsc();
        //KeyboardButtonClicked();					// uncomment if you want to ski
    }

}