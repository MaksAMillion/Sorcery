using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class QuestList : MonoBehaviour {

	public static QuestList questListInfo;

	//  Week 1 
	//		Josh Quest
	public bool findBook = false;
	public bool plantTrees = false;

	void Awake(){

		if(questListInfo == null){
			DontDestroyOnLoad (gameObject);
			questListInfo = this;
		}else if(questListInfo != this){
			Destroy (gameObject);
		}

	}

	void Start () {
	}

	void Update () {

	}

	public void generateQuestList(){
	
		if (Managers.Mission.curWeek == 1 && Managers.Mission.curDay == 0) {
			Managers.QuestLog.AddQuest ("Visit: Art Professor");
			Managers.QuestLog.AddQuest ("Locate: Health Building Entrance");
			Managers.QuestLog.AddQuest ("Buy: Cafeteria Lunch");
		}

		if (Managers.Mission.curWeek == 2 && Managers.Mission.curDay == 1) {
			Managers.QuestLog.ClearQuests ();
		}

	}
		

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/questListInfo.dat");

		QuestListData data = new QuestListData ();
		data.findBook = findBook;
		data.plantTrees = plantTrees;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/questListInfo.dat", FileMode.Open);
		QuestListData data = (QuestListData)bf.Deserialize (file);
		file.Close ();

		findBook = data.findBook;
		plantTrees = data.plantTrees;
	}
}

[Serializable]
class QuestListData{

	public bool findBook;
	public bool plantTrees;
}