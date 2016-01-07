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

	void Start(){
		DoorAnimation = gameObject.transform.GetChild (0);
		OpenAnimation = DoorAnimation.GetComponent<Animation> ();
		closed = true;
		DoorTrigger = gameObject.GetComponent<Collider> ();
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
		DataAquisitie = GameObject.Find ("DataAquisitie");
		doorOpened = SaveLoadManager.GetComponent<SaveLoadScript> ().DoorOpened;
		if (Storagedoor && doorOpened[1]) {
			OpenAnimation.Play ();
		}
		if (Labdoor && doorOpened [0]) {
			Debug.Log ("Playing labdoor animation");
			OpenAnimation.Play ();
		}
	}

	void OnTriggerEnter(){
		if (closed) {
			PlayerInstructions.text = OpenDoorString;
			PlayerInstructions.enabled = true;
		}
	}

	void OnTriggerStay(){
		if (Input.GetKeyDown ("e")) {
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

	IEnumerator DoorOpening(){
		PlayerInstructions.text = AfterOpenString;
		yield return new WaitForSeconds (2);
		PlayerInstructions.enabled = false;
	}

}
