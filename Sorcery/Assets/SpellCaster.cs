using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, ISpeechHandler
{
    [SerializeField]
    ParticleSystem particleSystem;
    [SerializeField]
    Transform _SpawnPoint;

    private ParticleSystem particleObject;

	void Start ()
    {

    }
	
	void Update ()
    {
		
	}

    public void castSpell()
    {
        Debug.Log("Expecto Patronum");
        particleSystem.Play();
    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        switch (eventData.RecognizedText)
        {
            case "Expecto Patronum":
                castSpell();
                break;
        }
    }
}
