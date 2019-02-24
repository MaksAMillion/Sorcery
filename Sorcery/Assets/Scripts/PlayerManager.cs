using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int stamina { get; private set; }
    public int maxStamina { get; private set; }
    public int money { get; private set; }
    public string playerName { get; private set; }

    public bool sleeping = false;
    /*
    public string currentLocation { get; private set; }
    public string previousLocation { get; private set; }
    */
    private string _previousRoomName = "";
    private string _enteringRoomName = "";
    public bool useDoor = false;

    public void Startup()
    {
        Debug.Log("Player manager starting...");
        stamina = 100;
        maxStamina = 100;
        money = 1000;

        status = ManagerStatus.Started;
	}

    public void UpdateData(int stamina, int maxStamina, int money, string playerName)
    {
        this.stamina = stamina;
        this.maxStamina = maxStamina;
        this.money = money;
        this.playerName = playerName;
    }

    public void ChangeStamina(int value)
    {
        stamina += value;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }

        if (stamina == 0)
        {
            // broadcast that the player must sleep to UI
            // Messenger.Broadcast(GameEvent.STAMINA_DRAINED);
        }

        if (value < 0)
        {
            Messenger.Broadcast(GameEvent.LOSE_STAMINA);
        }
        else if (value > 0)
        {
            Messenger.Broadcast(GameEvent.ADD_STAMINA);
        }
    }

    public void ChangeMoney(int value)
    {
        money += value;
        
        if (money < 0)
        {
            money = 0;
        }

        if (money == 0)
        {
            // broadcast that the player must sleep to UI
            // Messenger.Broadcast(GameEvent.NO_MONEY);
        }

        if (value < 0)
        {
            Messenger.Broadcast(GameEvent.PAY_MONEY);
        }
        else if (value > 0)
        {
            Messenger.Broadcast(GameEvent.EARN_MONEY);
        }
    }
    
    public void PlayerAwake()
    {
        UpdateData(maxStamina, maxStamina, money, playerName);
    }


    /*
    public void ChangePreviousLocation(string previousLocation)
    {
        this.previousLocation = previousLocation;
    }

    public void ChangeCurrentLocation(string currentLocation)
    {
        this.currentLocation = currentLocation;
    }
    */
    public void SetPreviousRoomName(string roomName)
    {
        _previousRoomName = roomName;
    }

    public string GetPreviousRoomName()
    {
        return _previousRoomName;
    }

    public void SetEnteringRoomName(string roomName)
    {
        _enteringRoomName = roomName;
    }

    public string GetEnteringRoomName()
    {
        return _enteringRoomName;
    }
}