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

	void Start(){
		DoorAnimation = gameObject.transform.GetChild (0);
		OpenAnimation = DoorAnimation.GetComponent<Animation> ();
		closed = true;
		DoorTrigger = gameObject.GetComponent<Collider> ();
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
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
