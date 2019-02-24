using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera myCam;

	void Start () {
        myCam = Camera.main;	
	}
	
	void Update () {
        transform.LookAt(myCam.transform);
	}
}
