using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InventoryPickupScript : MonoBehaviour {

	public Text InspectInstructions;
	public string displayText;
	public string afterPickupText;
	private Ray PlayerRay;
	private GameObject Player;
	private RaycastHit RayHit;
	private GameObject Inventory;
	public bool IsKey;
	public bool IsSecurityCodeNote;
	public bool IsScaryNote;
	public GameObject KeyObject;

	void Start(){
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
		Player = GameObject.FindGameObjectWithTag ("Player");

	}
	void OnTriggerStay() {
		PlayerRay = new Ray (Player.transform.position, Player.transform.forward);
		Physics.Raycast (PlayerRay, out RayHit);
		if (RayHit.collider.gameObject == KeyObject) {
			InspectInstructions.text = displayText;
			InspectInstructions.enabled = true;
				if (Input.GetKeyDown ("e")) {
				KeyObject.SetActive(false);
					StartCoroutine(ObjectPickup ());
					if (IsSecurityCodeNote){
					Inventory.GetComponent<InventoryManager>().SecurityCodeNoteButtonObject.SetActive(true);
					}
					if(IsScaryNote){
						Inventory.GetComponent<InventoryManager>().ScaryNoteButtonObject.SetActive(true);
					}
					if(IsKey){
						Inventory.GetComponent<InventoryManager>().KeyFound = true;
					}
				}
		}
	}

	IEnumerator ObjectPickup(){
		InspectInstructions.text = afterPickupText;
		yield return new WaitForSeconds (2);
		InspectInstructions.enabled = false;
		gameObject.SetActive (false);
	}

	void OnTriggerExit(){
		InspectInstructions.enabled = false;
	}


}
