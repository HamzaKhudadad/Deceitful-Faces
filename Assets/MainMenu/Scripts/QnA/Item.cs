using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item
{
	private string itemName;
	private int itemPrice;

	public Item (string _itemName, int _itemPrice)
	{
		this.itemName = _itemName;
		this.itemPrice = _itemPrice;
	}

	public string GetItemName()
	{
		return this.itemName;
	}

	public void SetItemName(string _itemName)
	{
		this.itemName = _itemName;
	}

	public int GetItemPrice()
	{
		return this.itemPrice;
	}

	public void SetItemPrice(int _itemPrice)
	{
		this.itemPrice = _itemPrice;
	}
}