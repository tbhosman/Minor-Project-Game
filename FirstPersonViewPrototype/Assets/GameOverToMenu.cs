using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverToMenu : MonoBehaviour {

	// Use this for initialization
	void Update(){
		if (Time.timeSinceLevelLoad > 11.0f){
			Application.LoadLevel("menu");
		}
	}
}
