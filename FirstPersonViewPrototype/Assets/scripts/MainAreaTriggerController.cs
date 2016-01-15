using UnityEngine;
using System.Collections;

public class MainAreaTriggerController : MonoBehaviour {

	public bool inStorage;
	public bool inOffice;
	public bool inLab;
	public bool inArchive;
	public bool inMachine;
	public bool inReaktor;
	public GameObject EnemySpawnLocations;
	private GameObject EnemyObject;
	private Vector3 spawnLocation;

	// Use this for initialization
	void Start () {
		EnemySpawnLocations = GameObject.Find ("EnemySpawnLocations");
		EnemyObject = GameObject.Find ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void InArea(string Area){
		if (Area == "Office" && inOffice == false) {
			inStorage = false;
			inOffice = true;
			inArchive = false;
			inLab = false;
			inMachine = false;
			inReaktor = false;
			respawnIn(Area);
			if (GameObject.Find("MainMusicController") != null)
				GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn (Area);
			else
				GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController> ().FadeIn (Area);
		} else if (Area == "Storage" && inStorage == false) {
			inStorage = true;
			inOffice = false;
			inArchive = false;
			inLab = false;
			inMachine = false;
			inReaktor = false;
			Debug.Log ("in storage");
			respawnIn(Area);
			if (GameObject.Find("MainMusicController") != null)
				GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn (Area);
			else
				GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController> ().FadeIn (Area);
		} else if (Area == "Lab" && inLab == false) {
			inStorage = false;
			inOffice = false;
			inArchive = false;
			inLab = true;
			inMachine = false;
			inReaktor = false;
			respawnIn(Area);
			if (GameObject.Find("MainMusicController") != null)
				GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn (Area);
			else
				GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController> ().FadeIn (Area);
		} else if (Area == "Machine" && inMachine == false) {
			inStorage = false;
			inOffice = false;
			inArchive = false;
			inLab = false;
			inMachine = true;
			inReaktor = false;
			respawnIn(Area);
			if (GameObject.Find("MainMusicController") != null)
				GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn (Area);
			else
				GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController> ().FadeIn (Area);
		} else if (Area == "Archive" && inArchive == false) {
			inStorage = false;
			inOffice = false;
			inArchive = true;
			inLab = false;
			inMachine = false;
			inReaktor = false;
			respawnIn(Area);
			if (GameObject.Find("MainMusicController") != null)
				GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn (Area);
			else
				GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController> ().FadeIn (Area);
		} else if (Area == "Reaktor" && inReaktor == false) {
			inStorage = false;
			inOffice = false;
			inArchive = false;
			inLab = false;
			inMachine = false;
			inReaktor = true;
			if (GameObject.Find("MainMusicController") != null)
				GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn (Area);
			else
				GameObject.Find ("MainMusicController(Clone)").GetComponent<MainMusicController> ().FadeIn (Area);
		}	}

	void respawnIn(string Area){
		if (Area == "Office") {
			spawnLocation = EnemySpawnLocations.transform.FindChild ("OfficeSpawnLocation").position;
		} else if (Area == "Storage") {
			spawnLocation = EnemySpawnLocations.transform.FindChild ("StorageSpawnLocation").position;
		} else if (Area == "Lab") {
			spawnLocation = EnemySpawnLocations.transform.FindChild ("LaboratorySpawnLocation").position;
		} else if (Area == "Machine") {
			spawnLocation = EnemySpawnLocations.transform.FindChild ("MachineRoomSpawnLocation").position;
		} else if (Area == "Archive") {
			spawnLocation = EnemySpawnLocations.transform.FindChild ("ArchiveSpawnLocation").position;
		}

		EnemyObject.transform.position = spawnLocation;
		EnemyObject.GetComponent<EnemyRouting> ().getNewWaypoint ();
	}
}
