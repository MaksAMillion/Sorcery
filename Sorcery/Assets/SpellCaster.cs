using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
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
        particleObject = Instantiate(particleSystem, _SpawnPoint);
        particleObject.transform.up = transform.right;
    }
}
