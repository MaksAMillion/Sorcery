using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ToggleMiniMap : MonoBehaviour
{
    public Camera[] MiniMapCam;
	[SerializeField] public GameObject miniMapPanel;
    [SerializeField] public GameObject miniMapLabelsPanel;

	void Start(){
		miniMapPanel.SetActive (false);
        miniMapLabelsPanel.SetActive(false);
	}

    public void toggle()
    {
		if (MiniMapCam [0].enabled) {
			MiniMapCam [0].enabled = false;
			miniMapPanel.SetActive (false);
            miniMapLabelsPanel.SetActive(false);
            Messenger.Broadcast(GameEvent.PLAYER_GO);
		} else {
			MiniMapCam [0].enabled = true;
			miniMapPanel.SetActive (true);
            miniMapLabelsPanel.SetActive(true);
            Messenger.Broadcast(GameEvent.PLAYER_STOP);
		}
    }



}

