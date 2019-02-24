using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClamPlaceName : MonoBehaviour
{

    public Text nameLabel;
    //this is a comment

	// Update is called once per frame
	void Update ()
    {
        //GameObject.Find("myObject").GetComponent<Camera>()
        //GameObject.FindWithTag("Main Camera").camera

        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;

        //Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        //nameLabel.transform.position = namePos;

    }
}
