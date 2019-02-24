using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour {

    [SerializeField] private float camHeight;

	[SerializeField] private GameObject player1;
	[SerializeField] private GameObject player2;
	[SerializeField] private GameObject player3;

	private int playerAvatar;

    GameObject target;

	void Awake(){
		playerAvatar = PlayerPrefs.GetInt ("AvatarSelect");
	}

    void Start()
    {
		switch (playerAvatar) {
		case 1:
			target = player1;
			break;
		case 2:
			target = player2;
			break;
		case 3:
			target = player3;
			break;
		default:
			target = player1;
			break;
		}

    }

    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(target.transform.position.x, camHeight, target.transform.position.z);
        gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
    }
}

