using UnityEngine;
using System.Collections;

//1: sleutel
//2: koevoet
//3: code note
//4: reaktor deur
//5: scare note

public class DataAquisitie : MonoBehaviour {

	public int timeTaken;
	public string url = "http://drproject.twi.tudelft.nl:8086";

	void Start(){
		WWW www = new WWW (url + "/userid" );  //+ PlayerPrefs.GetInt ("ID"));
		yield return www;
		
		if (www.isDone) {
			int user_id = int.Parse (www.text);
			//PlayerPrefs.SetInt ("ID", user_id); //DEZE MOET ER UIT BIJ TESTEN!
			Debug.Log ("Player ID = " + user_id);	
		}
	}

	public void PickedUpItem (int item) {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		Debug.Log ("Found item: " + item + "  on time: " + timeTaken);
		WWW www_pickUp = new WWW (url + "/pickUp" + item + "?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp;
	}

	public void OpenedDoor (int door) {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		Debug.Log ("Opened door: " + door + "  on time: " + timeTaken);
		WWW www_door = new WWW (url + "/openedDoor" + door + "?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_door;
	}

	public void CompletedGame () {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		WWW www_gameTime = new WWW (url + "/totalGameTime?Time=" + timeTaken + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_gameTime;	
	}

	public void GameOver (Vector3 location) {
		timeTaken = Mathf.RoundToInt(Time.timeSinceLevelLoad/60);
		string location_string = "&Location_x=" + location.x + "&Location_y=" + location.y + "&Location_z=" + location.z;
		WWW www_pickUp = new WWW (url + "/gameOver" + "?Time=" + timeTaken + location_string + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp;	}
} 
