/// <summary>
/// HOW TO USE:
/// 
/// 1) Set the UI gameobject with the tag "chatUI"
/// 2) Make sure the NPC gameobject is tag with its name
/// 3) The sprite resource name needs to match the tag
/// 
/// Example:   		Messenger<string>.Broadcast(GameEvent.START_TALK_MC_LEFT, "npcNameHere" );

///
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkingSpriteArtRoom : MonoBehaviour
{

    // ashley code variables
    private GameObject UI;
    //public GameObject touchControl;
    private Sprite[] rightSprites; //These will be needed to show different expressions for npc
    private Sprite[] leftSprites; //These will be needed to show different expressions for mc
    private int rightCurrentSpriteNum;
    private int leftCurrentSpriteNum;
    private Vector3 leftSpriteLoc;
    private Vector3 rightSpriteLoc;
    private GameObject leftSprite;
    private GameObject rightCurrentSpriteObj;
    private GameObject leftCurrentSpriteObj;
    private GameObject rightSprite;
    int mcNum;



    void Awake()
    {

        Messenger<string>.AddListener(GameEvent.START_TALK_MC_LEFT, startChatMcLeft);
        Messenger<string>.AddListener(GameEvent.START_TALK_MC_RIGHT, startChatMcRight);
        Messenger.AddListener(GameEvent.END_TALK_SPRITES, endChat);


    }

    void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvent.START_TALK_MC_LEFT, startChatMcLeft);
        Messenger<string>.RemoveListener(GameEvent.START_TALK_MC_RIGHT, startChatMcRight);
        Messenger.RemoveListener(GameEvent.END_TALK_SPRITES, endChat);
    }


    void Start()
    {
        //PlayerPrefs.SetInt ("AvatarSelect",1);
        //startChatMcRight ("flower");
        //startChatMcLeft ("flower");
    }



    public void startChatMcLeft(string npcTagName)
    {

        //This variable will change depending on which MC sprite they choose
        if (PlayerPrefs.HasKey("AvatarSelect"))
        {
            mcNum = PlayerPrefs.GetInt("AvatarSelect");
        }
        else
        {
            mcNum = 2;
        }

        UI = GameObject.FindGameObjectWithTag("chatUI");
        //npcSpriteLoc = new Vector3 (360f, -51.5f, 0);
        //mcSpriteLoc = new Vector3 (-360f,-51.5f,0);


        //mcSprites=Resources.LoadAll<Sprite>("TWEWYNeku");
        //Show the first sprite 
        //npcCurrentSpriteNum = 0;
        //mcCurrentSpriteNum = 0;

        //Generate the Main Character Sprite on Left Side
        leftSprite = (GameObject)Resources.Load("left_character");
        //mcSprites = mcSprite.GetComponent<Image> ().sprite;
        leftCurrentSpriteObj = Instantiate(leftSprite) as GameObject;
        leftCurrentSpriteObj.transform.localScale = new Vector3(1, 1, 1);
        leftCurrentSpriteObj.transform.SetParent(UI.transform);
        leftCurrentSpriteObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("MC" + mcNum);


        //Generate the NPC Sprite on Right Side
        rightSprite = (GameObject)Resources.Load("right_character");
        rightCurrentSpriteObj = Instantiate(rightSprite, rightSprite.transform.position, rightSprite.transform.rotation) as GameObject;
        rightCurrentSpriteObj.transform.SetParent(UI.transform, false);
        rightCurrentSpriteObj.transform.localScale = new Vector3(1, 1, 1);
        rightCurrentSpriteObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(npcTagName);

    }

    public void startChatMcRight(string npcTagName)
    {

        //This variable will change depending on which MC sprite they choose
        if (PlayerPrefs.HasKey("AvatarSelect"))
        {
            mcNum = PlayerPrefs.GetInt("AvatarSelect");
        }
        else
        {
            mcNum = 2;
        }
        UI = GameObject.FindGameObjectWithTag("chatUI");
        //npcSpriteLoc = new Vector3 (360f, -51.5f, 0);
        //mcSpriteLoc = new Vector3 (-360f,-51.5f,0);


        //mcSprites=Resources.LoadAll<Sprite>("TWEWYNeku");
        //Show the first sprite 
        //npcCurrentSpriteNum = 0;
        //mcCurrentSpriteNum = 0;

        //Generate the Main Character Sprite on Left Side
        leftSprite = (GameObject)Resources.Load("left_character");
        leftCurrentSpriteObj = Instantiate(leftSprite) as GameObject;

        leftCurrentSpriteObj.transform.SetParent(UI.transform);
        leftCurrentSpriteObj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        leftCurrentSpriteObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(npcTagName);


        //Generate the NPC Sprite on Right Side
        rightSprite = (GameObject)Resources.Load("right_character");
        rightCurrentSpriteObj = Instantiate(rightSprite, rightSprite.transform.position, rightSprite.transform.rotation) as GameObject;
        rightCurrentSpriteObj.transform.SetParent(UI.transform, false);
        rightCurrentSpriteObj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        rightCurrentSpriteObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("MC" + PlayerPrefs.GetInt("AvatarSelect"));

    }

    public void endChat()
    {
        Destroy(leftCurrentSpriteObj);
        Destroy(rightCurrentSpriteObj);

    }
}
