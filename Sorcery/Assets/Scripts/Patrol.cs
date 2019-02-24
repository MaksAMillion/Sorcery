using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{   [SerializeField]
    private Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
        
    }

    // Update is called once per frame
    void Update () {
        if (agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
	}
}
