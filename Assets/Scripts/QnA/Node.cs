using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Node
{
	private string dialogue = "";
	private bool hasClue = false;
	private string clue = "";
    private int score = 0;
    private int total = 0;
    private string expression = "";
    private string answer = "";
    private string emotion = "";
    private List<Node> nextNodes = new List<Node>();
	private bool nextIsStore = false;
	private bool nextIsReceipt = false;
	private string Button1text = "";
	private string Button2text = "";
	private string Button3text = "";
	private string Button4text = "";
	private Dictionary<Button, Item> storeItems = new Dictionary<Button, Item>();
//	private Sprite myBackground;

	private AudioClip myAudioClip;
	private AudioClip myMusicClip;
	private bool hasAudioClip = false;
	private bool hasMusicClip = false;

	public Node  ()
	{
	}


	// =====================================
	// GET INFORMATION ABOUT NODE VARIABLES
	// =====================================

	public string GetDialogue()
	{
		return this.dialogue;
	}

	public List<Node> GetNextNodes()
	{
		return this.nextNodes;
	}

	public bool GetHasClue()
	{
		return this.hasClue;
	}

    public string GetClue()
    {
        return this.clue;
    }
    public int GetTotal()
    {
        return this.total;
    }
    public string GetAnswer()
    {
        return this.answer;
    }
    public string GetEmotion()
    {
        return this.emotion;
    }
    public int GetScore()
    {
        return this.score;
    }
    public string GetExpression()
    {
        return this.expression;
    }

    public bool GetNextIsStore()
	{
		return this.nextIsStore;
	}

	public bool GetNextIsReceipt()
	{
		return this.nextIsReceipt;
	}

	public Dictionary<Button, Item> GetDic()
	{
		return this.storeItems;
	}

	public AudioClip GetMyAudioClip()
	{
		return this.myAudioClip;
	}

	public AudioClip GetMyMusicClip()
	{
		return this.myMusicClip;
	}

	public bool GetHasAudioClip()
	{
		return this.hasAudioClip;
	}

	public bool GetHasMusicClip()
	{
		return this.hasMusicClip;
	}

	public string GetButton1text()
	{
		return this.Button1text;
	}

	public string GetButton2text()
	{
		return this.Button2text;
	}

	public string GetButton3text()
	{
		return this.Button3text;
	}

	public string GetButton4text()
	{
		return this.Button4text;
	}


	//	public Sprite GetMyBackground()
	//	{
	//		return this.myBackground;
	//	}

	// =============================
	// SET VALUES OF NODE VARIABLES
	// =============================

	public void AddNextNodes(List<Node> _nextNodes)
	{
		this.nextNodes = _nextNodes;
	}

    public void SetDialogue(string _dialogue)
    {
        this.dialogue = _dialogue;
    }
    public void SetEmotion(string _emotion)
    {
        this.emotion = _emotion;
    }

    public void SetHasClue(bool _hasClue)
	{
		this.hasClue = _hasClue;
	}

    public void SetClue(string _clue)
    {
        this.clue = _clue;
    }
    public void SetAnswer(string _answer)
    {
        this.answer = _answer;
    }

    public void SetExpression(string _expression)
    {
        this.expression = _expression;
    }
    public void SetScore(int _score)
    {
        this.score = _score;
    }
    public void SetTotal(int _total)
    {
        this.total = _total;
    }
    public void SetNextIsStore(bool _isStore)
	{
		this.nextIsStore = _isStore;
	}

	public void SetStoreItems(Button _button, Item _item)
	{
		this.storeItems.Add(_button, _item);
	}

	public void SetNextIsReceipt(bool _isReceipt)
	{
		this.nextIsReceipt = _isReceipt;
	}

	public void SetHasAudioClip(bool _hasAudioClip) 
	{
		this.hasAudioClip = _hasAudioClip;
	}

	public void SetMyAudioClip(AudioClip _myAudioClip)
	{
		this.myAudioClip = _myAudioClip;
	}

	public void SetHasMusicClip(bool _hasMusicClip)
	{
		this.hasMusicClip = _hasMusicClip;
	}

	public void SetMyMusicClip(AudioClip _myMusicClip)
	{
		this.myMusicClip = _myMusicClip;
	}


	public void SetButton1text(string _Button1text)
	{
		this.Button1text = _Button1text;
	}


	public void SetButton2text(string _Button2text)
	{
		this.Button2text = _Button2text;
	}



	public void SetButton3text(string _Button3text)
	{
		this.Button3text = _Button3text;
	}



	public void SetButton4text(string _Button4text)
	{
		this.Button4text = _Button4text;
	}














	//	public void SetMyBackground(Sprite _myImage)
	//	{
	//		this.myBackground = _myImage;
	//	}
}