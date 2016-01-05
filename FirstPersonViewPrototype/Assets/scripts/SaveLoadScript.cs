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

	void Start () {
		savertimeplayed = 0;
		Load ();
		//initialize allobjectsarray
	}

	public void Save() {

		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create(Application.persistentDataPath + "/savefile.sav");


		SaveManager saver = new SaveManager ();

		//initialize arrays
		saver.active = new bool[ObjectsToSave.Length];
		//Stuff to save!!!
		for (int i = 0; i < ObjectsToSave.Length; i++ ) {
			saver.active[i] = ObjectsToSave[i].activeSelf;
		}
		saver.timeplayed = Time.timeSinceLevelLoad + savertimeplayed;

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
			savertimeplayed = saver.timeplayed;
			Playerlight.SetActive (true);
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
		public float timeplayed;
		public bool[] active;
	}
}
