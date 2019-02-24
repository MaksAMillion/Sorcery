using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	[SerializeField] private GameObject avatarNumber;
	private Text avatarNumberText;

	public GameObject mainMenuScreen;
	public GameObject avatarSelectionScreen;
	public GameObject enterNameScreen;
	public GameObject optionsScreen;

	public GameObject leftButton;
	public GameObject rightButton;
	private Button avatar1Button;
	private Button avatar2Button;
	private Color curColor1;
	private Color curColor2;

	private int avatarInt;
	public Sprite avatar1;
	public Sprite avatar2;
	public GameObject avatarImageObject;
	private Image avatarImage;

	private string playerName;
	public GameObject previewName;
	public InputField nameInputField;

	public GameObject confirmName;
	public GameObject noNameError;
	public GameObject nameLengthError;


	private void Awake() {
		
		PlayerPrefs.DeleteAll ();

//		if(!PlayerPrefs.HasKey("CurrentMoney")){
//			PlayerPrefs.SetInt ("CurrentMoney", 10000);
//		}
//		if(!PlayerPrefs.HasKey("CurrentStam")){
//			PlayerPrefs.SetInt ("CurrentStam", 10);
//		}
//		if(!PlayerPrefs.HasKey("MaxStam")){
//			PlayerPrefs.SetInt ("MaxStam", 20);
//		}

//		PlayerPrefs.DeleteAll ();
//		PlayerPrefs.DeleteKey("PlayerName");
//		PlayerPrefs.SetInt ("CurrentMoney", 10000);
//		PlayerPrefs.SetFloat ("CurrentStam", 10);
//		PlayerPrefs.SetFloat ("MaxStam", 20);
//
//		PlayerPrefs.SetFloat ("CurrentCredits", 70);
//		PlayerPrefs.SetFloat ("MaxCredits", 100);

	}

	// Use this for initialization
	void Start () {
		AudioController.Audio.PlayIntroMusic();

		avatarNumberText = avatarNumber.GetComponent<Text> ();
		avatarNumberText.text = "0";

		avatar1Button = leftButton.GetComponent<Button> ();
		avatar2Button = rightButton.GetComponent<Button> ();
		curColor1 = avatar1Button.GetComponent<Image> ().color;
		curColor2 = avatar2Button.GetComponent<Image> ().color;

		avatarImage = avatarImageObject.GetComponent<Image> ();

		mainMenuScreen.SetActive (true);
		avatarSelectionScreen.SetActive (false);
		optionsScreen.SetActive (false);
		enterNameScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.HasKey ("AvatarSelect")) {
			avatarInt = PlayerPrefs.GetInt ("AvatarSelect");

			switch(avatarInt){
				case 1:
					avatarImage.sprite = avatar1;
					break;
				case 2:
					avatarImage.sprite = avatar2;
					break;
				default:
					avatarImage.sprite = avatar1;
					break;
			}	
		}
	}

	public void GoToMainMenu(){
		mainMenuScreen.SetActive (true);
		avatarSelectionScreen.SetActive (false);
		optionsScreen.SetActive (false);	
		enterNameScreen.SetActive (false);
	}

	public void GoToAvatarSelection(){
		mainMenuScreen.SetActive (false);
		avatarSelectionScreen.SetActive (true);
		optionsScreen.SetActive (false);	
		enterNameScreen.SetActive (false);
	}

	public void GoToName(){
		mainMenuScreen.SetActive (false);
		avatarSelectionScreen.SetActive (false);
		optionsScreen.SetActive (false);	
		enterNameScreen.SetActive (true);

		if(!PlayerPrefs.HasKey("AvatarSelect"))
			PlayerPrefs.SetInt ("AvatarSelect", 1);
	}

	public void GoToOptions(){
		mainMenuScreen.SetActive (false);
		avatarSelectionScreen.SetActive (false);
		optionsScreen.SetActive (true);	
		enterNameScreen.SetActive (false);
	}

	public void Avatar1Selection(){
		PlayerPrefs.SetInt ("AvatarSelect", 1);
		avatarNumberText.text = "1";
		avatar1Button.GetComponent<Image> ().color = curColor1;
		avatar2Button.GetComponent<Image> ().color = Color.black;
	}

	public void Avatar2Selection(){
		PlayerPrefs.SetInt ("AvatarSelect", 2);
		avatarNumberText.text = "2";
		avatar1Button.GetComponent<Image> ().color = Color.black;
		avatar2Button.GetComponent<Image> ().color = curColor2;
	}

	public void Avatar3Selection(){
		PlayerPrefs.SetInt ("AvatarSelect", 3);
		avatarNumberText.text = "3";
	}


	public void OnSubmit(){
		playerName = nameInputField.text;

		Text preview = previewName.GetComponent<Text> ();
		preview.text = "Name: \n" + playerName;
	}

	public void PlayGame(){

		playerName = nameInputField.text;

		if(playerName.Length <= 0){
			noNameError.SetActive (true);
			nameLengthError.SetActive (false);
			confirmName.SetActive (false);
		}else if (playerName.Length > 15) {
			noNameError.SetActive (false);
			nameLengthError.SetActive (true);
			confirmName.SetActive (false);
		}else{
			noNameError.SetActive (false);
			nameLengthError.SetActive (false);
			confirmName.SetActive (true);

			PlayerPrefs.DeleteKey("PlayerName");
			PlayerPrefs.SetInt ("CurrentMoney", 10000);
			PlayerPrefs.SetFloat ("CurrentStam", 10);
			PlayerPrefs.SetFloat ("MaxStam", 20);

			PlayerPrefs.SetFloat ("CurrentCredits", 20);
			PlayerPrefs.SetFloat ("MaxCredits", 100);

			PlayerPrefs.SetFloat ("GameTime", 600);

			PlayerPrefs.SetString ("PlayerName", playerName);
			AudioController.Audio.StopMusic ();
			QuestList.questListInfo.generateQuestList ();
			SceneManager.LoadScene ("Dorm");
        } 
	}

}
