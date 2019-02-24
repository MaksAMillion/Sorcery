using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IGameManager {
    // public string equippedItem { get; private set; }

    public ManagerStatus status
    {
        get;
        private set;
    }

    private Dictionary<string, bool> _quests;

    public void Startup()
    {
        Debug.Log("Quests manager starting...");
        UpdateData(new Dictionary<string, bool>());
        status = ManagerStatus.Started;
    }
		
    public void UpdateData(Dictionary<string, bool> quests)
    {
        _quests = quests;
    }

    public Dictionary<string, bool> GetData()
    {
        return _quests;
    }

    private void DisplayQuests()
    {
        string questDisplay = "quests: ";

        foreach (KeyValuePair<string, bool> quest in _quests)
        {
            questDisplay += quest.Key + "(" + quest.Value + ") ";
        }

        Debug.Log(questDisplay);
    }

    public void AddQuest(string name)
    {
        // CollectionObjective co = new CollectionObjective(string titleVerb, int totalAmount, GameObject item, string descrip, bool bonus);
        
        if (_quests.ContainsKey(name))
        {
            _quests[name] = false;
            Debug.Log("Quest is already in quest Manager");
        }
        else
        {
            _quests[name] = false;
            Debug.Log("Quest Added");
        }

        DisplayQuests();
    }

    public bool CompleteQuest(string name)
    {
        if (_quests.ContainsKey(name))
        {
            // _quests[name]--;

            if (_quests[name] == false)
            {
                _quests[name] = true;
				_quests.Remove (name);
//				AddQuest ("{COMPLETED} " + name);
            }
        }
        else
        {
            Debug.Log("Quest is not yet Available or may not exist: " + name);
            return false;
        }

        DisplayQuests();
        return true;
    }

    public List<string> GetQuestList()
    {
        List<string> list = new List<string>(_quests.Keys);
        return list;
    }

    public int GetQuestCount(string name)
    {
        return _quests.Count;
    }

    public int GetCompletedQuests()
    {
        int questsCompleted = 0;
        foreach (KeyValuePair<string, bool> quest in _quests)
        {

            if (quest.Value == true)
            {
                questsCompleted++;
            }
        }

        return questsCompleted;
    }

    public int GetIncompleteQuests()
    {
        int incompleteQuests = 0;

        foreach (KeyValuePair<string, bool> quest in _quests)
        {
            if (quest.Value == false)
            {
                incompleteQuests++;
            }
        }

        return incompleteQuests;
    }

	public void ClearQuests(){
		_quests.Clear ();
	}
}