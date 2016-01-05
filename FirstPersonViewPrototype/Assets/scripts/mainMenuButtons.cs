using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


public class mainMenuButtons : MonoBehaviour {

	private InputField input;
	public Canvas optionsCanvas;
	private bool infoOpen = false;
	public GameObject MainMenuMusic;
	public GameObject mainbuttons;
	public GameObject newGamePopup;
	public GameObject continuePopup;
	public static string leveltoload;
	public Button continuebutton;
	public Text continuetext;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		Destroy (GameObject.Find("MainMusicController"));

		if (GameObject.Find ("MainMenuMusic(Clone)") == null) {
			Instantiate(MainMenuMusic);
		}
		DontDestroyOnLoad(GameObject.Find("MainMenuMusic(Clone)"));
		Cursor.visible = true;
		if (!File.Exists (Application.persistentDataPath + "/savefile.sav")) {
			continuebutton.interactable = false;
			continuetext.color = new Color(65f/255f,65/255f,65/255f);
			Debug.Log ("continue disbaled");
		}
		else{
			continuetext.color = new Color(174/255f,178/255f,178/255f);
			Debug.Log ("continue enabled");
		}
		//optionsCanvas.enabled = false;
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void BackToMainMenu(){
		continuePopup.SetActive (false);
		newGamePopup.SetActive (false);
		mainbuttons.SetActive (true);
	}

	public void newGameClick(){
		leveltoload = "intro";
		if (File.Exists (Application.persistentDataPath + "/savefile.sav")) {
			newGamePopup.SetActive (true);
			mainbuttons.SetActive (false);
		} else {
			Application.LoadLevel("LoadingScreen");
		}
	}

	public void newGameStart(){
		File.Delete (Application.persistentDataPath + "/savefile.sav");
		Application.LoadLevel("LoadingScreen");
	}

	public void openHighscores () {
		Application.LoadLevel("Highscores");
	}

	public void Continue(){
		leveltoload = "prototype1.0";
		Application.LoadLevel ("LoadingScreen");
	}

    public void quitGame () {
		Application.Quit ();
	}

	public void getInput(string name){
		input = GameObject.Find("InputField").GetComponent<InputField>();
	}

	public void optionsPanel () {
		Application.LoadLevel ("options");
		//optionsCanvas.enabled = !optionsCanvas.enabled;
	}

	public void openControls(){
		Application.LoadLevel ("Controls");
	}
}
