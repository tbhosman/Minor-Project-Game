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
	public GameObject KeyPicture;
	public GameObject DataAquisitie;
	public bool IsFlashlight;
	public GameObject flashlight;
	void Start(){
        if (IsKey)
        {
            KeyPicture = GameObject.Find("KeysIcon");
        }

        if (IsSecurityCodeNote)
        {
            
			KeyPicture = GameObject.Find("SecurityCodeNoteButton");
        }

        InspectInstructions =GameObject.Find("PlayerInfo").GetComponent<Text>(); ;
        Inventory = GameObject.FindGameObjectWithTag ("Inventory");
		Player = GameObject.FindGameObjectWithTag ("Player");
		DataAquisitie = GameObject.Find ("DataAquisitie");

	}
	void OnTriggerStay() {
		PlayerRay = new Ray (Player.transform.position, Player.transform.forward);
		Physics.Raycast (PlayerRay, out RayHit);
		if (RayHit.collider.gameObject == KeyObject) {
			InspectInstructions.text = displayText;
			InspectInstructions.enabled = true;
			if (Input.GetKeyDown ("e")) {
				KeyObject.SetActive (false);
				StartCoroutine (ObjectPickup ());
				gameObject.GetComponent<Collider>().enabled = false;
				if (IsFlashlight){
					Debug.Log ("picked up flashlight");
				}
				if (IsSecurityCodeNote) {
					Inventory.GetComponent<InventoryManager> ().SecurityCodeNoteButtonObject.SetActive (true);
					DataAquisitie.GetComponent<DataAquisitie>().PickedUpItem(3);
				}
				if (IsScaryNote) {
					Inventory.GetComponent<InventoryManager> ().ScaryNoteButtonObject.SetActive (true);
					DataAquisitie.GetComponent<DataAquisitie>().PickedUpItem(5);
				}
				if (IsKey) {
					KeyPicture.SetActive (true);
					DataAquisitie.GetComponent<DataAquisitie>().PickedUpItem(1);
				}
			}
		} else {
			InspectInstructions.enabled = false;
		}
	}

	IEnumerator ObjectPickup(){
		if (IsFlashlight) {
			InspectInstructions.enabled = false;
			Debug.Log ("Light toggled");
			flashlight.SetActive(!flashlight.activeSelf);
			yield return new WaitForSeconds (0.5f);
			flashlight.SetActive(!flashlight.activeSelf);
			yield return new WaitForSeconds (0.4f);
			flashlight.SetActive(!flashlight.activeSelf);
			yield return new WaitForSeconds (0.3f);
			flashlight.SetActive(!flashlight.activeSelf);
			yield return new WaitForSeconds (0.2f);
			flashlight.SetActive(!flashlight.activeSelf);
			yield return new WaitForSeconds (0.1f);
			flashlight.SetActive(!flashlight.activeSelf);
			yield return new WaitForSeconds(0.1f);
			flashlight.SetActive (!flashlight.activeSelf);
		}
		InspectInstructions.enabled = true;
		InspectInstructions.text = afterPickupText;
		yield return new WaitForSeconds (2f);
		InspectInstructions.enabled = false;
		gameObject.SetActive (false);
	}

	void OnTriggerExit(){
		InspectInstructions.enabled = false;
	}


}
