using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneController : MonoBehaviour
{
    public Text InGameDate;
	public Text questWeek;

    private string _week = "Week";
    private string _day = "Day";

	private GameObject currentScreen;
	public GameObject iconsScreen;

    public GameObject[] questButtons;

    void Awake()
    {
        Messenger.AddListener(GameEvent.QUEST_ADD, AddQuest);

        if (Managers.Mission == null)
        {
            return;
        }

        if (Managers.Mission.curWeek == 1 && Managers.Mission.curDay == 0)
        {
            InGameDate.text = "Tutorial";
        }
        else
        {
            InGameDate.text = _week + " " + Managers.Mission.curWeek + ", " + _day + " " + Managers.Mission.curDay;
			questWeek.text = "Quests: " + _week + " " + Managers.Mission.curWeek;
        }

        List<string> myList = Managers.QuestLog.GetQuestList();

        int counter = 0;
        if (myList.Count > 0)
        {
            foreach (string quest in myList)
            {
                GameObject reference = questButtons[counter];
                reference.SetActive(true);
                reference.GetComponentInChildren<Text>().text = quest;
                counter++;
            }
        }
        else
        {
            questButtons[0].SetActive(true);
            
            questButtons[0].GetComponentInChildren<Text>().text = "There is no quests to display!";
        }
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.QUEST_ADD, AddQuest);    
    }
		

    public void AddQuest()
    {
        List<string> myList = Managers.QuestLog.GetQuestList();

        int counter = 0;
        if (myList.Count > 0)
        {
            foreach (string quest in myList)
            {
                GameObject reference = questButtons[counter];
                reference.SetActive(true);
                reference.GetComponentInChildren<Text>().text = quest;
                counter++;
            }
        }
        else
        {
            questButtons[0].SetActive(true);

            questButtons[0].GetComponentInChildren<Text>().text = "There is no quests to display!";
        }
    }

    public void switchToScreen(GameObject screen){
		screen.SetActive (true);
		currentScreen = screen;
		iconsScreen.SetActive (false);
	}
	public void switchBackToIcons(){
		currentScreen.SetActive (false);
		iconsScreen.SetActive(true);

	}
    public void OnEnable()
    {
        if (Managers.QuestLog != null)
        {
            List<string> myList = Managers.QuestLog.GetQuestList();

            int counter = 0;
            if (myList.Count > 0)
            {
                foreach (string quest in myList)
                {
                    GameObject reference = questButtons[counter];
                    reference.SetActive(true);
                    reference.GetComponentInChildren<Text>().text = quest;
                    counter++;
                }
            }
            else
            {
                questButtons[0].SetActive(true);

                questButtons[0].GetComponentInChildren<Text>().text = "There is no quests to display!";
            }
        }
    }

	public void RefreshQuests(){

        if (Managers.QuestLog == null)
        {
            return;
        }

		List<string> myList = Managers.QuestLog.GetQuestList();

		int counter = 0;
		if (myList.Count > 0)
		{
			foreach (string quest in myList)
			{
				GameObject reference = questButtons[counter];
				reference.SetActive(true);
				reference.GetComponentInChildren<Text>().text = quest;
				counter++;
			}
		}
		else
		{
			questButtons[0].SetActive(true);

			questButtons[0].GetComponentInChildren<Text>().text = "There is no quests to display!";
		}
	}

    public void GoToSleep()
    {
            Messenger<int>.Broadcast(GameEvent.SLEEP, 6);
    }

//   Testing functions for completing quests

	public void completeQuest(){
//		Managers.QuestLog.CompleteQuest ("Visit: Art Professor");
//		Managers.QuestLog.CompleteQuest ("Locate: First Aid Building Entrance");
//		Managers.QuestLog.CompleteQuest ("Buy: Cafeteria Lunch");
	}

}
