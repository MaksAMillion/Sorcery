using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Josh : MonoBehaviour {

	public static Josh joshInfo;

	public int reputation = 25;
	public int romance = 25;

	[SerializeField] public GameObject book;

	void Awake(){
		
		if(joshInfo == null){
			DontDestroyOnLoad (gameObject);
			joshInfo = this;
		}else if(joshInfo != this){
			Destroy (gameObject);
		}
	}

	void Start () {
		
	}
	
	void Update () {
		
	}


	public void FindBook(){
		Instantiate (book,
			new Vector3 (10, 0, 0),
			Quaternion.identity);
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/joshInfo.dat");

		JoshData data = new JoshData ();
		data.reputation = reputation;
		data.romance = romance;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/joshInfo.dat", FileMode.Open);
		JoshData data = (JoshData)bf.Deserialize (file);
		file.Close ();

		reputation = data.reputation;
		romance = data.romance;
	}
}

[Serializable]
class JoshData{

	public int reputation;
	public int romance;
}