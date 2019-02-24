using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Female : MonoBehaviour { 


    private bool talked = false;
    public GameObject talkPanel;

    // Use this for initialization
    void Start()
    {
        talkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (!talked)
        {
            //			talked = true;
            //			if (other.gameObject.CompareTag ("neku")) {
            //				Messenger<string>.Broadcast(GameEvent.TALK_TO, "neku");
            //			} else if (other.gameObject.CompareTag ("josh")) {
            //				Messenger<string>.Broadcast(GameEvent.TALK_TO, "joshua");
            //			}

            talkPanel.SetActive(true);
        }


    }

    void OnTriggerExit(Collider other)
    {
        talkPanel.SetActive(false);

    }

}
