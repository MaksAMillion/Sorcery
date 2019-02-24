using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private string _filename;

    public void Startup()
    {
        Debug.Log("Data manager starting...");
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();

        // Invnetory Info
        gamestate.Add("inventory", Managers.Inventory.GetData());

        // Player Info
        gamestate.Add("stamina", Managers.Player.stamina);
        gamestate.Add("maxStamina", Managers.Player.maxStamina);
        gamestate.Add("money", Managers.Player.money);
        gamestate.Add("name", Managers.Player.playerName);

        // Mission Info
        gamestate.Add("curDay", Managers.Mission.curDay);
        gamestate.Add("maxDay", Managers.Mission.maxDay);
        gamestate.Add("curWeek", Managers.Mission.curWeek);
        gamestate.Add("maxWeek", Managers.Mission.maxWeek);

        // Quest Info
        gamestate.Add("questLog", Managers.QuestLog.GetData());

        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    public void LoadGameState()
    {
        if (!File.Exists(_filename))
        {
            Debug.Log("No saved Game");
            return;
        }

        Dictionary<string, object> gamestate;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(_filename, FileMode.Open);
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        Managers.Inventory.UpdateData((Dictionary<string, int>)gamestate["inventory"]);
        Managers.Player.UpdateData((int)gamestate["stamina"], (int)gamestate["maxStamina"], (int)gamestate["money"], (string)gamestate["playerName"]);
        // Managers.QuestLog.UpdateData((Dictionary<string, bool>)gamestate["questLog"]);
        Managers.Mission.UpdateData((int)gamestate["curDay"], (int)gamestate["maxDay"], (int)gamestate["curWeek"], (int)gamestate["maxWeek"]);

        Managers.Mission.Awaken();
    }
}