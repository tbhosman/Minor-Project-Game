using UnityEngine;
using System.Collections;

//1: sleutel
//2: koevoet
//3: code note
//4: reaktor deur
//5: scare note

public class DataAquisitie : MonoBehaviour {

	public float timeTaken;

	public void PickedUpItem (int item) {
		timeTaken = Time.timeSinceLevelLoad;
		//send item ID, player ID and time here
	}

	public void OpenedDoor (int door) {
		timeTaken = Time.timeSinceLevelLoad;
		//send door ID, player ID and time here
	}

	public void CompletedGame () {
		timeTaken = Time.timeSinceLevelLoad;
		//send player ID and time here
	}

	public void GameOver (Vector3 location) {
		timeTaken = Time.timeSinceLevelLoad;
		//send death location, player ID and time here
	}
}
