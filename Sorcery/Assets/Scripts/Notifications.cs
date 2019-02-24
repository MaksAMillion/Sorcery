using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Notifications : MonoBehaviour {

	[SerializeField] private AudioClip ding;

	[SerializeField] private GameObject plusStamina;
	[SerializeField] private GameObject minusStamina;
	[SerializeField] private GameObject plusMoney;
	[SerializeField] private GameObject minusMoney;
	[SerializeField] private GameObject plusCredits;
	[SerializeField] private GameObject textNotification;

	void Awake(){
		Messenger.AddListener(GameEvent.UI_PLUS_STAMINA, showPlusStamina);
		Messenger.AddListener(GameEvent.UI_MINUS_STAMINA, showMinusStamina);
		Messenger.AddListener(GameEvent.UI_PLUS_MONEY, showPlusMoney);
		Messenger.AddListener(GameEvent.UI_MINUS_MONEY, showMinusMoney);
		Messenger.AddListener(GameEvent.UI_PLUS_CREDITS, showPlusCredits);
		Messenger<string>.AddListener(GameEvent.UI_TEXT_NOTIFICATION, showTextNotification);
	}

	void OnDestroy(){
		Messenger.RemoveListener(GameEvent.UI_PLUS_STAMINA, showPlusStamina);
		Messenger.RemoveListener(GameEvent.UI_MINUS_STAMINA, showMinusStamina);
		Messenger.RemoveListener(GameEvent.UI_PLUS_MONEY, showPlusMoney);
		Messenger.RemoveListener(GameEvent.UI_MINUS_MONEY, showMinusMoney);
		Messenger.RemoveListener(GameEvent.UI_PLUS_CREDITS, showPlusCredits);
		Messenger<string>.RemoveListener(GameEvent.UI_TEXT_NOTIFICATION, showTextNotification);
	}

	void Start(){
	}
		

	void Update () {

	}


	public void showPlusStamina(){
		StartCoroutine(PlusStamina());
	}

	public void showMinusStamina(){
		StartCoroutine(MinusStamina());
	}

	public void showPlusMoney(){
		StartCoroutine(PlusMoney());
	}

	public void showMinusMoney(){
		StartCoroutine(MinusMoney());
	}

	public void showPlusCredits(){
		StartCoroutine(PlusCredits());
	}

	public void showTextNotification(string message){
		StartCoroutine (TextNotification(message));
	}

	IEnumerator PlusStamina(){
		AudioController.Audio.PlaySound(ding);

		plusStamina.SetActive (true);
		yield return new WaitForSeconds(5);
		plusStamina.SetActive (false);
	}
		
	IEnumerator MinusStamina(){
		AudioController.Audio.PlaySound(ding);

		minusStamina.SetActive (true);
		yield return new WaitForSeconds(5);
		minusStamina.SetActive (false);
	}

	IEnumerator PlusMoney(){
		AudioController.Audio.PlaySound(ding);

		plusMoney.SetActive (true);
		yield return new WaitForSeconds(5);
		plusMoney.SetActive (false);
	}

	IEnumerator MinusMoney(){
		AudioController.Audio.PlaySound(ding);

		minusMoney.SetActive (true);
		yield return new WaitForSeconds(5);
		minusMoney.SetActive (false);
	}

	IEnumerator PlusCredits(){
		AudioController.Audio.PlaySound(ding);

		plusCredits.SetActive (true);
		yield return new WaitForSeconds(5);
		plusCredits.SetActive (false);
	}

	IEnumerator TextNotification(string message){
		AudioController.Audio.PlaySound(ding);

		Text txt = textNotification.GetComponent<Text> ();
		txt.text = message;
			
		textNotification.SetActive (true);
		yield return new WaitForSeconds(5);
		textNotification.SetActive (false);
	}

}

