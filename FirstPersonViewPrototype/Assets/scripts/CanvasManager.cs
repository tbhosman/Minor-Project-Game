/// <summary>
/// Manages the inventory and pause canvasses in-game.
/// </summary>

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
	public Image fadeOutPanel;
	public float fadeSpeed = 1.5f;

	void Start () {
		GasMaskOverlay.enabled = false;
		pauseCanvas = transform.FindChild ("PauseMenus").FindChild ("PauseOverlay").gameObject;
		QuitToCanvas = transform.FindChild ("PauseMenus").FindChild ("QuitToOverlay").gameObject;
		inventory = transform.FindChild ("InventoryOverlay").gameObject;
		fadeOutPanel = transform.FindChild ("QuitGameFadePanel").GetComponent<Image>();
		fadeOutPanel.enabled = false;
		pauseCanvas.SetActive (false);
		QuitToCanvas.SetActive (false);
		inventory.SetActive (false);
	}

	void Update(){
		updatePause ();
		updateInventory ();
	}

	// Manage pause canvasses
	public void updatePause(){

		//check if any object is open that should block the pause from opening
		for (int i=0; i<IgnoreOnPause.Length; i++) {
			if (IgnoreOnPause[i].activeSelf){
				pauseCanvas.SetActive(false);
				Time.timeScale = 1;
				return; //pause may not be opened
			}
		}

		//check if the inventory is open
		if (inventory.activeSelf) {
			pauseCanvas.SetActive(false);
			return; //pause may not be opened
		}

		//pause if escape is pressed, open pause menu
		if( Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;

			if (isPause){ //First pause canvas is opened
				Time.timeScale = 0;
				pauseCanvas.SetActive(true);
				Cursor.visible = true;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = false;
				PauseAllAudio();
			}else{ //A pause canvas is opened, check which one
				if (QuitToCanvas.activeSelf){ //second pause canvas is opened, go back to first pause canvas
					pauseCanvas.SetActive(true);
					QuitToCanvas.SetActive(false);
					isPause = !isPause; //must remain in pause state
				}else{ //first pause canvas is opened, resume game
					Time.timeScale = 1;
					pauseCanvas.SetActive(false);
					Cursor.visible = false;
					GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
					UnpauseAllAudio();
				}
			}
		}
	}

	//Manage inventory canvas
	public void updateInventory(){

		//check if any object is open that should block the pause from opening
		for (int i=0; i<IgnoreOnPause.Length; i++) {
			if (IgnoreOnPause[i].activeSelf){
				inventory.SetActive(false);
				Time.timeScale = 1;
				return; //pause cannot be opened
			}
		}

		//check if a pause canvas is open
		if (pauseCanvas.activeSelf || QuitToCanvas.activeSelf) {
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

		// player uses the inventory shortcut
		if (Input.GetKeyDown(KeyCode.I))
		{
			isPause = !isPause;
			
			if (isPause){ // if the game has just been paused, open the inventory
				Time.timeScale = 0;
				inventory.SetActive(true);
				Cursor.visible = true;
				GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = false;
				PauseAllAudio();
			}else{ //if the inventory was already open, resume game
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
		StartCoroutine(fadeOutAndLoad (""));//Application.Quit ();
	}

	public void QuitToMenu(){
		GameObject.Find ("DataAquisitie").GetComponent<DataAquisitie> ().CompletedGame ();
		StartCoroutine(fadeOutAndLoad ("menu"));//Application.LoadLevel ("menu");
	}

	IEnumerator fadeOutAndLoad(string level){
		fadeOutPanel.enabled = true;
		while (fadeOutPanel.color.a < 0.95f){
			fadeOutPanel.color = Color.Lerp(fadeOutPanel.color, Color.black, fadeSpeed * Time.unscaledDeltaTime);
			yield return null; //wait for next frame
		}

		if (level == "") Application.Quit ();
		else Application.LoadLevel (level);

		yield return true;
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
