using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int health;

    private int _currentRoomId = 1;
    private Animator _animator;
    // private PlayerController _playerController;

    void Start () {
        _animator = GetComponent<Animator>();
        // _playerController = GetComponent<PlayerController>();

        health = 10;
	}
	
	void Update () {
		
	}

    public void Hurt(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("My Health is: " + health);
        }
    }

    private void OnSubmitAnswer()
    {
        if (_currentRoomId == 1)
        {
            Debug.Log("on trigger enter on : PlayerCharacter");
        }
    }
}
