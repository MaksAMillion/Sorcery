using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animationWaypoint : MonoBehaviour {

    NavMeshAgent nm;

    public Transform[] Waypoints;
    public int cur_loc;
    public float stop_distance;

	// Use this for initialization
	void Start () {
        nm = GetComponent<NavMeshAgent>();
        nm.stoppingDistance = stop_distance;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, Waypoints[cur_loc].position);
        transform.LookAt(Waypoints[cur_loc]);

        int randomNumber = Random.Range(1, 4);

        if (distance <= stop_distance)
        {
            cur_loc += 1;
        }
        if(cur_loc >= Waypoints.Length)
        {
            cur_loc = 0;
        }
        if(cur_loc == 0)
        {
            nm.SetDestination(Waypoints[0].position);
        }
        if(cur_loc == 1)
        {
            nm.SetDestination(Waypoints[1].position);
        }
        if(cur_loc == 2)
        {
            nm.SetDestination(Waypoints[2].position);
        }
	}
}
