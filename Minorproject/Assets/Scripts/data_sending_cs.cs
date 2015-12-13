using UnityEngine;
using System.Collections;

public class data_sending_cs : MonoBehaviour {
	
	public string url = "http://drproject.twi.tudelft.nl:8086";

	public int gameTime = 24;
	public int pickUp1 = 12;
	public int pickUp2 = 14;
	public int pickUp3 = 22;
	public int pickUp4 = 32;
	public int pickUp5 = 44;
	// Use this for initialization
	
	
	IEnumerator Start() {


	//if (PlayerPrefs.GetInt ("ID") == 0) { //TODO: Moet weer geimplementeerd worden zodra de continue functie werkt, 
		//in deze versie moet bij elke keer het spel starten een nieuwe user_uid aangemaakt worden
		WWW www = new WWW (url + "/userid" );  //+ PlayerPrefs.GetInt ("ID"));
		yield return www;
			
		if (www.isDone) {
			int user_id = int.Parse (www.text);
			PlayerPrefs.SetInt ("ID", user_id); //DEZE MOET ER UIT BIJ TESTEN!
			Debug.Log ("Player ID = " + user_id);	
		}
	//} 

		WWW www_gameTime = new WWW (url + "/totalGameTime?Time=" + gameTime + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_gameTime;
		Debug.Log (www_gameTime.isDone);

		WWW www_pickUp1 = new WWW (url + "/pickUp1?Time=" + pickUp1 + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp1;

		WWW www_pickUp2 = new WWW (url + "/pickUp2?Time=" + pickUp2 + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp2;

		WWW www_pickUp3 = new WWW (url + "/pickUp3?Time=" + pickUp3 + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp3;

		WWW www_pickUp4 = new WWW (url + "/pickUp4?Time=" + pickUp4 + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp4;

		WWW www_pickUp5 = new WWW (url + "/pickUp5?Time=" + pickUp5 + "&User_id=" + PlayerPrefs.GetInt("ID"));
		yield return www_pickUp5;
	
	}


}