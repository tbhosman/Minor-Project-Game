/// <summary>
/// Makes pickups interactable for pickup, and gives information about them to the player
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InventoryPickupScript : MonoBehaviour {

	public Text InspectInstructions;
	public string displayText;
	public string afterPickupText;
	private Ray PlayerRay;
	public GameObject Player;
	private RaycastHit RayHit;
	public GameObject Inventory;
	public bool IsKey;
	public bool IsSecurityCodeNote;
	public bool IsScaryNote;
	public GameObject KeyObject;
	public GameObject KeyPicture;
	public GameObject DataAquisitie;
	public bool IsFlashlight;
	public GameObject flashlight;
	public GameObject ProcedureHelp;


	void Start(){
		ProcedureHelp = GameObject.FindGameObjectWithTag ("ProcedureHelp");
        if (IsKey)
        {
            KeyPicture = ProcedureHelp.GetComponent<ProcedureHelp>().KeysIcon;
        }

        if (IsSecurityCodeNote)
		{  
			KeyPicture = ProcedureHelp.GetComponent<ProcedureHelp>().SecurityNoteButton;
        }
		Inventory = ProcedureHelp.GetComponent<ProcedureHelp> ().Inventory;
        InspectInstructions =GameObject.Find("PlayerInfo").GetComponent<Text>(); ;
		Player = ProcedureHelp.GetComponent<ProcedureHelp>().Player;
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

				//check what item is picked up
				if (IsFlashlight){
					Debug.Log ("picked up flashlight");
				}
				if (IsSecurityCodeNote) {
					Inventory.GetComponent<InventoryManager> ().SecurityCodeNoteButtonObject.SetActive (true);
					Debug.Log ("picked up SecurityNote");
					DataAquisitie.GetComponent<DataAquisitie>().PickedUpItem(3);
				}
				if (IsScaryNote) {
					Inventory.GetComponent<InventoryManager> ().ScaryNoteButtonObject.SetActive (true);
					Debug.Log ("picked up ScaryNote");
					DataAquisitie.GetComponent<DataAquisitie>().PickedUpItem(5);
				}
				if (IsKey) {
					Inventory.GetComponent<InventoryManager> ().Key.SetActive (true);
					Debug.Log ("picked up Key");
					DataAquisitie.GetComponent<DataAquisitie>().PickedUpItem(1);
				}
			}
		} else {
			InspectInstructions.enabled = false;
		}
	}

	IEnumerator ObjectPickup(){

		// if flashlight is picked up, start with a flickering light
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

		//show instructions
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
