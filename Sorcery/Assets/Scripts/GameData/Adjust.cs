using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(500,100,100,30),"Reputation Up") ){
			Josh.joshInfo.reputation += 5;
		}
		if(GUI.Button(new Rect(500,140,100,30),"Reputation Down") ){
			Josh.joshInfo.reputation -= 5;
		}
		if(GUI.Button(new Rect(500,180,100,30),"Romance Up") ){
			Josh.joshInfo.romance += 5;
		}
		if(GUI.Button(new Rect(500,220,100,30),"Romance Down") ){
			Josh.joshInfo.romance -= 5;
		}
		if(GUI.Button(new Rect(500,300,100,30),"Save") ){
			Josh.joshInfo.Save ();
		}
		if(GUI.Button(new Rect(500,340,100,30),"Load") ){
			Josh.joshInfo.Load ();
		}
	}
}
