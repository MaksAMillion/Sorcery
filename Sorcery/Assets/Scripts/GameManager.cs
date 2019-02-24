using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameObject player1;
	[SerializeField] private GameObject player2;
	[SerializeField] private GameObject player3;

    public CinemachineVirtualCamera[] myCams;
    public GameObject cafeteriaSpawnPoint;
    public GameObject TerrainCafeteriaSpawnPoint;
    public GameObject hallwaySpawnPoint;
    public GameObject hallwayArtRoomSpawnPoint;
    public GameObject TerrainArtHallwaySpawnPoint;
    public GameObject ArtRoomSpawnPoint;
    public GameObject TerrainDormSpawnPoint;
    public GameObject DormSpawnPoint;
    public GameObject TerrainScienceHallwaySpawnPoint;

    private int playerAvatar;

	void Awake(){
		playerAvatar = PlayerPrefs.GetInt ("AvatarSelect");
		Debug.Log (playerAvatar.ToString());
	
        switch (playerAvatar) {
		    case 1:
			    player1.SetActive (true);

                /*
                foreach (CinemachineVirtualCamera OneCam in myCams)
                {
                    if (OneCam.gameObject.tag == "Building" || OneCam.gameObject.tag == "ArtRoomCam")
                    {
                        continue;
                    }
                    else
                    {
                        Transform objectToFollow = null;
                        foreach (Transform playerChild in player1.transform)
                        {
                            if (playerChild.gameObject.CompareTag("CharacterTransform"))
                            {
                                objectToFollow = playerChild;
                            }
                        }
                        
                        OneCam.Follow = objectToFollow;
                        OneCam.LookAt = objectToFollow;
                    }
                }
                */

                if (player1.GetComponent<NavMeshAgent>() != null)
                {
                    player1.GetComponent<NavMeshAgent>().enabled = false;
                }

                if (Managers.Player != null)
                {
                    if (Managers.Player.GetEnteringRoomName() == "InsideCafeteriaBuilding")
                    {
                        if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                        {
                            player1.transform.position = cafeteriaSpawnPoint.transform.position;
                        }
                    }
                    else if (Managers.Player.GetEnteringRoomName() == "TerrainCampus")
                    {
                        if (Managers.Player.GetPreviousRoomName() == "InsideCafeteriaBuilding")
                        {
                            player1.transform.position = TerrainCafeteriaSpawnPoint.transform.position;
                        }
                        else if (Managers.Player.GetPreviousRoomName() == "ArtHallway")
                        {
                            player1.transform.position = TerrainArtHallwaySpawnPoint.transform.position;
                        }
                        else if (Managers.Player.GetPreviousRoomName() == "ScienceHallway")
                        {
                            player1.transform.position = TerrainScienceHallwaySpawnPoint.transform.position;
                        }
                        else if (Managers.Player.GetPreviousRoomName() == "Dorm")
                        {
                            player1.transform.position = TerrainDormSpawnPoint.transform.position;
                        }
                    }
                    else if (Managers.Player.GetEnteringRoomName() == "ArtHallway")
                    {
                        if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                        {
                            player1.transform.position = hallwaySpawnPoint.transform.position;
                        }
                        else if (Managers.Player.GetPreviousRoomName() == "ArtRoom")
                        {
                            player1.transform.position = hallwayArtRoomSpawnPoint.transform.position;
                        }
                    }
                    else if (Managers.Player.GetEnteringRoomName() == "ScienceHallway")
                    {
                        if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                        {
                            player1.transform.position = hallwaySpawnPoint.transform.position;
                        }
                        else if (Managers.Player.GetPreviousRoomName() == "ScienceRoom")
                        {
                            player1.transform.position = hallwayArtRoomSpawnPoint.transform.position;
                        }
                    }
                    else if (Managers.Player.GetEnteringRoomName() == "ArtRoom")
                    {
                        if (Managers.Player.GetPreviousRoomName() == "ArtHallway")
                        {
                            player1.transform.position = ArtRoomSpawnPoint.transform.position;
                        }
                    }
                    else if (Managers.Player.GetEnteringRoomName() == "Dorm")
                    {
                        if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                        {
                            player1.transform.position = DormSpawnPoint.transform.position;
                        }
                        Debug.Log("GameManager.cs: line 108 Dorm");
                    }
                }

                if (player1.GetComponent<NavMeshAgent>() != null)
                {
                    player1.GetComponent<NavMeshAgent>().enabled = true;

                }

                break;
		case 2:
			player2.SetActive (true);

            /*
            foreach (CinemachineVirtualCamera OneCam in myCams)
            {
                if (OneCam.gameObject.tag == "Building" || OneCam.gameObject.tag == "ArtRoomCam")
                {
                    continue;
                }
                else
                {
                    Transform objectToFollow = null;
                    
                    foreach (Transform playerChild in player2.transform)
                    {
                        if (playerChild.CompareTag("CharacterTransform"))
                        {
                            objectToFollow = playerChild.transform;
                        }
                    }

                    OneCam.Follow = objectToFollow;
                    OneCam.LookAt = objectToFollow;
                }
            }
            */

            if (player2.GetComponent<NavMeshAgent>() != null)
            {
                player2.GetComponent<NavMeshAgent>().enabled = false;
            }


            if (Managers.Player != null)
            {
                if (Managers.Player.GetEnteringRoomName() == "InsideCafeteriaBuilding")
                {
                    if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                    {
                        player2.transform.position = cafeteriaSpawnPoint.transform.position;
                    }
                }
                else if (Managers.Player.GetEnteringRoomName() == "TerrainCampus")
                {
                    if (Managers.Player.GetPreviousRoomName() == "InsideCafeteriaBuilding")
                    {
                        player2.transform.position = TerrainCafeteriaSpawnPoint.transform.position;
                    }
                    else if (Managers.Player.GetPreviousRoomName() == "ArtHallway")
                    {
                        player2.transform.position = TerrainArtHallwaySpawnPoint.transform.position;
                    }
                    else if (Managers.Player.GetPreviousRoomName() == "ScienceHallway")
                    {
                        player2.transform.position = TerrainScienceHallwaySpawnPoint.transform.position;
                    }
                    else if (Managers.Player.GetPreviousRoomName() == "Dorm")
                    {
                        player2.transform.position = TerrainDormSpawnPoint.transform.position;
                    }
                }
                else if (Managers.Player.GetEnteringRoomName() == "ArtHallway")
                {
                    if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                    {
                        player2.transform.position = hallwaySpawnPoint.transform.position;
                    }
                    else if (Managers.Player.GetPreviousRoomName() == "ArtRoom")
                    {
                        player2.transform.position = hallwayArtRoomSpawnPoint.transform.position;
                    }
                }
                else if (Managers.Player.GetEnteringRoomName() == "ScienceHallway")
                {
                    if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                    {
                        player2.transform.position = hallwaySpawnPoint.transform.position;
                    }
                    else if (Managers.Player.GetPreviousRoomName() == "ScienceRoom")
                    {
                        player2.transform.position = hallwayArtRoomSpawnPoint.transform.position;
                    }
                }
                else if (Managers.Player.GetEnteringRoomName() == "ArtRoom")
                {
                    if (Managers.Player.GetPreviousRoomName() == "ArtHallway")
                    {
                        player2.transform.position = ArtRoomSpawnPoint.transform.position;
                    }
                }
                else if (Managers.Player.GetEnteringRoomName() == "Dorm")
                {
                    if (Managers.Player.GetPreviousRoomName() == "TerrainCampus")
                    {
                        player2.transform.position = DormSpawnPoint.transform.position;
                    }
                }
            }

            if (player1.GetComponent<NavMeshAgent>() != null)
            {
                player2.GetComponent<NavMeshAgent>().enabled = true;
            }

            break;
		case 3:
			player3.SetActive (true);

            /*
            foreach (CinemachineVirtualCamera OneCam in myCams)
            {
                if (OneCam.gameObject.tag == "Building" || OneCam.gameObject.tag == "ArtRoomCam")
                {
                    continue;
                }
                else
                {
                        Transform objectToFollow = null;
                        foreach (Transform playerChild in player3.transform)
                        {
                            if (playerChild.CompareTag("CharacterTransform"))
                            {
                                objectToFollow = playerChild.transform;
                            }
                        }

                        OneCam.Follow = objectToFollow;
                        OneCam.LookAt = objectToFollow;
                    }
            }
            */

           break;
		default:
			player1.SetActive (true);

            /*
            foreach (CinemachineVirtualCamera OneCam in myCams)
            {
                if (OneCam.gameObject.tag == "Building" || OneCam.gameObject.tag == "ArtRoomCam")
                {
                    continue;
                }
                else
                {
                    Transform objectToFollow = null;
                    foreach (Transform playerChild in player1.transform)
                    {
                        if (playerChild.CompareTag("CharacterTransform"))
                        {
                            objectToFollow = playerChild;
                        }
                    }

                    OneCam.Follow = objectToFollow;
                    OneCam.LookAt = objectToFollow;
                }
            }
            */

            break;
		}
    }
}