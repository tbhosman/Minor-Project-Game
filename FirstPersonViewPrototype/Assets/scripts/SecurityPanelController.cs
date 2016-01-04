using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SecurityPanelController : MonoBehaviour {

	public GameObject codeDisplayPanel;
	public Text codeDisplayText;
	public string correctCode;
	public GameObject MachineRoomDoor;
	private GameObject DataAquisitie;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).name == "CodePanel"){
				codeDisplayPanel = transform.GetChild (i).gameObject;
				codeDisplayText = codeDisplayPanel.transform.GetChild(0).GetComponent<Text>();
			}
		}
		DataAquisitie = GameObject.Find ("DataAquisitie");
		codeDisplayText.text = "";
		MachineRoomDoor = GameObject.Find ("MachineRoomDoor");
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			gameObject.SetActive(false);
			Cursor.visible = false;
		}

		if (isActiveAndEnabled) {
			GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = false;
		} else {
			GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
		}
	}

	public void GetDigitInput(Text input){
		if (codeDisplayText.text.Length < 4) {
			codeDisplayText.text += input.text;
			GetComponent<AudioSource>().Play();
			StartCoroutine (UpdateTextField ());
		}
	}

	public void UndoInput(){
		if (codeDisplayText.text.Length > 0 && codeDisplayText.text.Length != 4){
			codeDisplayText.text = codeDisplayText.text.Remove(codeDisplayText.text.Length - 1);
			GetComponent<AudioSource>().Play();
			StartCoroutine(UpdateTextField ());
		}
	}

	IEnumerator UpdateTextField(){
		if (codeDisplayText.text.Length > 3) {
			if (codeDisplayText.text == correctCode) {
				GameObject.Find ("MachineRoomDoorTrigger").GetComponent<Collider> ().enabled = false;
				codeDisplayPanel.GetComponent<Image> ().color = Color.green;
				DataAquisitie.GetComponent<DataAquisitie>().OpenedDoor(3);
				yield return StartCoroutine (WaitForRealSeconds (2.0f));
				codeDisplayText.text = "";
				gameObject.SetActive(false);
				Cursor.visible = false;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
				MachineRoomDoor.GetComponent<Animation>().Play();
				MachineRoomDoor.GetComponent<AudioSource>().Play();
				codeDisplayPanel.GetComponent<Image> ().color = new Color (255, 255, 255, 100);
			}
			else {
				codeDisplayPanel.GetComponent<Image> ().color = Color.red;
				yield return StartCoroutine (WaitForRealSeconds (2.0f));
				codeDisplayText.text = "";
				codeDisplayPanel.GetComponent<Image> ().color = new Color (255, 255, 255, 100);
			}
		}
	}

	private IEnumerator WaitForRealSeconds(float waitTime)
	{
		float endTime = Time.realtimeSinceStartup + waitTime;
		
		while (Time.realtimeSinceStartup < endTime)
		{
			yield return null;
		}
	}

}
