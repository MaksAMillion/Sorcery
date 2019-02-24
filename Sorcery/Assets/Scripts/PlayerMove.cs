using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	Vector3 touchSpot;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 1){

			Vector3 point = Input.GetTouch(0).position;
			ray = Camera.main.ScreenPointToRay (point);

			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				GameObject hitObject = hit.transform.gameObject;
				ReactivePlane plane = hitObject.GetComponent<ReactivePlane> ();
				if (plane != null) {
					touchSpot = hit.point;
				} 
			}
		}
		transform.position = Vector3.MoveTowards(transform.position, touchSpot, 10.0f*Time.deltaTime);

	}
}
