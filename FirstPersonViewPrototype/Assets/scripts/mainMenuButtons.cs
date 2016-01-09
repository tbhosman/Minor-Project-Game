/// <summary>
/// Manages the buttons in the main menu
/// </summary>
/// 
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
	private SceneFadeInOut SceneFader;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		Destroy (GameObject.Find("MainMusicController"));
		Destroy (GameObject.Find("MainMusicController(Clone)"));

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
		SceneFader = GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ();
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
			SceneFader.scene = "LoadingScreen";
			SceneFader.sceneEnding = true;
			//SceneFader.EndScene("LoadingScreen");
			//Application.LoadLevel("LoadingScreen");
		}
	}

	public void newGameStart(){
		File.Delete (Application.persistentDataPath + "/savefile.sav");
		SceneFader.scene = "LoadingScreen";
		SceneFader.sceneEnding = true;
		//SceneFader.EndScene("LoadingScreen");
		//Application.LoadLevel("LoadingScreen");
	}

	public void openHighscores () {
		SceneFader.scene = "Highscores";
		SceneFader.sceneEnding = true;
		//SceneFader.EndScene("Highscores");
		//Application.LoadLevel("Highscores");
	}

	public void Continue(){
		leveltoload = "prototype1.0";
		SceneFader.scene = "LoadingScreen";
		SceneFader.sceneEnding = true;
		//SceneFader.EndScene("LoadingScreen");
		//Application.LoadLevel ("LoadingScreen");
	}

    public void quitGame () {
		SceneFader.scene = "";
		SceneFader.sceneEnding = true;
		//SceneFader.EndScene("");
		//Application.Quit ();
	}

	public void getInput(string name){
		input = GameObject.Find("InputField").GetComponent<InputField>();
	}

	public void optionsPanel () {
		SceneFader.scene = "options";
		SceneFader.sceneEnding = true;
		//Application.LoadLevel ("options");
		//optionsCanvas.enabled = !optionsCanvas.enabled;
	}

	public void openControls(){
		SceneFader.scene = "Controls";
		SceneFader.sceneEnding = true;
		//SceneFader.EndScene("Controls");
		//Application.LoadLevel ("Controls");
	}
}
