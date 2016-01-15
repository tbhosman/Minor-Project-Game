/// <summary>
/// Controller for the player input into the security panel
/// </summary>

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
	public GameObject MachineRoomDoorTrigger;
    public ParticleSystem steamlinks;
    public ParticleSystem steamrechts;
	
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

		// leaving the security panel
		if (Input.GetKeyDown (KeyCode.Escape)) {
			gameObject.SetActive(false);
			Cursor.visible = false;
		}

		// disable FPS movement if panel is opened
		if (isActiveAndEnabled) {
			GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = false;
		} else {
			GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
		}
	}

	// Function that is triggered on pressing a security panel number
	public void GetDigitInput(Text input){
		if (codeDisplayText.text.Length < 4) {
			codeDisplayText.text += input.text;
			GetComponent<AudioSource>().Play();
			StartCoroutine (UpdateTextField ());
		}
	}

	// Function for undoing a security panel input
	public void UndoInput(){
		if (codeDisplayText.text.Length > 0 && codeDisplayText.text.Length != 4){
			codeDisplayText.text = codeDisplayText.text.Remove(codeDisplayText.text.Length - 1);
			GetComponent<AudioSource>().Play();
			StartCoroutine(UpdateTextField ());
		}
	}

	// Function for updating the digits on the display
	IEnumerator UpdateTextField(){

		// if there are more than 3 digits, a code is inputted
		if (codeDisplayText.text.Length > 3) {

			// if the code is correct, make the display green and wait 2 seconds, then close panel and open the door
			if (codeDisplayText.text == correctCode) {

				GameObject.Find ("MachineRoomDoorTrigger").GetComponent<Collider> ().enabled = false;
				codeDisplayPanel.GetComponent<Image> ().color = Color.green;
				DataAquisitie.GetComponent<DataAquisitie>().OpenedDoor(3);
				yield return StartCoroutine (WaitForRealSeconds (2.0f));
				codeDisplayText.text = "";
				gameObject.SetActive(false);
				Cursor.visible = false;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
                steamlinks.Play();
                steamrechts.Play();
                MachineRoomDoor.GetComponent<Animation>().Play();
				MachineRoomDoor.GetComponent<AudioSource>().Play();
				codeDisplayPanel.GetComponent<Image> ().color = new Color (255, 255, 255, 100);
                
			} else { // if the code was incorrect, display a red screen for 2 seconds, then reset

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
