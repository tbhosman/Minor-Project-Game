using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InspectNoteScript : MonoBehaviour {

	public Text InspectInstructions;
	public string displayText;
	private Ray PlayerRay;
	private GameObject Player;
	private RaycastHit RayHit;
	public bool inspected;
	private GameObject Inventory;

	void Start(){
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
		inspected = false;
		Player = GameObject.FindGameObjectWithTag ("Player");

	}
	void OnTriggerStay() {
		PlayerRay = new Ray (Player.transform.position, Player.transform.forward);
		Physics.Raycast (PlayerRay, out RayHit);
		if (RayHit.collider.gameObject == gameObject) {
			if (!inspected) {
				InspectInstructions.text = "Press E to inspect";
				InspectInstructions.enabled = true;
				if (Input.GetKeyDown ("e")) {
					InspectInstructions.enabled = false;
					inspected = true;
					Inventory.GetComponent<InventoryManager>().SecurityCodeNoteFound = true;
					gameObject.SetActive(false);
				}
			}
		}else {
			InspectInstructions.enabled = false;
		}
	}

	void OnTriggerExit(){
		InspectInstructions.enabled = false;
	}


}
