using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

/// CSV based dialogue manager
public class DialogueManager : MonoBehaviour
{
    /// Support languages
    public enum Languages
    {
        English
    }
    /// How to display inactive answers
    public enum InactiveAnswersMode
    {
        Gray,                                                                       // Will have blured color
        Invisible                                                                   // Will not be displayed
    }

    public static DialogueManager Instance;                                         // Singleton

    public Text npcName;                                                            // Non-Player Character name
    public Text npcSay;                                                             // Non-Player Character text
    public GameObject answerPrefab;                                                 // Prefab for answer object
    public GameObject answerFolder;                                                 // Parent folder for answers
    public InactiveAnswersMode answersMode = InactiveAnswersMode.Gray;              // Inactive answers display mode
    public Color inactiveAnswerColor = Color.gray;                                  // Inactive answers color (only for InactiveAnswersMode.Gray)
    public float typingDelay = 0.02f;                                               // Delay between letters typing on screen
    private bool dialogInProgress = false;                                          // Is dialog active now
    private TextAsset dialogueCsv;                                                  // Comma-separated values dialogue descriptor file
    private IEnumerator npcTextRoutine;                                             // NPC text routine
    private bool npcIsTalking = false;                                              // Is NPC talking now
    private IEnumerator playerTextRoutine;                                          // Player text routine
    private bool playerIsTalking = false;                                           // Is player talking now
    private IEnumerator answersDisplayRoutine;                                      // Answers display routine
    private int answersCounter = 0;                                                 // Current answers counter
    private Languages language = Languages.English;                                 // Current language
    private string languageSign = "Eng";                                            // Current dialog language separator
    private string currentPage;                                                     // Current dialog page
    private string npcOnStartName, npcOnStartSay;                                   // NPC name and text before dialog start
    private string[] stuffLineName = { "NpcAnswer", "Requirements", "Effect" };     // Stuff lines will be excluded from answers
    private List<string> clickedAnswers = new List<string>();                       // List of answers were clicked during dialog
    private List<string> blockedAnswers = new List<string>();                       // List of answers manualy blocked during dialog
   

	public GameObject UI;

	//  Variables for special npcs not part of the random answers
	private Dictionary<string, bool> specialNPCs;
	private string textFileName;

    //  Variables for random NPC answers
    private Dictionary<int, string> responses;
    public GameObject inputPrefab;
    public InputField input;

    void Start()
    {
		UI.SetActive (false);

        if (    npcName == null
            ||  npcSay == null
            ||  answerPrefab == null
            ||  answerFolder == null)
        {
            Debug.LogError("Wrong default settings");
            return;
        }

    }

    /// Start dialog from CSV descriptor
    /// <param name="csvFile"> CSV dialog descriptor </param>
	public void StartDialogue(TextAsset csvFile)
    {
		textFileName = csvFile.name;

        UI.SetActive (true);
        if (specialNPCs.ContainsKey(textFileName))
        {
            answerFolder.SetActive(true);
        } else
        {
             inputPrefab.SetActive(true);   
        }


            Messenger.Broadcast(GameEvent.PLAYER_STOP);

        if (csvFile == null)
        {
            Debug.Log("Wrong input data");
            return;
        }

        if (Managers.Mission != null  && Managers.Mission.curDay != 0)
        {
            csvFile = Resources.Load("Dialogue/Week" + Managers.Mission.curWeek + "/"+ csvFile.name + "Day" + Managers.Mission.curDay) as TextAsset;
        }
        

        dialogInProgress = true;
        npcOnStartName = npcName.text;                                              // Save NPC name before dialog start
        npcOnStartSay = npcSay.text;                                                // Save NPC text before dialog start
        dialogueCsv = csvFile;
        DisplayCommonInfo();                                                        // Display info from descriptor common page
        string page;
        page = CsvParser.Instance.GetPage("Welcome", dialogueCsv);                  // Find start page named "Welcome"
        if (page != null)
        {
			
			if (specialNPCs.ContainsKey(textFileName)) {
				DisplayNpcAnswer (GetNpcAnswer (page));         
			} else {
           
                DisplayNpcAnswer("");

            }


            if (DisplayAnswers(page) == true)                                       // Display player answers from start page
            {
                currentPage = page;                                                 // Set current page
            }
            else
            {
                EndDialogue();                                                      // If no valid answers - end dialog
            }
        }
    }

    /// Check if dialog in progress
    /// <returns> true - in progress, false - not started </returns>
    public bool IsDialogInProgress()
	{
		return dialogInProgress;
	}

    /// End current dialog
    public void EndDialogue()
    {
        if (npcTextRoutine != null)                                                 // Stop dialog coroutines
        {
            StopCoroutine(npcTextRoutine);
        }
        if (playerTextRoutine != null)
        {
            StopCoroutine(playerTextRoutine);
        }
        if (answersDisplayRoutine != null)
        {
            StopCoroutine(answersDisplayRoutine);
        }
        CleanAnswers();                                                             // Remove answers from diplay
        npcName.text = npcOnStartName;                                              // Restore NPC name
        npcOnStartName = null;
        npcSay.text = npcOnStartSay;                                                // Restore NPC text
        npcOnStartSay = null;
        currentPage = null;
        clickedAnswers.Clear();                                                     // Clear list of clicked answers
        blockedAnswers.Clear();                                                     // Clear list of manualy blocked answers
        dialogInProgress = false;
        Messenger.Broadcast(GameEvent.END_TALK_SPRITES);                            // destory left, right sprites


        if (answerFolder.activeSelf)
        {
            answerFolder.SetActive(false);
        }
        if (inputPrefab.activeSelf)
        {
            inputPrefab.SetActive(false);
        }

        UI.SetActive (false);

        Messenger.Broadcast(GameEvent.PLAYER_GO);

    }

	public void submitAnswer(){

		int end = Random.Range (0, 10);
		if(end < 1){	//  30% chance to end dialogue
			EndDialogue();
		}

		int num = Random.Range (0, responses.Count);
		DisplayNpcAnswer(responses[num]);
        input.text = "";    
	}

    public void endAnswer()
    {
        EndDialogue();
    }




    /// Actions on user's answer click
    /// <param name="answer"> Clicked answer </param>
    public void OnAnswerClick(GameObject answer)
    {
        if (answer == null)
        {
            Debug.Log("Wrong input data");
            return;
        }
        string page = CsvParser.Instance.GetPage(answer.name, dialogueCsv);         // Find page with answer's name
        if (page != null)
        {
            if (clickedAnswers.Contains(answer.name) == false)
            {
                clickedAnswers.Add(answer.name);                                    // Add answer to clicked answers list
            }
            string npcAnswer = GetNpcAnswer(page);                                  // Get NPC answer from page
            if (npcAnswer != null)
            {
				if (specialNPCs.ContainsKey(textFileName)) {
					DisplayNpcAnswer(npcAnswer);                                        // Display NPC answer
				} else {
					int num = Random.Range (0, responses.Count);
					DisplayNpcAnswer(responses[num]);
				}

            }
            if (ApplyEffects(page) == true)                                         // Apply answer effects
            {
                return;                                                             // true - need to stop dialog
            }
            if (DisplayAnswers(page) == true)                                       // Try to display player answers from page
            {
                currentPage = page;                                                 // Save current page
            }
            else
            {
                if (DisplayAnswers(currentPage) == false)                           // If no active answers in new page - stay on current page
                {
                    Debug.Log("No active answers");
                    EndDialogue();                                                  // If no active answers on current page - end dialog
                }
            }
        }
    }

    /// Display data from dialog descriptor page
    private void DisplayCommonInfo()
    {
        string page;
        page = CsvParser.Instance.GetPage("Desc", dialogueCsv);                     // Get page named "Desc"
        if (page != null)
        {
            string name;
            List<string> lines = CsvParser.Instance.GetLines(page);                 // Get all lines from page
            if (lines != null)
            {
                foreach (string line in lines)
                {
                    /// Dislay NPC name
                    if (CsvParser.Instance.GetLineName(line) == "NpcName")          // Find line named "NpcName"
                    {
                        if (CsvParser.Instance.GetTextValue(out name, languageSign, line) == true)
                        {
                            if (name != null)                                       // Get text from line
                            {
                                npcName.text = name;                                // Display NPC name
                            }
                        }
                        continue;
                    }
                }
            }
        }
    }

    /// Letter by letter NPC answer display
    /// <param name="text"> NPC anwer text </param>
    /// <returns></returns>
    private IEnumerator NpcAnswerCoroutine(string text)
    {
        npcIsTalking = true;
        if (typingDelay > 0)                                                        // If display delay setted
        {
            foreach (char letter in text)
            {
                npcSay.text += letter;                                              // Add letter to answer
                yield return new WaitForSeconds(typingDelay);                       // Wait for delay
            }
        }
        else
        {
            npcSay.text += text;                                                    // If no delay needed - display whole text
        }
        npcIsTalking = false;
    }

    /// Display NPC answer
    /// <param name="text"> NPC answer text </param>
    private void DisplayNpcAnswer(string text)
    {
        if (npcSay != null)
        {
            if (npcTextRoutine != null)
            {
                StopCoroutine(npcTextRoutine);                                      // Stop current coroutine if it is
            }
            npcSay.text = "";                                                       // Clear answer text field
            npcTextRoutine = NpcAnswerCoroutine(text);
            StartCoroutine(npcTextRoutine);                                         // Start coroutine
        }
    }

    /// Get NPC answer text from page
    /// <param name="page"> Page from CSV dialogue descriptor </param>
    /// <returns> NPC answer text </returns>
    private string GetNpcAnswer(string page)
    {
        string res = null;
        if (page == null)
        {
            Debug.Log("Wrong input data");
            return res;
        }
        List<string> lines = CsvParser.Instance.GetLines(page);                     // Get all lines from page
        if (lines != null)
        {
            foreach (string line in lines)
            {
                if (CsvParser.Instance.GetLineName(line) == "NpcAnswer")            // Find line named "NpcAnswer"
                {
                    if (CsvParser.Instance.GetTextValue(out res, languageSign, line) == true)
                    {
                        break;                                                      // Get text from line
                    }
                }
            }
        }
        return res;
    }

    /// Letter by letter player answer display
    /// <param name="text"> Answer text </param>
    /// <param name="answer"> Answer display text field </param>
    /// <returns></returns>
    /*edit answers here!!!!!!!!!!!
     * 
     * 
     * 
     * 
        * 
        * 
        * 
        * 
        * 
        * 
        *
        */

   
       


    private IEnumerator PlayerAnswerCoroutine(string text, Text answer)
    {
        if (answer != null)
        {
            playerIsTalking = true;
            if (typingDelay > 0)                                                    // If display delay setted
            {
                foreach (char letter in text)
                {
                    answer.text += letter; //add letter to answer
                    answer.fontSize = 23;  // fontsize of answers is 19
                    yield return new WaitForSeconds(typingDelay);                   // Wait for delay
                
                }
            }
            else
            {
                answer.text += text;                                                // If no delay needed - display whole text
            }
            playerIsTalking = false;
        }
    }

    /// Add answer into answer folder
    /// <param name="name"> Answer name </param>
    /// <param name="answer"> Answer text </param>
    /// <param name="isActive"> true - answer is interactive, false - answer is inactive </param>
    private void AddAnswer(string name, string answer, bool isActive)
    {
        if (name == null || answer == null)
        {
            Debug.Log("Wrong input data");
            return;
        }
        GameObject newAnswer = Instantiate(answerPrefab) as GameObject;             // Clone answer prefab
        if (newAnswer != null)
        {
            newAnswer.transform.SetParent(answerFolder.transform);                  // Place it into anwer folder
            newAnswer.name = name;                                                  // Set answer name
            Text text = newAnswer.GetComponent<Text>();
            if (text != null)
            {
                answersCounter++;                                                   // Increase answers counter (cleared by ClearAnswers)
                AnswerHandler answerHandler = newAnswer.GetComponent<AnswerHandler>();
                if (isActive == false)                                              // If answer inactive
                {
                    answerHandler.active = false;                                   // Make answer not clickable
                    text.color = inactiveAnswerColor;                               // Set inactive color
                }
                else
                {
                    answerHandler.active = true;                                    // Make answer clickable
                }
                if (playerTextRoutine != null)
                {
                    StopCoroutine(playerTextRoutine);                               // Stop current coroutine if it is
                }
                text.text = " ";                       // Display answer counter
                playerTextRoutine = PlayerAnswerCoroutine(answer, text);
                StartCoroutine(playerTextRoutine);                                  // Start coroutine
            }
        }
    }

    /// Remove current answers from screen and reset answers counter
    private void CleanAnswers()
    {
        answersCounter = 0;                                                         // Reset answers counter
        if (playerTextRoutine != null)                                              // Stop current coroutine if it is
        {
            StopCoroutine(playerTextRoutine);
        }
        if (answersDisplayRoutine != null)
        {
            StopCoroutine(answersDisplayRoutine);
        }
        List<GameObject> children = new List<GameObject>();                         // Get list of current answers
        foreach (Transform child in answerFolder.transform)
        {
            children.Add(child.gameObject);
        }
        answerFolder.transform.DetachChildren();
        foreach (GameObject child in children)
        {
            Destroy(child);                                                         // Destroy current answers
        }
    }

    /// Make active answers clickable
    private void EnableAnswersRaycast()
    {
        foreach (Transform child in answerFolder.transform)                         // Get current answers list from answers folder
        {
            AnswerHandler answerHandler = child.gameObject.GetComponent<AnswerHandler>();
            if (answerHandler != null)
            {
                if (answerHandler.active)                                           // If answer setted as clickable
                {
                    Text text = child.gameObject.GetComponent<Text>();
                    if (text != null)
                    {
                        text.raycastTarget = true;                                  // Enable text raycast
                    }
                }
            }
        }
    }

    /// Get all answers line from page
    /// <param name="page"> Page from CSV dialog descriptor </param>
    /// <returns> List of player answers lines </returns>
    private List<string> GetAnswersLines(string page)
    {
        List<string> res = new List<string>();
        List <string> lines = CsvParser.Instance.GetLines(page);                    // Get all lines from page
        if (lines != null)
        {
            foreach (string line in lines)
            {
                string lineName = CsvParser.Instance.GetLineName(line);             // Get name of line
                if (lineName != null)
                {
                    bool stuff = false;
                    foreach (string stuffLine in stuffLineName)
                    {
                        if (lineName == stuffLine)                                  // Compare with stuff lines names
                        {
                            stuff = true;
                            break;
                        }
                    }
                    if (stuff == false)
                    {
                        res.Add(line);                                              // If line is not stuff - add it to list
                    }
                }
            }
        }
        return res;
    }

    /// Display player answers one by one followed
    /// <param name="answers"> List of answers line and their flags of clickable state </param>
    /// <returns></returns>
    private IEnumerator AnswersDisplayCoroutine(Dictionary<string, bool> answers)
    {
        while ((typingDelay > 0) && (npcIsTalking == true))                         // Wait while NPC stop talking
        {
            yield return new WaitForSeconds(typingDelay);
        }
        foreach (KeyValuePair<string, bool> answer in answers)
        {
            string text;
                                                                                    // Get answer text from line
            if (CsvParser.Instance.GetTextValue(out text, languageSign, answer.Key) == true)
            {
                                                                                    // Add answer to answer folder
                AddAnswer(CsvParser.Instance.GetLineName(answer.Key), text, answer.Value);
                while ((typingDelay > 0) && (playerIsTalking == true))              // Wait for previous answer stop display
                {
                    yield return new WaitForSeconds(typingDelay);
                }
            }
        }
        EnableAnswersRaycast();                                                     // Make active answers clickable
    }

    /// Display all player answers from page
    /// <param name="page"> Page from CSV dialog descriptor </param>
    /// <returns> true - have active answers, false - no active answers </returns>
    private bool DisplayAnswers(string page)
    {
        bool res = false;
        if (page == null)
        {
            Debug.Log("Wrong input data");
            return res;
        }
        List<string> lines = GetAnswersLines(page);                                 // Get list of answers from page
        if ((lines != null) && (lines.Count > 0))
        {
            bool hasActiveAnswers = false;
            Dictionary<string, bool> answersLines = new Dictionary<string, bool>();
            foreach (string line in lines)
            {
                string answerName = CsvParser.Instance.GetLineName(line);           // Get answer name
                                                                                    // Get page named "answerName"
                string answerPage = CsvParser.Instance.GetPage(answerName, dialogueCsv);
                                                                                    // Check for active answer requirements
                bool isActive = CheckAnswerRequirements(answerPage) && !IsAnswerBlocked(answerName);
                if ((answersMode == InactiveAnswersMode.Invisible) && isActive == false)
                {
                    continue;                                                       // If no need to display inactive answers - skip it
                }
                if (isActive)
                {
                    hasActiveAnswers = true;                                        // If at least one active answer - save it
                }
                answersLines.Add(line, isActive);                                   // Add answer to display list
            }
            if ((answersLines.Count > 0) && (hasActiveAnswers == true))             // If have active answers
            {
                res = true;
                if (answersDisplayRoutine != null)
                {
                    StopCoroutine(answersDisplayRoutine);                           // Stop current coroutine if it is
                }
                answersDisplayRoutine = AnswersDisplayCoroutine(answersLines);
                CleanAnswers();                                                     // Remove current answers
                StartCoroutine(answersDisplayRoutine);                              // Start coroutine
            }
        }
        return res;
    }

    /// Check if answer was clicked before in current dialog
    /// <param name="answerName"> Answer name </param>
    /// <returns> true - was clicked before, false - was not clicked </returns>
    private bool WasAnswerClickedBefore(string answerName)
    {
        return clickedAnswers.Contains(answerName);
    }

    /// Manualy block answer and make it inactive untill dialog end
    /// <param name="answerName"> Answer name </param>
    /// <param name="condition"> true - block answer, false - remove from blocked </param>
    private void BlockAnswer(string answerName, bool condition)
    {
        if (answerName != null)
        {
            if ((blockedAnswers.Contains(answerName) == false) && (condition == true))
            {
                blockedAnswers.Add(answerName);
            }
            else if ((blockedAnswers.Contains(answerName) == true) && (condition == false))
            {
                blockedAnswers.Remove(answerName);
            }
        }
    }

    /// Check if answer in blocked list
    /// <param name="answerName"> Answer name </param>
    /// <returns> true - answer blocked, false - answer not blocked manualy </returns>
    private bool IsAnswerBlocked(string answerName)
    {
        return blockedAnswers.Contains(answerName);
    }


    /// Check if answer meet requirements described in CSV dialog page
    /// <param name="page"> Page from CSV dialog descriptor with the same name as answer </param>
    /// <returns> true - meet requirements (active), false - fail requirements (inactive) </returns>
    private bool CheckAnswerRequirements(string page)
    {
        if (page == null)
        {
            Debug.Log("Wrong input data");
            return false;
        }
        bool res = true;
        List<string> reqLines = CsvParser.Instance.GetLines("Requirements", page);  // Get all lines named "Requirements" from page
        if (reqLines != null)
        {
            if (reqLines.Count > 0)
            {
                res = false;
            }
            /// Lines requirements will be united with logical OR
            foreach (string line in reqLines)
            {
                bool localRes = true;
                // Split line by value named "Req"
                List<string> reqs = CsvParser.Instance.SplitLineByValue("Req", line);
                if (reqs != null)
                {
                    /// Field requirements (in one line) will be united with logical AND
                    foreach (string req in reqs)
                    {
                        string data;
                        // Get requirement data from CSV
                        if (CsvParser.Instance.GetTextValue(out data, "Req", req) == true)
                        {
                            /// Example: gold requirement
                            if (data == "Gold")
                            {
                                int num;
                                if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                                {
                                    if (InventoryControl.Instance.GetGold() < num)
                                    {
                                        localRes = false;
                                        break;
                                    }
                                }
                                continue;
                            }
                            /// Example: one timed answer
                            else if (data == "OneOff")
                            {
                                if (WasAnswerClickedBefore(CsvParser.Instance.GetPageName(page)) == true)
                                {
                                    localRes = false;
                                    break;
                                }
                                continue;
                            }
                            /// Example: free item slot in inventory
                            else if (data == "FreeItemCell")
                            {
                                if (InventoryControl.Instance.IsItemCellFree() == false)
                                {
                                    localRes = false;
                                    break;
                                }
                                continue;
                            }
                            /// Example: player has no money
                            else if (data == "NoGold")
                            {
                                if (InventoryControl.Instance.GetGold() > 0)
                                {
                                    localRes = false;
                                    break;
                                }
                                continue;
                            }
                            /// Template for new requirement
                            else if (data == "MyOwnRequirement")
                            {
                                ///
                                /// Conditions
                                /// 
                                /// if conditions fails
                                /// localRes must be setted false
                                /// and break added
                                ///
                                continue;
                            }
                            else
                            {
                                Debug.Log("Unknown requirement");
                                continue;
                            }
                        }
                    }
                }
                res = res || localRes;                                              // Requirements lines united by OR and fields united by AND
            }
        }
        return res;
    }

    /// Aply effects described in clicked answer's page
    /// <param name="page"> Page from CSV dialog descriptor </param>
    /// <returns></returns>
    private bool ApplyEffects(string page)
    {
        if (page == null)
        {
            Debug.Log("Wrong input data");
            return false;
        }
        List<string> lines = CsvParser.Instance.GetLines(page);                     // Get all lines from page
        if (lines != null)
        {
            foreach (string line in lines)
            {
                string lineName = CsvParser.Instance.GetLineName(line);             // Get line name
                if (lineName == "Effect")                                           // Find lines named "Effect"
                {
                    string data;
                    // Get effect data
                    if (CsvParser.Instance.GetTextValue(out data, lineName, line) == true)
                    {
                        /// Example: end dialog
                        if (data == "Exit")
                        {
                            EndDialogue();
                            InventoryControl.Instance.ResetInventory();
                            return true;
                        }
                        /// Example: add or remove gold
                        else if (data == "Gold")
                        {
                            int num;
                            if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                            {
                                InventoryControl.Instance.AddGold(num);
                            }
                            continue;
                        }
                        /// Example: add food
                        else if (data == "Food")
                        {
                            int num;
                            if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                            {
                                InventoryControl.Instance.AddFood(num);
                            }
                            continue;
                        }
                        /// Example: add item to inventory
                        else if (data == "Item")
                        {
                            if (CsvParser.Instance.GetTextValue(out data, data, line) == true)
                            {
                                if (data == "Add")
                                {
                                    if (CsvParser.Instance.GetTextValue(out data, data, line) == true)
                                    {
                                        InventoryControl.Instance.AddItem(data);
                                    }
                                }
                            }
                            continue;
                        }
                        /// Example: manualy block answer and make it inactive
                        else if (data == "BlockIt")
                        {
                            BlockAnswer(CsvParser.Instance.GetPageName(page), true);
                            continue;
                        }

                        else if (data == "PayMoney")
                        {
                            int num;
                            if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                            {
                                Messenger<int>.Broadcast(GameEvent.PAY_MONEY, num);
                                Messenger.Broadcast(GameEvent.UI_MINUS_MONEY);

                            }
                        }

                        else if (data == "SubStamina")
                        {
                            float num;
                            if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                            {
                                Messenger<float>.Broadcast(GameEvent.LOSE_STAMINA, num);
                                Messenger.Broadcast(GameEvent.UI_MINUS_STAMINA);
                            }
                        }

                        else if (data == "EarnMoney")
                        {
                            float num;
                            if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                            {
                                Messenger<float>.Broadcast(GameEvent.EARN_MONEY, num);
                                Messenger.Broadcast(GameEvent.UI_PLUS_MONEY);
                            }
                        }


                        else if (data == "WeekOne")
                        {

                        }


                        /// Template for new effect
                        else if (data == "AddStamina")
                        {
                            float num;
                            if (CsvParser.Instance.GetNumValue(out num, data, line) == true)
                            {
                                Messenger<float>.Broadcast(GameEvent.ADD_STAMINA, num);
                                Messenger.Broadcast(GameEvent.UI_PLUS_STAMINA);
                            }
                            continue;
                        }
                        else if (data == "NewQuest")
                        {
                            string questName;
                            if (CsvParser.Instance.GetTextValue(out questName, data, line) == true)
                            {
                                // Messenger<float>.Broadcast(GameEvent.ADD_STAMINA, num);

                                Debug.Log("Quest Name: " + questName);
                                Managers.QuestLog.AddQuest(questName);

                            }
                        }
                        else if (data == "CompleteLunchQuest")
                        {
                            string questName;
                            if (CsvParser.Instance.GetTextValue(out questName, data, line) == true)
                            {
								if (Managers.QuestLog.CompleteQuest ("Buy: Cafeteria Lunch")) {
									Managers.QuestLog.CompleteQuest("Buy: Cafeteria Lunch");
									Messenger<string>.Broadcast(GameEvent.UI_TEXT_NOTIFICATION, "COMPLETED QUEST > Buy: Lunch");
									Messenger<float>.Broadcast (GameEvent.ADD_CREDITS, 20f);
									Messenger.Broadcast (GameEvent.UI_PLUS_CREDITS);
								}
									
                            }
                        }
                        else if (data == "CompleteVisitProfessorQuest")
                        {
                            string questName;
                            if (CsvParser.Instance.GetTextValue(out questName, data, line) == true)
                            {
								if (Managers.QuestLog.CompleteQuest ("Visit: Art Professor")) {
									Managers.QuestLog.CompleteQuest("Visit: Art Professor");
									Messenger<string>.Broadcast(GameEvent.UI_TEXT_NOTIFICATION, "COMPLETED QUEST > Visit: Art Professor");
									Messenger<float>.Broadcast (GameEvent.ADD_CREDITS, 20f);
									Messenger.Broadcast (GameEvent.UI_PLUS_CREDITS);

								}

                            }
                        }
                        else
                        {
                            Debug.Log("Unknown effect");
                            continue;
                        }
                    }
                }
            }
        }
        return false;
    }

	void Awake()
	{
		Instance = this;                                                            // Make singleton Instance
		specialNPCs = new Dictionary<string, bool>();
		responses = new Dictionary<int, string> ();

		specialNPCs.Add ("Esther(loveinterest)", true);
        specialNPCs.Add("Anna(cashier)", true);
        specialNPCs.Add("Janine(cashier)", true);
        specialNPCs.Add("Carol(cashier)", true);
        specialNPCs.Add("Samantha(cashier)", true);
        specialNPCs.Add("Selena(art)", true);
        specialNPCs.Add("Helen(cashier)", true);
        specialNPCs.Add("Josephine(market)", true);
        specialNPCs.Add("Won(market)", true);
        specialNPCs.Add("Blake(prof)", true);
        specialNPCs.Add("Parker(cashier)", true);

        responses.Add (0, "Sorry. I just want everything to be perfect.");
		responses.Add (1, "I’m a little nervous. I can barely move in this outfit.");
		responses.Add (2, "This is off the record, right?");
		responses.Add (3, "You told me everything was going to be fixed by now.");
		responses.Add (4, "Do you realize what I did for you?");
		responses.Add (5, "Maybe we should talk to your father?");
		responses.Add (6, "I’m not afraid of people who are guilty.");
		responses.Add (7, "Quite frankly, I can’t think of anyone more qualified than you.");
		responses.Add (8, "I like the job I have now.");
		responses.Add (9, "Why aren’t you dancing?");
		responses.Add (10, "I’m going to get some air.");
		responses.Add (11, "I can’t dance to this music. ");
		responses.Add (12, "When do the mariachis play?");
		responses.Add (13, "You want company?");
		responses.Add (14, "What a beautiful morning! A great day to be alive.");
		responses.Add (15, "I don’t even know why I’m here –");
		responses.Add (16, "It wouldn’t be a loss.");
		responses.Add (17, "Turn around. No visitors.");
		responses.Add (18, "Give me your phone.");
		responses.Add (19, "Are you planning on killing everyone?");
		responses.Add (20, "I’m so sorry. It was just an automatic response.");
		responses.Add (21, "They had a lot of leverage.");
		responses.Add (22, "It is polite, when addressed to make eye contact with the person addressing you.");
		responses.Add (23, "You did not approach me, I approached you.");
		responses.Add (24, "Maybe you have a point.");
		responses.Add (25, "It is not the same. (sighs) You would not understand.");
		responses.Add (26, "Is your father dead?");
		responses.Add (27, "Can my friend come?");
		responses.Add (28, "Well, just look at her. She’s filthy.");
		responses.Add (29, "Is there a law or mandate hat forbids her or not?");
		responses.Add (30, "Watch this.");
		responses.Add (31, "The courtyard, you say?");
		responses.Add (32, "That is very kind of you to say.");
		responses.Add (33, "You have a life so many children can only dream of.");
		responses.Add (34, "You what?");
		responses.Add (35, "It seemed like a nice thing to do.");
		responses.Add (36, "Mind your mother, but don’t stop being friendly.");
		responses.Add (37, "When do you leave?");
		responses.Add (38, "Your mother is a nasty woman.");
		responses.Add (39, "Yes, not that anyone would ever say that to her face.");
		responses.Add (40, "Oh, come on, you’ll meet loads of kids and some of them will be nice. It’s going to be okay.");
		responses.Add (41, "It’s not fair, who is he to make that kind of decision over your  life?");
		responses.Add (42, "Let’s break some stuff.");
		responses.Add (43, "You won’t be completely alone.");
		responses.Add (44, "Your coffee is getting cold.");
		responses.Add (45, "Well what are you standing there for? Go tell her.");
		responses.Add (46, "Wow, you must be really upset.");
		responses.Add (47, "We have been far too easy on you.");
		responses.Add (48, "You need to be taught some respect.");
		responses.Add (49, "It is for your own good.");
		responses.Add (50, "There’s nothing you can do to come between us.");
		responses.Add (51, "I can and I have done worse for less.");
		responses.Add (52, "You look so grown up.");
		responses.Add (53, "It was rough.");
		responses.Add (54, "Will you tell her the truth?");
		responses.Add (55, "The other kids were so mean and rough. They called me soft.");
		responses.Add (56, "You have always been given everything. ");
		responses.Add (57, "Sometimes you need to struggle and fight for what you want.");
		responses.Add (58, "But you must realize they let you win every time.");
		responses.Add (59, "How dare you speak to me like this?");
		responses.Add (60, "Who made you think that you could speak to me in such a manner?");
		responses.Add (61, "You treated me like a person. ");
		responses.Add (62, "Going away to school sure changed you.");
		responses.Add (63, "As much as anyone can speak with her, lucky to get a bloody word in.");
		responses.Add (64, "She told me the things she heard from the school were disappointing.");
		responses.Add (65, "You came here, hoping I would coddle you?");
		responses.Add (66, "You are one of the sweetest people I have ever met. ");
		responses.Add (67, "Never lose that.");
		responses.Add (68, "I will not be a flatterer, telling you what you want to hear. That’s not what friends do.");
		responses.Add (69, "You are not my best friend, you are not my friend.");
		responses.Add (70, "Is that the only reason you asked me to join you?");
		responses.Add (71, "I needed a break from sharing.");
		responses.Add (72, "You’re a pig.");
		responses.Add (73, "I know, I was too harsh, I am sorry for that.");
		responses.Add (74, "I’m scared.");
		responses.Add (75, "You are overstepping your boundaries.");
		responses.Add (76, "I'm not up for ransom.");
		responses.Add (77, "In its function, the power to punish is not essentially different from that of curing or educating.");
		responses.Add (78, "Freedom of conscience entails more dangers than authority and despotism.");
		responses.Add (79, "Radical simply means \"grasping things at the root.");
		responses.Add (80, "We have to talk about liberating minds as well as liberating society.");
		responses.Add (81, "To understand how any society functions you must understand the relationship between men and women.");
		responses.Add (82, "Everyone will be famous for 15 minutes.");
		responses.Add (83, "Art is what you can get away with.");
		responses.Add (84, "They always say time changes things, but you actually have to change them yourself.");
		responses.Add (85, "The willingness to accept responsibility for one's own life is the source whereself-respect springs.");
		responses.Add (86, "I write entirely to find out what I'm thinking, what I'm looking at, what I see and what it means.");
		responses.Add (87, "Life changes fast. Life changes in the instant. You sit down to dinner and life as you know it ends.");
		responses.Add (88, "The true revolutionary is guided by love. It is impossible to think of a genuine revolutionary lacking this.");
		responses.Add (89, "History repeats itself, first as tragedy, second as farce.");
		responses.Add (90, "Religion is the sigh of the oppressed creature just as it is the spirit of a spiritless situation.");
		responses.Add (91, "My mother told me to be a lady. And for her, that meant be your own person, be independent.");
		responses.Add (92, "Women will only have true equality when men also share the responsibility of bringing up the next generation.");
		responses.Add (93, "The state controlling a woman would mean denying her full autonomy and full equality.");
		responses.Add (94, "Given the choice of anyone in the world, whom would you want as a dinner guest?");
		responses.Add (95, "Would you like to be famous? In what way?");
		responses.Add (96, "Before making a telephone call, do you ever rehearse what you are going to say? Why?");
		responses.Add (97, "What would constitute a “perfect” day for you?");
		responses.Add (98, "When did you last sing to yourself? To someone else?");
		responses.Add (99, "If lived until 9O, would you retain the mind or body of a 3O-year-old for the last 6O years of your life");
		responses.Add (100, "Do you have a secret hunch about how you will die?");
		responses.Add (101, "For what in your life do you feel most grateful?");
		responses.Add (102, "If you could change anything about the way you were raised, what would it be?");
		responses.Add (103, "If you could wake up tomorrow having gained any one quality or ability, what would it be?");
		responses.Add (104, "Would you want to know the truth about yourself, your life, or the future if you could?");
		responses.Add (105, "Is there something that you’ve dreamed of doing for a long time? Why haven’t you done it?");
		responses.Add (106, "What is the greatest accomplishment of your life?");
		responses.Add (107, "What do you value most in a friendship?");
		responses.Add (108, "What is your most treasured memory?");
		responses.Add (109, "What is your most terrible memory?");



	}
}
