using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


public class mainMenuButtons : MonoBehaviour {

	private InputField input;
	public Canvas optionsCanvas;
	private bool infoOpen = false;
	public GameObject MainMenuMusic;
	public GameObject newGamePopup;
	public GameObject mainButtons;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		Destroy (GameObject.Find("MainMusicController"));

		if (GameObject.Find ("MainMenuMusic(Clone)") == null) {
			Instantiate(MainMenuMusic);
		}
		DontDestroyOnLoad(GameObject.Find("MainMenuMusic(Clone)"));
		Cursor.visible = true;
		//optionsCanvas.enabled = false;
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void BackToMenu(){
		newGamePopup.SetActive (false);
		mainButtons.SetActive (true);
	}
	public void NewGameClick(){
		newGamePopup.SetActive (true);
		mainButtons.SetActive (false);
	}
	public void StartNewGame () {
		File.Delete (Application.persistentDataPath + "/savefile.sav");
		Application.LoadLevel("LoadingScreen");
	}

	public void Continue(){
		if (File.Exists (Application.persistentDataPath + "/savefile.sav")) {
			Application.LoadLevel ("Prototype1.0");
		} else {
			Debug.Log ("no savefile exists yet");
		}
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
}
