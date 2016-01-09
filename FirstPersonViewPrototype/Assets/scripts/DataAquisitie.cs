/// <summary>
/// All information of the player that has to be sent to the server is sent here.
/// </summary>

using UnityEngine;
using System.Collections;

//1: sleutel
//2: koevoet
//3: code note
//4: reaktor deur
//5: scare note

public class DataAquisitie : MonoBehaviour {

	public GameObject SaveLoadManager;
	public float savetimeplayed;
	public int timeTaken;
	public string url = "http://drproject.twi.tudelft.nl:8086";

	IEnumerator Start(){
		DontDestroyOnLoad (gameObject);
		WWW www = new WWW (url + "/userid" );  //Get a player ID
		yield return www;
		
		if (www.isDone) {
			//check if a new game is started or if the game is loaded (player ID was saved)
			if(SaveLoadManager.GetComponent<SaveLoadScript>().PlayerID == 0){
			int user_id = int.Parse (www.text);
			PlayerPrefs.SetInt ("ID", user_id);
				Debug.Log ("New Player, ID = " + user_id);
			}
			else{
			int user_id = PlayerPrefs.GetInt("ID");
			Debug.Log ("Player ID = " + user_id);
			}
		}

	}

	// Send information to server if an item was picked up
	public void PickedUpItem (int item) {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		if (item == 1){
			SaveLoadManager.GetComponent<SaveLoadScript>().keyObjectsPickedUp[0] = true;
		};
		if (item == 2) {
			SaveLoadManager.GetComponent<SaveLoadScript>().keyObjectsPickedUp[1] = true;
		}
		if (item == 3) {
			SaveLoadManager.GetComponent<SaveLoadScript>().keyObjectsPickedUp[2] = true;
		}
		SaveLoadManager.GetComponent<SaveLoadScript> ().Save (); //save game on pickup
		Debug.Log ("Found item: " + item + "  on time: " + timeTaken);
		StartCoroutine(SendPickedUpItem (item));
	}

	// The actual sending of the pickup information
	IEnumerator SendPickedUpItem(int item){
		WWW www_pickUp = new WWW (url + "/pickUp" + item + "?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp;
	}

	// Send information to server if a door was opened
	public void OpenedDoor (int door) {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		SaveLoadManager.GetComponent<SaveLoadScript> ().DoorOpened [door - 1] = true;
		SaveLoadManager.GetComponent<SaveLoadScript> ().Save ();
		Debug.Log ("Opened door: " + door + "  on time: " + timeTaken);
		StartCoroutine(SendOpenedDoor (door));
	}

	// The actual sending of the door information
	IEnumerator SendOpenedDoor (int door){
		WWW www_door = new WWW (url + "/openedDoor" + door + "?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_door;
	}

	// Send information to server if game is completed
	public void CompletedGame () {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		SaveLoadManager.GetComponent<SaveLoadScript> ().Save ();
		Debug.Log ("Finished game in " + timeTaken + " minutes");
		if (PlayerPrefs.GetInt ("highscore") > timeTaken) { //set local highscore
			PlayerPrefs.SetInt ("highscore", timeTaken);
		}
		StartCoroutine(SendCompletedGame ());
	}

	// The actual sending of the completion information
	IEnumerator SendCompletedGame(){
		WWW www_gameTime = new WWW (url + "/totalGameTime?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_gameTime;	
	}

	// Send information to server if player has died
	public void GameOver (Vector3 location) {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		Debug.Log ("Lost game in " + timeTaken + " minutes");
		StartCoroutine(SendGameOver (location));
	}

	// The actual sending of the game-over status
	IEnumerator SendGameOver(Vector3 location){
		string location_string = "&Location_x=" + location.x + "&Location_y=" + location.y + "&Location_z=" + location.z;
		WWW www_pickUp = new WWW (url + "/gameOver" + "?Time=" + timeTaken + location_string + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp;
	}
} 