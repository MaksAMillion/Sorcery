using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		float x = 20f;
		Messenger<float>.Broadcast(GameEvent.ADD_CREDITS, x);
		Destroy (gameObject);
	}
}
