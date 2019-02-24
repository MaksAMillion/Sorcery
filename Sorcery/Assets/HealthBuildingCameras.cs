using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HealthBuildingCameras : MonoBehaviour
{
    PlayableDirector myPB;
    public PlayableDirector myEaseOutPB;

    void Start()
    {
        myPB = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //myEaseOutPB.Stop();
            myPB.Stop();
            myPB.Play();
            Debug.Log("Bam");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //myPB.Stop();
            myEaseOutPB.Stop();
            myEaseOutPB.Play();
        }
    }
}
