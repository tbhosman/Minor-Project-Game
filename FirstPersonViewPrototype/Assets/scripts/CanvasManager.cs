using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CanvasManager : MonoBehaviour {

	public RawImage GasMaskOverlay;
	public GameObject[] IgnoreOnPause = new GameObject[2]; //if one of these objects is active, do not allow pause to open
	private GameObject pauseCanvas;
	private GameObject QuitToCanvas;
	private bool isPause = false;
	private AudioSource[] audios;
	public GameObject inventory;

	void Start () {
		GasMaskOverlay.enabled = false;
		pauseCanvas = transform.FindChild ("PauseMenus").FindChild ("PauseOverlay").gameObject;
		QuitToCanvas = transform.FindChild ("PauseMenus").FindChild ("QuitToOverlay").gameObject;
		inventory = transform.FindChild ("InventoryOverlay").gameObject;
		pauseCanvas.SetActive (false);
		QuitToCanvas.SetActive (false);
		inventory.SetActive (false);
	}

	void Update(){
		Debug.Log (Time.timeScale);
		updatePause ();
		updateInventory ();
	}

	public void updatePause(){
		for (int i=0; i<IgnoreOnPause.Length; i++) {
			if (IgnoreOnPause[i].activeSelf){
				pauseCanvas.SetActive(false);
				Time.timeScale = 1;
				return; //pause cannot be opened
			}
		}

		if (inventory.activeSelf) {
			pauseCanvas.SetActive(false);
			return; //pause cannot be opened
		}

		if( Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;

			if (isPause){
				Time.timeScale = 0;
				pauseCanvas.SetActive(true);
				Cursor.visible = true;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = false;
				PauseAllAudio();
			}else{
				if (QuitToCanvas.activeSelf){
					pauseCanvas.SetActive(true);
					QuitToCanvas.SetActive(false);
					isPause = !isPause; //must remain in pause state
				}else{
					Time.timeScale = 1;
					pauseCanvas.SetActive(false);
					Cursor.visible = false;
					GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
					UnpauseAllAudio();
				}
			}
		}
	}

	public void updateInventory(){
		for (int i=0; i<IgnoreOnPause.Length; i++) {
			if (IgnoreOnPause[i].activeSelf){
				inventory.SetActive(false);
				Time.timeScale = 1;
				return; //pause cannot be opened
			}
		}

		if (pauseCanvas.activeSelf) {
			inventory.SetActive(false);
			return; //pause cannot be opened
		}

		//if a note is opened in the inventory
		if (inventory.transform.FindChild ("SecurityCodeNote").gameObject.activeSelf || inventory.transform.FindChild ("ScareNote").gameObject.activeSelf) {
			if (Input.GetKeyDown(KeyCode.Escape)){ //close note on pressing escape
				inventory.transform.FindChild ("SecurityCodeNote").gameObject.SetActive(false);
				inventory.transform.FindChild ("ScareNote").gameObject.SetActive(false);
			} else if (Input.GetKeyDown (KeyCode.I)){ //resume game on pressing I
				inventory.transform.FindChild ("SecurityCodeNote").gameObject.SetActive(false);
				inventory.transform.FindChild ("ScareNote").gameObject.SetActive(false);
				isPause = !isPause;
				Time.timeScale = 1;
				inventory.SetActive(false);
				Cursor.visible = false;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
				UnpauseAllAudio();
			}
			return;
		} else if (Input.GetKeyDown (KeyCode.Escape) && inventory.activeSelf) { //if pressing escape without notes being open, resume game
			isPause = !isPause;
			Time.timeScale = 1;
			inventory.SetActive(false);
			Cursor.visible = false;
			GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
			UnpauseAllAudio();
		}
		
		if (Input.GetKeyDown(KeyCode.I))
		{
			isPause = !isPause;
			
			if (isPause){
				Time.timeScale = 0;
				inventory.SetActive(true);
				Cursor.visible = true;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = false;
				PauseAllAudio();
			}else{
				Time.timeScale = 1;
				inventory.SetActive(false);
				Cursor.visible = false;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
				UnpauseAllAudio();
			}
		}
	}

	public void resumeFromPause(){
		pauseCanvas.SetActive(false);
		Cursor.visible = false;
		isPause = false;
		Time.timeScale = 1;
		GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
		UnpauseAllAudio ();
	}
	
	public void QuitGame (){
		GameObject.Find ("DataAquisitie").GetComponent<DataAquisitie> ().CompletedGame ();
		Application.Quit ();
	}

	public void QuitToMenu(){
		GameObject.Find ("DataAquisitie").GetComponent<DataAquisitie> ().CompletedGame ();
		Application.LoadLevel ("menu");
	}

	public void QuitFromPause(){
		pauseCanvas.SetActive (false);
		QuitToCanvas.SetActive (true);
	}

	public void BackToPause(){
		pauseCanvas.SetActive (true);
		QuitToCanvas.SetActive (false);
	}

	void PauseAllAudio(){
		audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

		foreach (AudioSource aud in audios) {
			aud.Pause();
		}
	}

	void UnpauseAllAudio(){
		audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

		foreach (AudioSource aud in audios) {
			aud.UnPause();
		}
	}
}
