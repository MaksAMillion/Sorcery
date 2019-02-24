using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniDance : MonoBehaviour {

    public Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("dance");
        

   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().Rebind();
           
        }
    }
}
