using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionAgent : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_STOP, OnPlayerStop);
        Messenger.AddListener(GameEvent.PLAYER_GO, OnPlayerGo);
        agent = GetComponent<NavMeshAgent>();

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_STOP, OnPlayerStop);
        Messenger.RemoveListener(GameEvent.PLAYER_GO, OnPlayerGo);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        // agent = GetComponent<NavMeshAgent>();
        // agent.autoBraking = false;
    }

    void Update()
    {
        bool shouldMove = agent.velocity.magnitude > 0.05f && agent.remainingDistance > agent.radius;

        if (shouldMove)
        {
			if (SceneManager.GetActiveScene().name == "TerrainCampus" )
            {
                // this is the animator controller parameter to enter running animation
                anim.SetFloat("Speed", 1.1f);
                agent.speed = 4.0f;

            }
			else if (SceneManager.GetActiveScene().name != "TerrainCampus")
            {
                // this is the animator controller parameter to enter the walking animation

                // anim.SetBool("Jumping", false);
                anim.SetFloat("Speed", 0.2f);
                agent.speed = 3.0f;
            }
        }
        else
        {
            // anim.SetBool("Jumping", false);
            anim.SetFloat("Speed", 0.00f);
        }
    }

    private void OnPlayerStop()
    {
        if (gameObject.tag == "Player")
        {
            agent.destination = gameObject.transform.position;
            agent.isStopped = true;
        }
    }

    private void OnPlayerGo()
    {
        if (gameObject.tag == "Player")
        {
            agent.destination = gameObject.transform.position;
            agent.isStopped = false;
        }
    }
}