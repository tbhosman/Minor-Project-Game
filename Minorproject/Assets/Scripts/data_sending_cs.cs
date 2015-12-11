using UnityEngine;
using System.Collections;

public class data_sending_cs : MonoBehaviour {
	
	public string url = "http://drproject.twi.tudelft.nl:8086/userid";
	
	// Use this for initialization
	
	
	IEnumerator Start() {
		//PlayerPrefs.SetInt ("ID", 0); //als er nog geen is aangemaakt
		
		
		if (PlayerPrefs.GetInt ("ID") == 0) {
			PlayerPrefs.SetInt ("ID", 0);
		} 
		
		WWW www = new WWW (url + "?ID=" + PlayerPrefs.GetInt ("ID"));
		yield return www;

		PlayerPrefs.SetInt ("ID", www.text);
		Debug.Log ("Player ID = " + layerPrefs.SetInt ("ID", www.text));

		//		Debug.Log (www.text);
		//		Debug.Log ("Made it");
		
	}
}