using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragReaction : MonoBehaviour
{
    private AudioSource audioSource;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
        }
    }
}
