using UnityEngine;
using System.Collections;

//1: sleutel
//2: koevoet
//3: code note
//4: reaktor deur
//5: scare note

public class DataAquisitie : MonoBehaviour {

	public int timeTaken;

	public void PickedUpItem (int item) {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		Debug.Log ("Found item: " + item + "  on time: " + timeTaken);
		//send item ID, player ID and time here
	}

	public void OpenedDoor (int door) {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		Debug.Log ("Opened door: " + door + "  on time: " + timeTaken);
		//send door ID, player ID and time here
	}

	public void CompletedGame () {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		//send player ID and time here
	}

	public void GameOver (Vector3 location) {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		//send death location, player ID and time here
	}
}
