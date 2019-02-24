using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBallVolume : MonoBehaviour {

   [SerializeField] public GameObject BGM;
   [SerializeField] public GameObject DanceBGM;

    AudioSource DanceSource;


    /*
    for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(i);
        }
    */
    void Start()
    {
        DanceSource = DanceBGM.GetComponent<AudioSource>();
        BGM.SetActive(true);
        DanceBGM.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        BGM.SetActive(false);

        DanceBGM.SetActive(true);







    }



    void OnTriggerExit(Collider other)
    {
        BGM.SetActive(true);
        DanceBGM.SetActive(false);
    }




}
