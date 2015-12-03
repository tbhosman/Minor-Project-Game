using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveLoadScript : MonoBehaviour {

	public string parentstosave;
	public GameObject[] allObjects;
	public int AOGameObjects;


	void Start () {
	
		//initialize allobjectsarray
		allObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject)) ;
		AOGameObjects = allObjects.Length;
	}
	
	public void Save() {

		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create(Application.persistentDataPath + "/savefile.sav");


		SaveManager saver = new SaveManager ();

		//initialize arrays
		saver.xcoordinates = new float[AOGameObjects];
		saver.ycoordinates = new float[AOGameObjects];
		saver.zcoordinates = new float[AOGameObjects];
		saver.rotationx = new float[AOGameObjects];
		saver.rotationy = new float[AOGameObjects];
		saver.rotationz = new float[AOGameObjects];
		saver.rotationw = new float[AOGameObjects];
		saver.active = new bool[AOGameObjects];


		//Stuff to save!!!
		for (int i = 0; i < AOGameObjects; i++) {
			GameObject ObjectToSave = allObjects[i];
			saver.xcoordinates [i] = ObjectToSave.transform.position.x;
			saver.ycoordinates [i] = ObjectToSave.transform.position.y;
			saver.zcoordinates [i] = ObjectToSave.transform.position.z;
			saver.rotationx [i] = ObjectToSave.transform.forward.x;
			saver.rotationy [i] = ObjectToSave.transform.forward.y;
			saver.rotationz [i] = ObjectToSave.transform.forward.z;
			saver.rotationw [i] = ObjectToSave.transform.localRotation.w;
			saver.active[i] = ObjectToSave.activeInHierarchy;
		}


		binary.Serialize (fStream, saver);
		fStream.Close ();
		print ("game saved");
	}

	public void Load()	{
		if (File.Exists (Application.persistentDataPath + "/savefile.sav")) {
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "/savefile.sav", FileMode.Open);
			SaveManager saver = (SaveManager)binary.Deserialize (fStream);
			fStream.Close ();

			//Stuff to load!!!
			for (int i = 0; i < AOGameObjects; i++) {
				GameObject ObjectToLoad = allObjects[i];
				ObjectToLoad.transform.position = new Vector3 (saver.xcoordinates [i], saver.ycoordinates [i], saver.zcoordinates [i]);
				ObjectToLoad.SetActive(saver.active[i]);
			}
			print ("game loaded");
		} else {
			print ("No SaveFile Exists yet");
		}
	}

	public void RemoveSaveFile(){
		File.Delete (Application.persistentDataPath + "/savefile.sav");
		Application.LoadLevel(0);
		print ("savefile removed");

	}


	[Serializable]
	class SaveManager
	{
		public float[] xcoordinates;
		public float[] ycoordinates;
		public float[] zcoordinates;
		public float[] rotationx;
		public float[] rotationy;
		public float[] rotationz;
		public float[] rotationw;
		public bool[] active;
	}
}
