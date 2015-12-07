using UnityEngine;
using System.Collections;

public class MainAreaTriggerController : MonoBehaviour {

	public bool inStorage;
	public bool inOffice;
	public bool inLab;
	public bool inArchive;
	public bool inMachine;

	// Use this for initialization
	void Start () {
		inStorage = false;
		inOffice = true;
		inArchive = false;
		inLab = false;
		inMachine = false;
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
			GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn ("Office");
		} else if (Area == "Storage" && inStorage == false) {
			inStorage = true;
			inOffice = false;
			inArchive = false;
			inLab = false;
			inMachine = false;
			GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn ("Storage");
		} else if (Area == "Lab" && inLab == false) {
			inStorage = false;
			inOffice = false;
			inArchive = false;
			inLab = true;
			inMachine = false;
			GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn ("Laboratory");
		} else if (Area == "Machine" && inMachine == false) {
			inStorage = false;
			inOffice = false;
			inArchive = false;
			inLab = false;
			inMachine = true;
			GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn ("MachineRoom");
		} else if (Area == "Archive" && inArchive == false) {
			inStorage = false;
			inOffice = false;
			inArchive = true;
			inLab = false;
			inMachine = false;
			GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn ("Archive");
		}
	}
}
