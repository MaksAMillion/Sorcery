using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour {

	private float gameTime;
	private  Text timeText;
	private bool timerIsActive = false;

	void Awake(){
		Messenger.AddListener(GameEvent.STOP_TIME, stopTime);
		Messenger.AddListener(GameEvent.START_TIME, startTime);
	}

	void OnDestroy(){
		Messenger.RemoveListener(GameEvent.STOP_TIME, stopTime);
		Messenger.RemoveListener(GameEvent.START_TIME, startTime);
	}

	void Start () {
		timeText = GameObject.FindGameObjectWithTag ("TimeText").GetComponent<Text> ();

		if (PlayerPrefs.HasKey ("GameTime"))
			gameTime = PlayerPrefs.GetFloat ("GameTime");
		else
			gameTime = 600;

	}

	void Update () {

		if (timerIsActive) {
			gameTime -= Time.deltaTime;
			PlayerPrefs.SetFloat ("GameTime", gameTime);

			int sec = (int)(gameTime % 60);
			int min = (int)(gameTime / 60) % 60;

			string timeString = string.Format ("{0:00}:{1:00}", min, sec);

			timeText.text = timeString;

			if (gameTime <= 0) {
				SceneManager.LoadScene ("Score");
			}

		}

	}
		

	public void stopTime (){
		timerIsActive = false;
	}

	public void startTime(){
		timerIsActive = true;
	}


}
