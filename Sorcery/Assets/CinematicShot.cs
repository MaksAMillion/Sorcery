using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicShot : MonoBehaviour
{
    public PlayableDirector myTimeline;
    public PlayableDirector easeOutTimeline;

    private void Start()
    {
        myTimeline = GetComponent<PlayableDirector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            easeOutTimeline.Stop();
            myTimeline.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myTimeline.Stop();
            easeOutTimeline.Play();
        }
    }
}
