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
		if (GameObject.Find ("MainMenuMusic(Clone)") == null) {
			Instantiate(MainMenuMusic);
		}
		DontDestroyOnLoad(GameObject.Find("MainMenuMusic(Clone)"));
		//optionsCanvas.enabled = false;
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void startGame () {
		Application.LoadLevel("intro");

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
