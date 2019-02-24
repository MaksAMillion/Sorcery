using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory2Manage : MonoBehaviour {

	private Dictionary<string, BaseItem> _items;
	public GameObject container;
	//public GameObject itemDisplay;
	public GameObject itemNameDisplay;
	public GameObject itemDescDisplay;
	public GameObject itemPicDisplay;


	private Transform containerTransform;
	private string currentItemName="";
	private int xPos=0;
	private int yPos=0;

	public void Start(){
		

	}
	public void Awake(){
		_items = new Dictionary<string,BaseItem> ();
		DontDestroyOnLoad (gameObject);
	}
	public void OnEnable(){
		containerTransform = container.GetComponent<Transform> ();
		_items.Add("coffee",new BaseItem("coffee","How else do you wake up?",1,"coffee","energy"));
		_items.Add("coffee2",new BaseItem("coffee2","How else do you wake up?2",1,"alertBubble","energy"));
		_items.Add("coffee3",new BaseItem("coffee3","How else do you wake up?2",1,"coffee","energy"));
		_items.Add("coffee4",new BaseItem("coffee4","How else do you wake up?2",1,"coffee","energy"));
		_items.Add("coffee5",new BaseItem("coffee5","How else do you wake up?2",1,"coffee","energy"));
		_items.Add("coffee6",new BaseItem("coffee6","How else do you wake up?2",1,"coffee","energy"));
		_items.Add("coffee7",new BaseItem("coffee7","How else do you wake up?2",1,"coffee","energy"));

		updateItemDisplay ();
		//updateItemDisplay ();
	}

	public void updateItemDisplay(){
		
		//empty all the children in the content container
		int children = containerTransform.childCount;
		for (int i = 0; i < children; i++) {
			DestroyImmediate(containerTransform.GetChild (0).gameObject);
		}
		//go through list of items and add them to content container
		foreach (KeyValuePair<string, BaseItem> item in _items)
		{
			//get the infor for the item
			int amount = item.Value.ItemAmt;
			string imgName = item.Value.ItemImgName;

			GameObject itemToAddBase = (GameObject)Resources.Load("itemBox");
			GameObject itemToAddObj = Instantiate(itemToAddBase) as GameObject;
			itemToAddObj.transform.SetParent(containerTransform,false);
			itemToAddObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgName);
			itemToAddObj.transform.localScale = new Vector3(.5f, .5f, 1f);
			itemToAddObj.GetComponent<RectTransform> ().localPosition = new Vector3 (xPos, yPos, 0);
			xPos += (int)itemToAddObj.GetComponent<RectTransform> ().rect.width;
			//Add the onclick to the item
			Button itemButton = itemToAddObj.GetComponent<Button> ();
			itemButton.onClick.AddListener (delegate{showDetails(item.Key);});

			//Change the number of inventory
			Text itemAmountText = itemToAddBase.GetComponentInChildren<Text> ();
			itemAmountText.text = amount.ToString();

			if(string.IsNullOrEmpty(currentItemName)){
				showDetails (item.Key);
			}
		}


	}
	public void showDetails(string itemName){
		BaseItem foundItem;
		_items.TryGetValue (itemName,out foundItem);

		currentItemName = itemName;
		itemNameDisplay.GetComponent<Text> ().text = itemName;

		itemDescDisplay.GetComponent<Text> ().text = foundItem.ItemDesc;

		itemPicDisplay.GetComponent<Image>().sprite = Resources.Load<Sprite>(foundItem.ItemImgName);
		itemPicDisplay.GetComponent<Image> ().preserveAspect = true;


	}
	public void addItem(BaseItem newItem){
		BaseItem foundItem;
		if(_items.TryGetValue(newItem.ItemName,out foundItem)){
			foundItem.ItemAmt++;
		}
		else{
			_items.Add(newItem.ItemName,newItem);
		}

	}
	public void useItem(){
		//Put in code that handles the different item types

		tossItem ();


	}
	public void tossItem(){
		BaseItem foundItem;
		_items.TryGetValue (currentItemName,out foundItem);
		foundItem.ItemAmt--;
		if (foundItem.ItemAmt <= 0) {
			_items.Remove (currentItemName);
			currentItemName = "";
		}
		updateItemDisplay ();
	}
				

	public void debugItem(string s){
	
		Debug.Log (s);
	}
}
