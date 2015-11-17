using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class mainMenuButtons : MonoBehaviour {

	private InputField input;
	public Canvas optionsCanvas;
	private bool infoOpen = false;
	public GameObject quitButton;

	// Use this for initialization
	void Start () {
		optionsCanvas.enabled = false;
		quitButton = GameObject.Find ("Quit");
	}

	void OnMouseOver() {
		quitButton.transform.localScale = new Vector3 (2, 2, 2);
		//quitButton.image.rectTransform.sizeDelta = new Vector2 (200, 50); 
		//GUILayout.BeginArea(Rect(Screen.width/4, 70, Screen.width/2, Screen.height/1.5));
		//GUI.Button(new Rect(10, 10, 70, 30), "A button");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame () {
		Application.LoadLevel ("intro");

	}

	public void quitGame () {
		Application.Quit ();
	}

	public void getInput(string name){
		input = GameObject.Find("InputField").GetComponent<InputField>();
	}

	public void optionsPanel () {
		optionsCanvas.enabled = !optionsCanvas.enabled;

	}
}
