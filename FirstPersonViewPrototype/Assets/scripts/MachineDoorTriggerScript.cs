using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MachineDoorTriggerScript : MonoBehaviour {

	public Text PlayerInfo;
	public string	PlayerinfoText;
	public string OnClosedText;
	public string OnOpenText;
	public GameObject Inventory;
	public GameObject SecurityPanelCanvas;
	public GameObject SecurityPanelGameObject;
	private GameObject FPS;
	private Ray PlayerRay;
	private RaycastHit RayHit;
	public GameObject SaveLoadManager;
	public GameObject MachineRoomDoor;
   

	void Start(){
		if (SaveLoadManager.GetComponent<SaveLoadScript> ().DoorOpened [2]) {
			MachineRoomDoor.GetComponent<Animation>().Play();
			gameObject.GetComponent<Collider>().enabled = false;
		};
		FPS = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter(){
		PlayerInfo.text = PlayerinfoText;
	}

	void OnTriggerStay(){
		PlayerRay = new Ray (FPS.transform.position, FPS.transform.forward);
		Physics.Raycast (PlayerRay, out RayHit);
		if (RayHit.collider.gameObject == SecurityPanelGameObject && !SecurityPanelCanvas.activeSelf) {
			PlayerInfo.enabled = true;
			if (Input.GetKeyDown ("e")) {

				if (Inventory.GetComponent<InventoryManager> ().SecurityCodeNoteButtonObject.activeSelf) {
					PlayerInfo.text = OnOpenText;
					PlayerInfo.enabled = false;
					SecurityPanelCanvas.SetActive (true);
					Cursor.visible = true;
				} else {
					PlayerInfo.text = OnClosedText;
				}
			}
		} else
			PlayerInfo.enabled = false;
	}

	void OnTriggerExit(){
		PlayerInfo.enabled = false;
	}

	public IEnumerator OnOpenCoroutine(){
		PlayerInfo.enabled = true;
		yield return new WaitForSeconds (2);
		PlayerInfo.enabled = false;
		gameObject.GetComponent<Collider> ().enabled = false;
	}
}
