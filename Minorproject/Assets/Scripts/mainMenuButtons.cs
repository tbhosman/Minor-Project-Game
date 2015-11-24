using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class mainMenuButtons : MonoBehaviour {

	private InputField input;
	public Canvas optionsCanvas;
	private bool infoOpen = false;

	// Use this for initialization
	void Start () {
		//optionsCanvas.enabled = false;
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void startGame () {
		Application.LoadLevel ("username");

	}

	public void quitGame () {
		Application.Quit ();
	}

	public void getInput(string name){
		input = GameObject.Find("InputField").GetComponent<InputField>();
	}

	public void optionsPanel () {
		//Application.LoadLevel ("");
		//optionsCanvas.enabled = !optionsCanvas.enabled;

	}
}
