using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animationRandomMovement : MonoBehaviour {

    NavMeshAgent nva;

    NavMeshPath path;

    public float timer;

    bool inCoroutine;

    bool validPath;

    Vector3 target;

    void Start()
    {
        nva = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    void Update()
    {
        if (!inCoroutine)
            StartCoroutine(DoSomething());
    }

    Vector3 getNewRandomPostion()
    {
        float x = Random.Range(-37, 15);
        float z = Random.Range(-114, 17);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoroutine = true;
        yield return new WaitForSeconds(timer);
        GetNewPath();
        validPath = nva.CalculatePath(target, path);
        if (!validPath) Debug.Log("Found Invalid path");

        while (!validPath)
        {
            yield return new WaitForSeconds(1f);
            GetNewPath();
            validPath = nva.CalculatePath(target, path);
        }
        inCoroutine = false;
    }

    void GetNewPath()
    {
        target = getNewRandomPostion();
        nva.SetDestination(target);
    }
}
