using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject item;
	
    // Use this for initialization
	void Start () {
        IQuestObjective qb = new CollectionObjective("Gather", 10, item, "Gather 10 meat!", false);
        Debug.Log(qb.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
