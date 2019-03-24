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
    [SerializeField]
    GameObject _CorporealPatronusPrefab;
    GameObject corporealPatronus;

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
        corporealPatronus = Instantiate(_CorporealPatronusPrefab, _SpawnPoint);
        corporealPatronus.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        corporealPatronus.transform.Rotate(new Vector3(-90, 0, 180));
        corporealPatronus.transform.parent = null;
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
