using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLocation : MonoBehaviour {

	public bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter() {
		triggered = true;
	}

	void OnTriggerExit() {
		triggered = false;
	}

	void OnGUI() {
		if (triggered) {
			if(GUI.Button(new Rect(500,100,100,30),"Plant Tree") ){
				Valeria.valeriaInfo.treeCount += 1;
				Destroy (gameObject);
			}
		}
	}

}
