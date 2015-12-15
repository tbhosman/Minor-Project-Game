using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverToMenu : MonoBehaviour {

	void Start(){
		FadeOutMusic ();
	}

	// Use this for initialization
	void Update(){
		if (Time.timeSinceLevelLoad > 10.5f){
			Application.LoadLevel("menu");
			Destroy (GameObject.Find("MainMusicController"));
		}
	}

	void FadeOutMusic(){
		GameObject.Find ("MainMusicController").GetComponent<MainMusicController> ().FadeIn ("NietBestaandNummer"); //Alle muziek uitfaden
	}
}
