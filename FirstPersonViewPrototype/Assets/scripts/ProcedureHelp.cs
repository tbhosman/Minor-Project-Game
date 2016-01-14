using UnityEngine;
using System.Collections;

public class ProcedureHelp : MonoBehaviour {

	public GameObject Inventory;
	public GameObject Player;
	public GameObject KeysIcon;
	public GameObject SecurityNoteButton;
	public GameObject SaveLoadManager;
	public bool[] KeyObjectsPickedUp;

	void Start(){
		KeyObjectsPickedUp = SaveLoadManager.GetComponent<SaveLoadScript> ().keyObjectsPickedUp;

	}

}
