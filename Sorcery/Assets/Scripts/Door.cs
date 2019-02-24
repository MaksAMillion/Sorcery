using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string entranceName;
    public int scene;
    public Canvas doorFeedback;
    public Canvas dialogueFeedback;
    private AudioSource audioSource;
    public AudioClip audioClip;

    FadeInOut fadeScr;
    public int sceneNumb;
    
    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.SLEEP, sleep);
        fadeScr = FindObjectOfType<FadeInOut>();
    }
    
    private void Start()
    {
        if (doorFeedback != null)
        {
            doorFeedback.enabled = false;
        }
        if (dialogueFeedback != null)
        {
            dialogueFeedback.enabled = false;
        }

        audioSource = GetComponent<AudioSource>();
    }
    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.SLEEP, sleep);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && Managers.Player != null)
        {
            Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);

            if (doorFeedback != null)
            {
                doorFeedback.enabled = false;
            }

            if (dialogueFeedback != null && Managers.Inventory != null && Managers.Inventory.GetItemCount("Phone") < 1)
            {
                dialogueFeedback.GetComponent<Animator>().SetTrigger("Display");
                // dialogueFeedback.enabled = false;
            }
        }

        Debug.Log("On door trigger exit set previous room: " + SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (entranceName == "InsideCafeteriaBuilding")
            {

                if (Managers.Player != null && entranceName != Managers.Player.GetPreviousRoomName())
                {
                    Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);
                    Managers.Player.SetEnteringRoomName(entranceName);

                    Debug.Log("On door trigger enter: " + entranceName);
                    fadeScr.EndScene(scene);
                }
                else
                {
                    Debug.Log("On door trigger enter: " + entranceName);
                }
            }
            else if (entranceName == "ArtHallway")
            {
                if (Managers.Player != null && entranceName != Managers.Player.GetPreviousRoomName())
                {
                    Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);
                    Managers.Player.SetEnteringRoomName(entranceName);

                    Debug.Log("On door trigger enter: " + entranceName);
                    fadeScr.EndScene(scene);
                }
                else
                {
                    Debug.Log("On door trigger enter: " + entranceName);
                }
            }
            else if (entranceName == "ScienceHallway")
            {
                if (Managers.Player != null && entranceName != Managers.Player.GetPreviousRoomName())
                {
                    Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);
                    Managers.Player.SetEnteringRoomName(entranceName);

                    Debug.Log("On door trigger enter: " + entranceName);
                    fadeScr.EndScene(scene);
                }
                else
                {
                    Debug.Log("On door trigger enter: " + entranceName);
                }
            }
            else if (entranceName == "ArtRoom")
            {
                if (Managers.Player != null && entranceName != Managers.Player.GetPreviousRoomName())
                {
                    Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);
                    Managers.Player.SetEnteringRoomName(entranceName);

                    Debug.Log("On door trigger enter: " + entranceName);
                    fadeScr.EndScene(scene);
                }
                else
                {
                    Debug.Log("On door trigger enter: " + entranceName);
                }
            }
			else if (entranceName == "TerrainCampus")
			{
                if (Managers.Inventory != null)
                {
                    Debug.Log(Managers.Inventory.GetItemCount("Phone"));

                    if (Managers.Inventory.GetItemCount("Phone") > 0)
                    {
                        if (Managers.Player != null && entranceName != Managers.Player.GetPreviousRoomName())
                        {
                            Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);
                            Managers.Player.SetEnteringRoomName(entranceName);

                            Debug.Log("On door trigger enter: " + entranceName);
                            fadeScr.EndScene(scene);
                        }
                        else
                        {
                            Debug.Log("On door trigger enter: " + entranceName);
                        }
                    }
                    else
                    {
                        doorFeedback.enabled = true;
                        dialogueFeedback.enabled = true;
                        dialogueFeedback.GetComponent<Animator>().SetTrigger("Display");
                        audioSource.PlayOneShot(audioClip);
                    }
                }
            }
            else if (entranceName == "Dorm")
            {

                if (Managers.Player != null && entranceName != Managers.Player.GetPreviousRoomName())
                {
                    Managers.Player.SetPreviousRoomName(SceneManager.GetActiveScene().name);
                    Managers.Player.SetEnteringRoomName(entranceName);

                    Debug.Log("On door trigger enter: " + entranceName);
                    fadeScr.EndScene(scene);
                }
                else
                {
                    Debug.Log("On door trigger enter: " + entranceName);
                }
            }
        }
    }

    private void sleep(int sceneNumber)
    {
        Managers.Player.SetEnteringRoomName("Dorm");

        Managers.Player.SetPreviousRoomName("TerrainCampus");
                fadeScr.EndScene(sceneNumber);
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (! ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);

            Debug.Log("Loading progress: " + (progress * 100) + "%");

            if (ao.progress == 0.9f)
            {
                // This code prints the current progress on the console, but it can be easily changed to update any UI you have designed.
                Debug.Log("Press a key to start");

                if (Input.anyKey)
                {
                    ao.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
