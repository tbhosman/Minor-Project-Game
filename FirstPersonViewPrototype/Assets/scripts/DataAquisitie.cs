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
		WWW www = new WWW (url + "/userid" );  //+ PlayerPrefs.GetInt ("ID"));
		yield return www;
		
		if (www.isDone) {
			int user_id = int.Parse (www.text);
			PlayerPrefs.SetInt ("ID", user_id); //DEZE MOET ER UIT BIJ TESTEN!
			Debug.Log ("Player ID = " + user_id);	
		}

	}

	public void PickedUpItem (int item) {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		SaveLoadManager.GetComponent<SaveLoadScript> ().Save ();
		Debug.Log ("Found item: " + item + "  on time: " + timeTaken);
		StartCoroutine(SendPickedUpItem (item));
	}

	IEnumerator SendPickedUpItem(int item){
		WWW www_pickUp = new WWW (url + "/pickUp" + item + "?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp;
	}

	public void OpenedDoor (int door) {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		SaveLoadManager.GetComponent<SaveLoadScript> ().Save ();
		Debug.Log ("Opened door: " + door + "  on time: " + timeTaken);
		StartCoroutine(SendOpenedDoor (door));
	}

	IEnumerator SendOpenedDoor (int door){
		WWW www_door = new WWW (url + "/openedDoor" + door + "?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_door;
	}

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

	IEnumerator SendCompletedGame(){
		WWW www_gameTime = new WWW (url + "/totalGameTime?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_gameTime;	
	}

	public void GameOver (Vector3 location) {
		savetimeplayed = SaveLoadManager.GetComponent<SaveLoadScript> ().savertimeplayed;
		timeTaken = Mathf.RoundToInt ((Time.timeSinceLevelLoad + savetimeplayed)/ 60);
		SaveLoadManager.GetComponent<SaveLoadScript> ().Save ();
		Debug.Log ("Lost game in " + timeTaken + " minutes");
		StartCoroutine(SendGameOver (location));
	}

	IEnumerator SendGameOver(Vector3 location){
		string location_string = "&Location_x=" + location.x + "&Location_y=" + location.y + "&Location_z=" + location.z;
		WWW www_pickUp = new WWW (url + "/gameOver" + "?Time=" + timeTaken + location_string + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp;
	}
} 