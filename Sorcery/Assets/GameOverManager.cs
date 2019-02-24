using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	private Text finalScore;

	void Awake(){
		int money = PlayerPrefs.GetInt ("CurrentMoney");
		float credits = PlayerPrefs.GetFloat ("CurrentCredits");

		float score = money + credits;

		Debug.Log (money);

		PlayerPrefs.SetFloat ("Score", score);
	}

	void Start () {
		finalScore = GameObject.FindGameObjectWithTag ("finalScore").GetComponent<Text> ();
		finalScore.text = PlayerPrefs.GetFloat ("Score").ToString ();
	}
	
	void Update () {
		
	}

	public void NewGame(){
		PlayerPrefs.SetInt ("CurrentMoney", 10000);
		PlayerPrefs.SetFloat ("CurrentStam", 10);
		PlayerPrefs.SetFloat ("MaxStam", 20);

		PlayerPrefs.SetFloat ("CurrentCredits", 20);
		PlayerPrefs.SetFloat ("MaxCredits", 100);

		PlayerPrefs.SetFloat ("GameTime", 600);

		QuestList.questListInfo.generateQuestList ();
		SceneManager.LoadScene ("Dorm");
	}

	public void MainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
