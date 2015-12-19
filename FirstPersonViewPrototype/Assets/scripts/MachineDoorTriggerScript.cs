using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MachineDoorTriggerScript : MonoBehaviour {

	public Text PlayerInfo;
	public string	PlayerinfoText;
	public string OnClosedText;
	public string OnOpenText;
	private GameObject Inventory;
	public GameObject SecurityPanelCanvas;
	public GameObject SecurityPanelGameObject;
	private GameObject FPS;
	private Ray PlayerRay;
	private RaycastHit RayHit;


	void Start(){
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
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
					//PlayerInfo.text = OnOpenText;
					PlayerInfo.enabled = false;
					SecurityPanelCanvas.SetActive (true);
					Cursor.visible = true;
					//StartCoroutine (OnOpenCoroutine ());

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

	IEnumerator OnOpenCoroutine(){
		yield return new WaitForSeconds (2);
		PlayerInfo.enabled = false;
		gameObject.GetComponent<Collider> ().enabled = false;
	}
}
