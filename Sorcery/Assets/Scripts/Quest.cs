using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPickup : MonoBehaviour
{
    public string questName;
    private void OnTriggerEnter(Collider other)
    {
        // This is a sample code to  add a quest, call this from
        // the players interaction with the NPCs, when a new quest is found
        if (other.gameObject.tag == "Player")
        {
            Managers.QuestLog.AddQuest(questName);
        }
        
    }
}
