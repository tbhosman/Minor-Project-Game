using UnityEngine;
using System.Collections;

public class highScore : MonoBehaviour {
	
	float score;
	float highscore;

	// Use this for initialization
	void Start () {
		highscore = PlayerPrefs.GetFloat ("highscore", score);
	}

	void OnDestroy() {
		score = Time.timeSinceLevelLoad;
		if (score > highscore) {
			PlayerPrefs.SetFloat("highscore", score);
		}
	}

}
