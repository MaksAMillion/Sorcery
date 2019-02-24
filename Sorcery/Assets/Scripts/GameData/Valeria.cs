using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Valeria : MonoBehaviour {

	public static Valeria valeriaInfo;

	public int reputation = 25;
	public int romance = 25;

	//  Week 1
	[SerializeField] public GameObject treeLocation;
	[SerializeField] public GameObject tree;
	public int treeCount = 0;

	void Awake(){

		if(valeriaInfo == null){
			DontDestroyOnLoad (gameObject);
			valeriaInfo = this;
		}else if(valeriaInfo != this){
			Destroy (gameObject);
		}
	}

	void Start () {

	}

	void Update () {

	}

	//  Week 1 Quest
	public void PlantTrees(){
		Instantiate (treeLocation, new Vector3 (7, -1, -16), Quaternion.identity);
		Instantiate (treeLocation, new Vector3 (7, -1, -8), Quaternion.identity);
		Instantiate (treeLocation, new Vector3 (3, -1, -12), Quaternion.identity);
		Instantiate (treeLocation, new Vector3 (11, -1, -12), Quaternion.identity);
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/valeriaInfo.dat");

		ValeriaData data = new ValeriaData ();
		data.reputation = reputation;
		data.romance = romance;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/valeriaInfo.dat", FileMode.Open);
		ValeriaData data = (ValeriaData)bf.Deserialize (file);
		file.Close ();

		reputation = data.reputation;
		romance = data.romance;
	}
}

[Serializable]
class ValeriaData{

	public int reputation;
	public int romance;
}