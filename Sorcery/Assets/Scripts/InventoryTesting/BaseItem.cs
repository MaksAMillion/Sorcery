using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem {


	private string _name;
	private string _desc;
	private int _value;
	private string _type;
	private int _amt;
	private string _imgName;

	public BaseItem(string name, string desc, int value,string imageName ,string type){
		ItemName = name;
		ItemDesc = desc;
		ItemValue = value;
		ItemType = type;
		ItemAmt = 1;
		ItemImgName = imageName;
	}

	public string ItemName
	{
		get{ return _name; }
		set{ _name = value;}
	}
	public string ItemDesc
	{
		get{ return _desc;}
		set{ _desc = value;}
	}
	public int ItemValue{
		get{ return _value;}
		set{ _value = value;}
	}
	public string ItemType{
		get{ return _type;}
		set{ _type = value;}
	}
	public int ItemAmt{
		get{ return _amt;}
		set{ _amt = value;}
	}
	public string ItemImgName
	{
		get{ return _imgName; }
		set{ _imgName = value;}
	}
}
