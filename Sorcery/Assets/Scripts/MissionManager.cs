using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int curDay { get; private set; }
    public int maxDay { get; private set; }
    public int curWeek { get; private set; }
    public int maxWeek { get; private set; }

    public void Startup()
    {
        Debug.Log("Mission manager starting...");
        UpdateData(0, 3, 1, 2);
        status = ManagerStatus.Started;
    }

    public void UpdateData(int curDay, int maxDay, int curWeek, int maxWeek)
    {
        UpdateDay(curDay, maxDay);
        UpdateWeek(curWeek, maxWeek);
    }

    public void ReachObjective()
    {
        // could have logic to handle multiple objectives
        Debug.Log("Messenger commented out in MissionManager.cs ~ in ReachObjective method");
        // Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    public void GoToNextDay()
    {
        if (curDay < maxDay)
        {
            curDay++;
            string name = "Week " + curWeek + ", Day " + curDay;
            Debug.Log("Loading " + name);

            // Perhaps if there is a a dorm
            // Application.LoadLevel(name);

            // temp for now: change to desired scene to load when waking up
            SceneManager.LoadScene("Dorm");
        }
        else if (curWeek < maxWeek)
        {
            Debug.Log("New Week, Welcome Back!!");
            curDay = 1;
            curWeek++;

            string name = "Week " + curWeek + ", Day " + curDay;
            Debug.Log("Loading " + name);

            // temp for now: change to desired scene to load when waking up
            SceneManager.LoadScene("Dorm");

            // Messenger.Broadcast(GameEvent.GAME_COMPLETE);
            // bettersuited as
            // Messenger.Broadcast(GameEvent.WEEK_COMPLETE);
        }
        else
        {
            Debug.Log("Semester Over");
            // Messenger.Broadcast(GameEvent.SEMESTER_COMPLETE);
        }
    }

    private void UpdateDay(int curDay, int maxDay)
    {
        this.curDay = curDay;
        this.maxDay = maxDay;
    }

    private void UpdateWeek(int curWeek, int maxWeek)
    {
        this.curWeek = curWeek;
        this.maxWeek = maxWeek;
    }

    public void Awaken()
    {
        Debug.Log("Waking up");

        SceneManager.LoadScene("MainMenu");
    }
}