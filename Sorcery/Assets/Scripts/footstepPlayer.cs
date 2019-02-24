﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepPlayer : MonoBehaviour {

	[SerializeField]
	private AudioClip[] clips;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Step () {
		AudioClip clip = GetRandomClip ();
		audioSource.PlayOneShot (clip);
	}
	private AudioClip GetRandomClip (){
		return clips[UnityEngine.Random.Range(0, clips.Length)];
	}
}
