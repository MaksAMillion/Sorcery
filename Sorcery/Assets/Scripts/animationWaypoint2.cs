using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animationWaypoint2 : MonoBehaviour
{

    NavMeshAgent nm;

    public Transform[] Waypoints;
    public int cur_loc;
    public float stop_distance;

    // Use this for initialization
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        nm.stoppingDistance = stop_distance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Waypoints[cur_loc].position);
        transform.LookAt(Waypoints[cur_loc]);

        int randomNumber = Random.Range(1, 5);

        if (distance <= stop_distance)
        {
            cur_loc += 1;
            //StartCoroutine(WaitForSeconds());
        }
        if (cur_loc >= Waypoints.Length)
        {
            cur_loc = 0;
        }
        if (cur_loc == 0)
        {
            nm.SetDestination(Waypoints[0].position);
            //StartCoroutine(WaitForSeconds());
        }
        if (cur_loc == 1)
        {
            nm.SetDestination(Waypoints[1].position);
            //StartCoroutine(WaitForSeconds());
        }
        if (cur_loc == 2)
        {
            //StartCoroutine(WaitForSeconds());
            nm.SetDestination(Waypoints[2].position);
          
        }
       
        if (cur_loc == 3)
        {
            //StartCoroutine(WaitForSeconds());
            nm.SetDestination(Waypoints[3].position);
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(5);
        //nm.Stop();
        //nm.isStopped = true;
        Debug.Log("Wait is over");

    }
}
