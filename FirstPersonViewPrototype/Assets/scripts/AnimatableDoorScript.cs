/// <summary>
/// This script enables the player to interact with a door if it has the correct key object in its inventory.
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AnimatableDoorScript : MonoBehaviour {

	private Collider DoorTrigger;
	private Animation OpenAnimation;
	private GameObject Inventory;
	public Text PlayerInstructions;
	private string OpenDoorString = "Press E to open door";
	public string DoorClosedString;
	public string AfterOpenString;
	public bool closed;
	public bool Storagedoor;
	public bool Labdoor;
	private Transform DoorAnimation;
	public GameObject Crowbarpicture;
	public GameObject Keypicture;
	private GameObject DataAquisitie;
	public GameObject SaveLoadManager;
	public bool[] doorOpened;
	public GameObject PointLightOfDoor;

	void Start(){

		//Initializing Variables
		DoorAnimation = gameObject.transform.GetChild (0);
		OpenAnimation = DoorAnimation.GetComponent<Animation> ();
		closed = true;
		DoorTrigger = gameObject.GetComponent<Collider> ();
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
		DataAquisitie = GameObject.Find ("DataAquisitie");

		//If loaded this value will change, if not loaded everything is false
		doorOpened = SaveLoadManager.GetComponent<SaveLoadScript> ().DoorOpened;

		// Open doors when they already have been opened in saved game
		if (Storagedoor && doorOpened[1]) {
			gameObject.GetComponent<Collider>().enabled = false;
			OpenAnimation.Play ();
		}
		if (Labdoor && doorOpened [0]) {
			gameObject.GetComponent<Collider>().enabled = false;
			OpenAnimation.Play ();
		}
	}

	void OnTriggerEnter(){

		//Initializing text of playerinstructions
		if (closed) {
			PlayerInstructions.text = OpenDoorString;
			PlayerInstructions.enabled = true;
		}
	}

	void OnTriggerStay(){

		//If interaction key is pressed while in trigger area
		if (Input.GetKeyDown ("e")) {

			//crowbar is needed for the storage
			if (Storagedoor){
				if(Crowbarpicture.activeSelf){
					StartCoroutine(DoorOpening());
					OpenAnimation.Play ();
					closed = false;
					DoorTrigger.enabled = false;
					DataAquisitie.GetComponent<DataAquisitie>().OpenedDoor(2);
				}
				else{
					PlayerInstructions.text = DoorClosedString;
				}
			}

			//key is needed for the lab door
			if (Labdoor){
				if(Keypicture.activeSelf){
					StartCoroutine(DoorOpening());
					OpenAnimation.Play ();
					closed = false;
					DoorTrigger.enabled = false;
					DataAquisitie.GetComponent<DataAquisitie>().OpenedDoor(1);
				}
				else{
					PlayerInstructions.text = DoorClosedString;
				}
			}

			//if the door does not lead to the lab or storage, no key is needed
			if(!Labdoor&&!Storagedoor){
				StartCoroutine(DoorOpening());
				OpenAnimation.Play ();
				closed = false;
				DoorTrigger.enabled = false;
			}
		}
	}

	void OnTriggerExit(){
		PlayerInstructions.enabled = false;
	}

	//Display AfterOpenString for 3 seconds
	IEnumerator DoorOpening(){
		PlayerInstructions.text = AfterOpenString;
		yield return new WaitForSeconds (3);
		PlayerInstructions.enabled = false;
	}

}
