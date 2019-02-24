using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour {

    public string text;
    public bool display = false;
    //public float speed = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider co)
    {
        if (co.transform.name == "Player1")
        {
            display = true;
            //Vector3 direction = (transform.position - co.transform.position).normalized;
            //co.GetComponent<Rigidbody>().AddForce(direction * speed);
        }
    }

    void OnTriggerExit(Collider ce)
    {
        if (ce.transform.name == "Player1")
        {
            display = false;
        }
    }

    void OnGUI()
    {
        if (display == true)
        {
            GUI.Box(new Rect(0, 200, Screen.width, Screen.height - 200), text);
        }
    }

}
