using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;


public class hudUI : MonoBehaviour {

/// Example:   		Messenger<int>.Broadcast(GameEvent.PAY_MONEY, moneyAmount );
/// Example: 		Messenger.Broadcast(GameEvent.FILL_STAMINA_MAX);  
	/// Example:		Messenger.Broadcast(GameEvent.TOGGLE_HUD);  

	void Awake()
	{
		scene = SceneManager.GetActiveScene();

		Messenger.AddListener (GameEvent.TOGGLE_PHONE_MENU,togglePhoneMenu);
		Messenger<int>.AddListener(GameEvent.PAY_MONEY, subtractMoney);
		Messenger<int>.AddListener(GameEvent.EARN_MONEY, addMoney);
		Messenger<float>.AddListener (GameEvent.ADD_CREDITS, addCredits);
		Messenger<float>.AddListener (GameEvent.ADD_STAMINA, addStamina);
		Messenger<float>.AddListener (GameEvent.LOSE_STAMINA, loseStamina);
		Messenger.AddListener (GameEvent.FILL_STAMINA_MAX, fillStaminaMax);
		Messenger.AddListener (GameEvent.TURN_ON_ALERT, turnOnAlert);
		Messenger.AddListener (GameEvent.TURN_OFF_ALERT, turnOffAlert);
		Messenger.AddListener (GameEvent.TOGGLE_HUD, toggleHUD);

//		PlayerPrefs.SetFloat ("CurrentCredits", 50);

	}

	void OnDestroy()
	{
		Messenger.RemoveListener (GameEvent.TOGGLE_PHONE_MENU,togglePhoneMenu);
		Messenger<int>.RemoveListener(GameEvent.PAY_MONEY, subtractMoney);
		Messenger<int>.RemoveListener(GameEvent.EARN_MONEY, addMoney);
		Messenger<float>.RemoveListener (GameEvent.ADD_CREDITS, addCredits);
		Messenger<float>.RemoveListener (GameEvent.ADD_STAMINA, addStamina);
		Messenger<float>.RemoveListener (GameEvent.LOSE_STAMINA, loseStamina);
		Messenger.RemoveListener (GameEvent.FILL_STAMINA_MAX,fillStaminaMax);
		Messenger.RemoveListener (GameEvent.TURN_ON_ALERT, turnOnAlert);
		Messenger.RemoveListener (GameEvent.TURN_OFF_ALERT, turnOffAlert);
		Messenger.RemoveListener (GameEvent.TOGGLE_HUD, toggleHUD);

	}

	Scene scene;
//	GameObject questApp;
	public Animator questAppAnim;

	public GameObject phoneMenu; //FILL THIS WITH THE PHONE UI
	public GameObject alertUI; //FILL WITH THE ALERT ICON ABOVE PHONE
	MoveToClickPoint move;

	GameObject hudMenu;
	float DEFAULT_STAMINA;
	float DEFAULT_CREDITS;

	// Use this for initialization
	void Start () {

        //Start all the defaults
        if (SceneManager.GetActiveScene().name != "ScienceHallway")
        {
            Messenger.Broadcast(GameEvent.START_TIME);

        }

        hudMenu = GameObject.FindGameObjectWithTag("gameUI");
        //		questApp = GameObject.Find ("Quests App");


        DEFAULT_STAMINA = 100;
		DEFAULT_CREDITS = 10;
		setDisplayName ();
		adjustStaminaBar ();
		displayMoney ();
		setPortrait ();
		setAlerts ();
		adjustCreditsBar ();

		//Testing

		/*PlayerPrefs.SetInt("CurrentMoney",1000);
		PlayerPrefs.SetFloat("MaxStam",100);
		PlayerPrefs.SetFloat("CurrentStam",100);

		loseStamina (30);
		fillStaminaMax ();
		subtractMoney (100);
		setDisplayName ("Hello World");*/

		if (Managers.Mission != null && Managers.Mission.curWeek == 1 && Managers.Mission.curDay == 0 && scene.name == "Dorm" ){
//			togglePhoneMenu();
//			questAppAnim.SetBool ("NewQuests", true);
//			questApp.SetActive (true);
		}

	}

	//SHOW PHONE MENU
	public void togglePhoneMenu(){
		// move = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToClickPoint>();

		if (phoneMenu.activeSelf) {
			//moveToClickScript.enabled = false;
			// move.enabled = true;
			phoneMenu.SetActive (false);
			hudMenu.SetActive (true);

            Messenger.Broadcast(GameEvent.PLAYER_GO);
        } else {
			//moveToClickScript.enabled = true;
			// move.enabled = false;
			phoneMenu.SetActive (true);
			hudMenu.SetActive (false);

            Messenger.Broadcast(GameEvent.PLAYER_STOP);
        }
		// Debug.Log(move.isActiveAndEnabled);
	}

	//SHOW/HIDE HUD - 
	public void toggleHUD(){
		if (hudMenu.activeSelf) {
			hudMenu.SetActive (false);
		} else {
			hudMenu.SetActive (true);
		}
	}

	//MONEY CONTROLS
	public void subtractMoney(int amountToSubtract){
		int curMoney = 0;
		if (PlayerPrefs.HasKey ("CurrentMoney")) {
			curMoney = PlayerPrefs.GetInt ("CurrentMoney");
		} 
		curMoney -= amountToSubtract;
		if (curMoney < 0) {
			curMoney = 0;
		}
		PlayerPrefs.SetInt ("CurrentMoney",curMoney);
		displayMoney ();


	}
	public void addMoney(int amountToAdd){
		int curMoney = 0;
		if (PlayerPrefs.HasKey ("CurrentMoney")) {
			curMoney = PlayerPrefs.GetInt ("CurrentMoney");
		} 
		curMoney += amountToAdd;
		PlayerPrefs.SetInt ("CurrentMoney",curMoney);
		displayMoney ();
	}

	public void displayMoney(){
		int curMoney=0;
		if (PlayerPrefs.HasKey ("CurrentMoney")) {
			curMoney = PlayerPrefs.GetInt ("CurrentMoney");
		} 
		GameObject moneyUI = GameObject.FindGameObjectWithTag ("moneyUI");
		moneyUI.GetComponent<UnityEngine.UI.Text>().text=curMoney.ToString();
	}

	//  CREDIT CONTROLS
	public void adjustCreditsBar(){
		float currCred;
		float maxCred;

		if (PlayerPrefs.HasKey ("MaxCredits") && PlayerPrefs.HasKey ("CurrentCredits")) {
			currCred = PlayerPrefs.GetFloat ("CurrentCredits");
			maxCred = PlayerPrefs.GetFloat ("MaxCredits");
		} else {
			currCred = DEFAULT_CREDITS;
			maxCred = DEFAULT_CREDITS;
			PlayerPrefs.SetFloat ("CurrentCredits",currCred);
			PlayerPrefs.SetFloat ("MaxCredits",maxCred);
		}

		float percentStam = currCred / maxCred;
		GameObject curCredUI = GameObject.FindGameObjectWithTag ("curCredUI");
		GameObject maxCredUI = GameObject.FindGameObjectWithTag ("maxCredUI");
		GameObject creditsBarUI = GameObject.FindGameObjectWithTag ("creditsBarUI");

        if (creditsBarUI != null)
        {
            Image fillBar = creditsBarUI.GetComponent<Image>();

            //change the value on the UI
            curCredUI.GetComponent<Text>().text = currCred.ToString();
            maxCredUI.GetComponent<Text>().text = maxCred.ToString();
            fillBar.fillAmount = percentStam;

        }
    }

	public void loseCredits(float amountLost){
	}	

	public void addCredits(float amountGain){
		float currCred;
		float maxCred;

		if (PlayerPrefs.HasKey ("CurrentCredits")) {
			currCred = PlayerPrefs.GetFloat ("CurrentCredits");
		} else {
			currCred = DEFAULT_STAMINA;
			PlayerPrefs.SetFloat ("CurrentCredits",currCred);
		}

		if (PlayerPrefs.HasKey ("MaxCredits")) {
			maxCred = PlayerPrefs.GetFloat ("MaxCredits");
		} else {
			maxCred = DEFAULT_CREDITS;
		}
		currCred += amountGain;

		if (currCred > maxCred) {
			currCred = maxCred;
		}
		PlayerPrefs.SetFloat ("CurrentCredits",currCred);
		adjustCreditsBar ();
	}

	//STAMINA CONTROLS
	public void adjustStaminaBar(){
		float currStam;
		float maxStam;

		if (PlayerPrefs.HasKey ("MaxStam") && PlayerPrefs.HasKey ("CurrentStam")) {
			currStam = PlayerPrefs.GetFloat ("CurrentStam");
			maxStam = PlayerPrefs.GetFloat ("MaxStam");
		} else {
			currStam = DEFAULT_STAMINA;
			maxStam = DEFAULT_STAMINA;
			PlayerPrefs.SetFloat ("CurrentStam",currStam);
			PlayerPrefs.SetFloat ("MaxStam",maxStam);

		}

		float percentStam = currStam / maxStam;
		GameObject curStamUI = GameObject.FindGameObjectWithTag ("curStamUI");
		GameObject maxStamUI = GameObject.FindGameObjectWithTag ("maxStamUI");
		GameObject stamBarUI = GameObject.FindGameObjectWithTag ("stamBarUI");

		Image fillBar = stamBarUI.GetComponent<Image> ();

		//change the value on the UI
		curStamUI.GetComponent<Text>().text=currStam.ToString();
		maxStamUI.GetComponent<Text>().text=maxStam.ToString();
		fillBar.fillAmount = percentStam;

	}
	public void loseStamina(float amountLost){
		float currStam;

		if (PlayerPrefs.HasKey ("CurrentStam")) {
			currStam = PlayerPrefs.GetFloat ("CurrentStam");
		} else {
			currStam = DEFAULT_STAMINA;
			PlayerPrefs.SetFloat ("CurrentStam",currStam);

		}
		currStam -= amountLost;

		if (currStam < 0) {
			currStam = 0;
		}
		PlayerPrefs.SetFloat ("CurrentStam",currStam);
		adjustStaminaBar ();
	}
	public void addStamina(float amountGain){
		float currStam;
		float maxStam;

		if (PlayerPrefs.HasKey ("CurrentStam")) {
			currStam = PlayerPrefs.GetFloat ("CurrentStam");
		} else {
			currStam = DEFAULT_STAMINA;
			PlayerPrefs.SetFloat ("CurrentStam",currStam);
		}

		if (PlayerPrefs.HasKey ("MaxStam")) {
			maxStam = PlayerPrefs.GetFloat ("MaxStam");
		} else {
			maxStam = DEFAULT_STAMINA;
		}
		currStam += amountGain;

		if (currStam > maxStam) {
			currStam = maxStam;
		}
		PlayerPrefs.SetFloat ("CurrentStam",currStam);
		adjustStaminaBar ();
	}
	public void fillStaminaMax(){
		float maxStamina;
		if (PlayerPrefs.HasKey ("MaxStam")) {
			maxStamina = PlayerPrefs.GetFloat ("MaxStam");
		} else {
			maxStamina = DEFAULT_STAMINA;
		}
		PlayerPrefs.SetFloat ("CurrentStam",maxStamina);
		adjustStaminaBar ();
	}

	//SET THE NAME
	public void setDisplayName(){
		GameObject nameUI = GameObject.FindGameObjectWithTag ("nameUI");

		nameUI.GetComponent<Text> ().text = PlayerPrefs.GetString("PlayerName");
	}
	//SET THE PORTRAIT
	public void setPortrait(){
		int portraitNum;

		if (PlayerPrefs.HasKey ("AvatarSelect")) {
			portraitNum = PlayerPrefs.GetInt ("AvatarSelect");
		} else {
			portraitNum = 1;
		}
		GameObject playerPicUI = GameObject.FindGameObjectWithTag ("playerPicUI");
		playerPicUI.GetComponent<Image>().sprite = Resources.Load<Sprite> ("MC"+portraitNum);

	}

	//SET INITAL ALERT ICON ON LOAD
	public void setAlerts(){
		if (PlayerPrefs.HasKey ("isAlert")) {
			int isAlert = PlayerPrefs.GetInt("isAlert");
			if (isAlert > 0) {
				alertUI.SetActive (true);
			} else {
				alertUI.SetActive (false);
			}
		} else {
			PlayerPrefs.SetInt ("isAlert",0);
			alertUI.SetActive (false);
		}
	}

	//TURN ALERTS ON OR OFF
	public void turnOnAlert(){
		PlayerPrefs.SetInt ("isAlert",1);
		setAlerts ();
	}

	public void turnOffAlert(){
		PlayerPrefs.SetInt ("isAlert", 0);
		setAlerts ();
	}

}
