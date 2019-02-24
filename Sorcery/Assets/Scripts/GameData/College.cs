using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class College : MonoBehaviour {

	public static College collegeInfo;

	public int reputation;
	public int community = 15;
	public int infrastructure = 15;

	void Awake(){

		if(collegeInfo == null){
			DontDestroyOnLoad (gameObject);
			collegeInfo = this;
		}else if(collegeInfo != this){
			Destroy (gameObject);
		}
	}

	void Start () {

	}

	void Update () {
		reputation = (community + infrastructure) / 2;
	}

	//	void OnEnable(){
	//		Load ();
	//	}
	//
	//	void OnDiable(){
	//		Save ();
	//	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/collegeInfo.dat");

		CollegeData data = new CollegeData ();
		data.reputation = reputation;
		data.community = community;
		data.infrastructure = infrastructure;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/collegeInfo.dat", FileMode.Open);
		CollegeData data = (CollegeData)bf.Deserialize (file);
		file.Close ();

		reputation = data.reputation;
		community = data.community;
		infrastructure = data.infrastructure;
	}
}

[Serializable]
class CollegeData{

	public int reputation;
	public int community;
	public int infrastructure;
}