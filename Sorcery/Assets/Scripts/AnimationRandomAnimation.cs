using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomAnimation : MonoBehaviour {

    static Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //anim.Play("Walking");
       animationRandom();
	}

    void animationRandom()
    {
        int randomNumber = Random.Range(1, 3);
        anim.SetInteger("Idling" , randomNumber);
        anim.SetInteger("Walking", randomNumber);
    }
}
