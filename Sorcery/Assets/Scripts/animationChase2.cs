using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationChase2 : MonoBehaviour
{

    //it will be in the inspector so know the distance between the npc and the player
    public Transform player;
    static Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //work out the direction form player to npc
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        //check the distance between the player and the npc position if its less than 10
        if (Vector3.Distance(player.position, this.transform.position) < 5 && angle < 30)
        {

            direction.y = 0;
            //rotate the npc towards the player position
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("isIdle", false);
            //if that direction vector of magnitude is greater than 5 
            if (direction.magnitude > 1)
            {
                this.transform.Translate(0, 0, 0.03f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isRising", false);
            }
            else
            {
                anim.SetBool("isRising", true);
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRising", false);
        }
    }
}
