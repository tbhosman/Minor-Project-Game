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

	void Start () {
		GasMaskOverlay.enabled = false;
		pauseCanvas = transform.FindChild ("PauseMenus").FindChild ("PauseOverlay").gameObject;
		QuitToCanvas = transform.FindChild ("PauseMenus").FindChild ("QuitToOverlay").gameObject;
		pauseCanvas.SetActive (false);
		QuitToCanvas.SetActive (false);
	}

	void Update(){
		updatePause ();
	}

	public void updatePause(){
		for (int i=0; i<IgnoreOnPause.Length; i++) {
			if (IgnoreOnPause[i].activeSelf){
				pauseCanvas.SetActive(false);
				Time.timeScale = 1;
				return; //pause cannot be opened
			}
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

	public void resumeFromPause(){
		pauseCanvas.SetActive(false);
		Cursor.visible = false;
		isPause = false;
		Time.timeScale = 1;
		GameObject.Find ("FPSController").GetComponent<FirstPersonController>().enabled = true;
		UnpauseAllAudio ();
	}
	
	public void QuitGame (){
		Application.Quit ();
	}

	public void QuitToMenu(){
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
