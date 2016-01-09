/// <summary>
/// Controller for the controls menu of the main screen
/// </summary>

using UnityEngine;
using System.Collections;

public class ControlsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BackToMenu(){
		GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().scene = "menu";
		GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().sceneEnding = true;
		//Application.LoadLevel ("menu");
	}
}
