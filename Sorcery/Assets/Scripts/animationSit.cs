using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSit : MonoBehaviour {

    public GameObject character;
    Animator anim;
    bool isWalkingTowards = false;
    bool sittingOn = false;

    /*void OnMouseDown()
    {
        if (!sittingOn)
        {
            anim.SetTrigger("isWalking");
            isWalkingTowards = true;
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!sittingOn)
            {
                anim.SetTrigger("isWalking");
                isWalkingTowards = true;
            }
        }
    }*/

    public float waitTime = 5f;
    float timer;

    // Use this for initialization
    void Start () {
        anim = character.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            if (!sittingOn)
            {
                anim.SetTrigger("isWalking");
                isWalkingTowards = true;
            }
        }

        if (isWalkingTowards)
        {
            Vector3 targetDir;
            targetDir = new Vector3(transform.position.x - character.transform.position.x, 0f,
                transform.position.z - character.transform.position.z);

            Quaternion rot = Quaternion.LookRotation(targetDir);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);
            character.transform.Translate(Vector3.forward * 0.03f);

            if(Vector3.Distance(character.transform.position, this.transform.position) < 0.6)
            {
                anim.SetTrigger("isSitting");
                character.transform.rotation = this.transform.rotation;

                isWalkingTowards = false;
                sittingOn = true;
            }
        }
	}
}
