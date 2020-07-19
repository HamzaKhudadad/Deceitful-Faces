using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerQ {

	private List<string> clueList = new List<string>();
	private int cash = 50;

	public List<string> GetClueList()
	{
		return this.clueList;
	}

	public int GetCash()
	{
		return this.cash;
	}
	
	public void SetCash (int change)
	{
		this.cash += change;
	}

	public void ResetCash()
	{
		this.cash = 50;
	}

	public void AddToClueList(string _clue)
	{
		this.clueList.Add (_clue);
	}
}

