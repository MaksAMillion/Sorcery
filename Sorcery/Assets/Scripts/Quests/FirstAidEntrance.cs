using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidEntrance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		Managers.QuestLog.CompleteQuest ("Locate: Health Building Entrance");
		Messenger<string>.Broadcast(GameEvent.UI_TEXT_NOTIFICATION, "COMPLETED QUEST > Locate: First Aid Building Entrance");
		Messenger<float>.Broadcast (GameEvent.ADD_CREDITS, 20f);
		Messenger.Broadcast (GameEvent.UI_PLUS_CREDITS);

		Destroy (gameObject);
	}
}
