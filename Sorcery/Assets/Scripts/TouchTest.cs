using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

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
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget> ();
				if (target != null) {
					target.ReactToHit ();
				} else {

				}
			}
		}

	}
}
