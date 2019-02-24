using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class MoveToClickPoint : MonoBehaviour
{
    public string entranceName;
    NavMeshAgent agent;
    AudioSource audioSource;
    
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                GameObject collidedObject = hit.collider.gameObject;

                if (collidedObject.GetComponent<ReactivePlane>() != null)
                {
                    float distancePoint = Vector3.Distance(transform.position, hit.point);

                    agent.enabled = true;

                    if (SceneManager.GetActiveScene().name == "ArtRoom" || SceneManager.GetActiveScene().name == "ArtHallway" || SceneManager.GetActiveScene().name == "ScienceHallway" || SceneManager.GetActiveScene().name == "Dorm" || SceneManager.GetActiveScene().name == "InsideCafeteriaBuilding" ? distancePoint > agent.radius + 0.06f : distancePoint > agent.radius + 0.01f)
                    {
                        agent.destination = hit.point;

                        audioSource.PlayOneShot(Resources.Load("SFX/Bubble") as AudioClip);
                    }
                }
                else if (collidedObject.tag == "collectible")
                {
                    string name = "";

                    if (collidedObject.GetComponent<CollectibleItem>() != null)
                    {
                        name = collidedObject.GetComponent<CollectibleItem>().GetItemName();
                    }

                    Debug.Log("Item collected: " + name);
                    if (Managers.Inventory != null)
                    {
                        Managers.Inventory.AddItem(name);

                    }

                    audioSource.PlayOneShot(Resources.Load("SFX/collected") as AudioClip);
                    Destroy(collidedObject);
                }
            }
        }
	}
}
