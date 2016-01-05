using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class mainMenuButtons : MonoBehaviour {

	private InputField input;
	public Canvas optionsCanvas;
	private bool infoOpen = false;
	public GameObject MainMenuMusic;

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

	public void startGame () {
		Application.LoadLevel("LoadingScreen");
	}

	public void openHighscores () {
		Application.LoadLevel("Highscores");
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
