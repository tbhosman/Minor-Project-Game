using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverToMenu : MonoBehaviour {

	void Start(){
		Destroy (GameObject.Find("MainMusicController"));
	}

	// Use this for initialization
	void Update(){
		if (Time.timeSinceLevelLoad > 10.5f){
			Application.LoadLevel("menu");
		}
	}
}
