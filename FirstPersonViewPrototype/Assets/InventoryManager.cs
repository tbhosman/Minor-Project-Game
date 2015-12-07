using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	public GameObject SecurityCodeNote;
	public GameObject ScaryNote;

	// Use this for initialization
	void Start () {
		SecurityCodeNote.SetActive (false);
		ScaryNote.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SecurityNoteButton(){
		if (SecurityCodeNote.activeSelf) {
			SecurityCodeNote.SetActive (false);
		} else {
			SecurityCodeNote.SetActive (true);
		}
	}

	public void ScaryNoteButton(){
		if (ScaryNote.activeSelf) {
			ScaryNote.SetActive (false);
		} else {
			ScaryNote.SetActive (true);
		}
	}
}
