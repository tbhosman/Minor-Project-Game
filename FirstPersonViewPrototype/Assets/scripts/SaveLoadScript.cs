/// <summary>
/// Script used for saving and loading of the game
/// </summary>

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
	public bool[] keyObjectsPickedUp;
	public int PlayerID;
	public GameObject AreaTrigger;
	public string area;
	public GameObject AnimatorPlayer;

	//This is needs to be executed before de Start void of every GameObject
	//If no savefile exists, the load function will handle it.
	void Awake(){
		savertimeplayed = 0;
		Load ();
	}


	//function that saves the activestate of diffferent objects (ObjectsToSave),
	// and the player and enemy locations inside a file called savefile.sav
	//Its only possible to save variables like String, bool, float etc. Not GameObjects or AudioSource etc.
	public void Save() {

		// Open a binary (encrypted) filestream to the savefile (saver)
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create(Application.persistentDataPath + "/savefile.sav");
		SaveManager saver = new SaveManager ();

		//initialize arrays for savefile in saver object
		saver.active = new bool[ObjectsToSave.Length];
		saver.antagcoordinates = new float[3];
		saver.playercoordinates = new float[3];
		saver.dooropened = new bool[DoorOpened.Length];

		//saving which area the player is located in
			if(AreaTrigger.GetComponent<MainAreaTriggerController>().inOffice){
				saver.area = "Office";
			area = saver.area;
			}
			if(AreaTrigger.GetComponent<MainAreaTriggerController>().inLab){
				saver.area = "Lab";
			area = saver.area;
			}
			if(AreaTrigger.GetComponent<MainAreaTriggerController>().inStorage){
				saver.area = "Storage";
			area = saver.area;
			}
			if(AreaTrigger.GetComponent<MainAreaTriggerController>().inArchive){
				saver.area = "Archive";
			area = saver.area;
			}
			if(AreaTrigger.GetComponent<MainAreaTriggerController>().inMachine){
				saver.area = "MachineRoom";
			area = saver.area;
			}

		//Saving playerID
		saver.PlayerID = PlayerPrefs.GetInt ("ID");

		//Saving wich keyObjects are PickedUp;
		saver.ObjectsPickedUp = keyObjectsPickedUp;

		//Saving ActiveState of public GameObject array ObjectsToSave
		for (int i = 0; i < ObjectsToSave.Length; i++ ) {
			saver.active[i] = ObjectsToSave[i].activeSelf;
		}

		//Saving which keyObjects are PickedUp
		saver.ObjectsPickedUp = keyObjectsPickedUp;

		//Saving which doors are opened
		for (int i = 0; i < DoorOpened.Length; i++ ) {
			saver.dooropened[i] = DoorOpened[i];
		}

		//Saving the amount of time played (savertimeplayed is timeplayed before previous load)
		saver.timeplayed = Time.timeSinceLevelLoad + savertimeplayed;

		//Saving player Rotation
		saver.PlayerYRotation = Player.transform.eulerAngles.y;

		//Saving player Coordinates
		saver.playercoordinates [0] = Player.transform.position.x;
		saver.playercoordinates [1] = Player.transform.position.y;
		saver.playercoordinates [2] = Player.transform.position.z;

		//Saving Antag Coordinates
		saver.antagcoordinates [0] = Antag.transform.position.x;
		saver.antagcoordinates [1] = Antag.transform.position.y;
		saver.antagcoordinates [2] = Antag.transform.position.z;

		//Writing everything in savefile.sav (encrypted)
		binary.Serialize (fStream, saver);
		fStream.Close ();


		print ("game saved to " + Application.persistentDataPath + "/savefile.sav,  playtime: " + saver.timeplayed + " Player ID: " + saver.PlayerID);
	}


	//Function that loads all the saved variables, this is executed only once (On Awake)
	public void Load()	{

		//Check if File Exists
		if (File.Exists (Application.persistentDataPath + "/savefile.sav")) {

			//Skip Wake Animation
			AnimatorPlayer = GameObject.Find ("AnimatorPlayer");
			AnimatorPlayer.GetComponent<Animator>().SetTrigger ("SkipWake");

			//Activate Playerlight always necessary when loading
			Playerlight.SetActive (true);

			//Initialize saver object with savefile.sav
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "/savefile.sav", FileMode.Open);
			SaveManager saver = (SaveManager)binary.Deserialize (fStream);
			fStream.Close ();

			//Initializing in Scene variables with saver object

			//Initialize area player is in
			if(saver.area == "Office"){
				AreaTrigger.GetComponent<MainAreaTriggerController>().inOffice = true;
			}
			if(saver.area == "Lab"){
				AreaTrigger.GetComponent<MainAreaTriggerController>().inLab = true;
			}
			if(saver.area == "Storage"){
				AreaTrigger.GetComponent<MainAreaTriggerController>().inStorage = true;
			}
			if(saver.area == "Archive"){
				AreaTrigger.GetComponent<MainAreaTriggerController>().inArchive= true;
			}
			if(saver.area == "MachineRoom"){
				AreaTrigger.GetComponent<MainAreaTriggerController>().inMachine= true;
			}
			area = saver.area;

			//Initialize PlayerID
			PlayerPrefs.SetInt("ID",saver.PlayerID);
			PlayerID = saver.PlayerID;

			//Initialize which objects are picked up
			keyObjectsPickedUp = saver.ObjectsPickedUp;

			//Initialize which doors are opened
			for (int i = 0; i < DoorOpened.Length; i++ ) {
				DoorOpened[i] = saver.dooropened[i];
			}

			for (int i = 0; i < ObjectsToSave.Length; i++ ) {
				ObjectsToSave[i].SetActive (saver.active[i]);
			}

			//Initialize savertimeplayed so when saved, the previous savetime is added
			savertimeplayed = saver.timeplayed;

			//Initialize player Rotation
			Player.transform.rotation = Quaternion.Euler(0,saver.PlayerYRotation,0);

			//Initialize player Position
			Player.transform.position = new Vector3(saver.playercoordinates [0],saver.playercoordinates [1],saver.playercoordinates [2]);

			//Initialize Antag Position
			Antag.transform.position = new Vector3(saver.antagcoordinates[0],saver.antagcoordinates[1],saver.antagcoordinates[2]);

			//Start areamusic
			GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController>().FadeIn (area);

			print ("game loaded, playtime: " + savertimeplayed + " Player ID: " + saver.PlayerID);
		
		} 
		//Execute when savefile.sav doesnt exist
		else {
			print ("No SaveFile Exists yet");
			AreaTrigger.GetComponent<MainAreaTriggerController>().inOffice = true;
		}
	}

	//Remove Savefile (only on Start new game)
	public void RemoveSaveFile(){
		File.Delete (Application.persistentDataPath + "/savefile.sav");
		Application.LoadLevel(0);
		print ("savefile removed");
	}



	//This is the class that is serializable with the encrypted savefile.sav
	[Serializable]
	class SaveManager
	{
		public float[] playercoordinates;
		public float[] antagcoordinates;
		public float timeplayed;
		public bool[] active;
		public bool[] dooropened;
		public float PlayerYRotation;
		public bool[] ObjectsPickedUp;
		public int PlayerID;
		public string area;
	}
}
