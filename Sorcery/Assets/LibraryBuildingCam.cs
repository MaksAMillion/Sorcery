using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LibraryBuildingCam : MonoBehaviour {
    public Camera myMainCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collisison is woorking as intended");
            PlayableDirector timeline = myMainCamera.GetComponent<PlayableDirector>();

            timeline.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collisison is woorking as intended");
            PlayableDirector timeline = myMainCamera.GetComponent<PlayableDirector>();

            timeline.Stop();
        }
    }
}
