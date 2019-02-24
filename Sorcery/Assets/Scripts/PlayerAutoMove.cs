using System;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour {
    // Update is called once per frame

    public float surfaceOffset = 1.5f;
    // public GameObject setTargetOn;


    void Update ()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (!Physics.Raycast(ray, out hitInfo))
        {
            return;
        }

		GameObject hitObject = hitInfo.transform.gameObject;
		ReactivePlane plane = hitObject.GetComponent<ReactivePlane> ();

		if(plane == null){
			return;
		}


        transform.position = hitInfo.point + hitInfo.normal * surfaceOffset;

        // if (setTargetOn != null)
        // {
            // setTargetOn.SendMessage("SetTarget", transform);
        // }         
    }
}
