using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private InputField input;

     void Awake()
    {
        
    }


    public void GetInput(string guess)
    {
        Debug.Log("You answered " + guess);
        input.text = "";

    }
}