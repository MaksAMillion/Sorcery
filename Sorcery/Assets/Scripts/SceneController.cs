using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject phoneCollectible;
    public Animator door;
    private bool levelInit = true;
    public GameObject phoneButton;


	void Start () {
        if (Managers.Inventory != null)
        {
            if (Managers.Inventory.GetItemCount("Phone") > 0)
            {
                Destroy(phoneCollectible);
                levelInit = false;
                phoneButton.SetActive(true);

            }
            else
            {
                phoneButton.SetActive(false);
            }
        }
	}

    private void Update()
    {
        if (levelInit && Managers.Inventory != null && Managers.Inventory.GetItemCount("Phone") > 0)
        {
            door.SetTrigger("Glow");
            phoneButton.SetActive(true);
            levelInit = false;
			Messenger<string>.Broadcast(GameEvent.UI_TEXT_NOTIFICATION, "You've picked up your phone!");

        }
    }


}
