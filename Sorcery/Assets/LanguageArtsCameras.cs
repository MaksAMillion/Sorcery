using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LanguageArtsCameras : MonoBehaviour
{
    PlayableDirector myPB;
    public PlayableDirector myEaseOutPB;

    void Start () {
        myPB = GetComponent<PlayableDirector>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myEaseOutPB.Stop();
            myPB.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myPB.Stop();
            myEaseOutPB.Play();
        }
    }
}
