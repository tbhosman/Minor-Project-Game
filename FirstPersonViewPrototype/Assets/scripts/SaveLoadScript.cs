using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveLoadScript : MonoBehaviour {

	public GameObject[] ObjectsToSave;
	public float savertimeplayed;
	public GameObject Playerlight;
	public GameObject Player;
	public GameObject Antag;
	public bool[] DoorOpened;

	void Awake(){
		Debug.Log ("Game Awake");
		savertimeplayed = 0;
		Load ();
	}


	public void Save() {

		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create(Application.persistentDataPath + "/savefile.sav");


		SaveManager saver = new SaveManager ();

		//initialize arrays
		saver.active = new bool[ObjectsToSave.Length];
		saver.antagcoordinates = new float[3];
		saver.playercoordinates = new float[3];
		saver.dooropened = new bool[DoorOpened.Length];
		//Stuff to save!!!
		for (int i = 0; i < ObjectsToSave.Length; i++ ) {
			saver.active[i] = ObjectsToSave[i].activeSelf;
		}

		for (int i = 0; i < DoorOpened.Length; i++ ) {
			saver.dooropened[i] = DoorOpened[i];
		}

		saver.timeplayed = Time.timeSinceLevelLoad + savertimeplayed;

		saver.playercoordinates [0] = Player.transform.position.x;
		saver.playercoordinates [1] = Player.transform.position.y;
		saver.playercoordinates [2] = Player.transform.position.z;

		saver.antagcoordinates [0] = Antag.transform.position.x;
		saver.antagcoordinates [1] = Antag.transform.position.y;
		saver.antagcoordinates [2] = Antag.transform.position.z;



		binary.Serialize (fStream, saver);
		fStream.Close ();
		print ("game saved to " + Application.persistentDataPath + "/savefile.sav,  playtime: " + saver.timeplayed);
	}

	public void Load()	{
		if (File.Exists (Application.persistentDataPath + "/savefile.sav")) {
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "/savefile.sav", FileMode.Open);
			SaveManager saver = (SaveManager)binary.Deserialize (fStream);
			fStream.Close ();

			//Stuff to load!!!
			for (int i = 0; i < ObjectsToSave.Length; i++ ) {
				ObjectsToSave[i].SetActive (saver.active[i]);
			}

			for (int i = 0; i < DoorOpened.Length; i++ ) {
				DoorOpened[i] = saver.dooropened[i];
			}

			savertimeplayed = saver.timeplayed;

			Playerlight.SetActive (true);

			Player.transform.position = new Vector3(saver.playercoordinates [0],saver.playercoordinates [1],saver.playercoordinates [2]);

			Antag.transform.position = new Vector3(saver.antagcoordinates[0],saver.antagcoordinates[1],saver.antagcoordinates[2]);

			print ("game loaded, playtime: " + savertimeplayed);
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
		public float[] playercoordinates;
		public float[] antagcoordinates;
		public float timeplayed;
		public bool[] active;
		public bool[] dooropened;
	}
}
