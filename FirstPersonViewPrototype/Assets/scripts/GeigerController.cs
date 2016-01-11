/// <summary>
/// Controls the Geiger counter sound and the screen
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeigerController : MonoBehaviour {
	
	private float playerDistance;

	private AudioSource GeigerAudioSource;
	public AudioClip GeigerLevel1;
	public AudioClip GeigerLevel2;
	public AudioClip GeigerLevel3;
	public AudioClip GeigerLevel4;
	public AudioClip GeigerLevel5;
	public AudioClip GeigerLevel6;

	public int audiolevel = 0;
	private bool canPlayAudio = true;
	//public float GeigerParameter;
	public float radioactivityConstant = 2000;
	public float overloadValue = 2000;
	private float radiodistance;
    private float offsetgeiger = 0;

	void Start () {

		GeigerAudioSource = GetComponent<AudioSource> ();
		GeigerAudioSource.clip = GeigerLevel1;
		GeigerAudioSource.loop = true;
		GeigerAudioSource.Play();
        Invoke("setGeigerText", Random.Range(0f, 0.2f));
	}

	void Update () {

		radiodistance = DistanceToClosestRadioactive ();
		
		audiolevel = ChooseAudioClip (radiodistance, audiolevel);
	}

	void setGeigerText(){

		//calculate value to display on screen
        float radioactivityValue = radioactivityConstant / radiodistance + Random.Range(-1f,1f);

        string radioactivityPadded = "";
		string radioactivityUnpadded = "";

		if (radioactivityValue > overloadValue) { // if value is very high, display OVERLD
			radioactivityPadded = " OVERLD";
		} else { //counter is not overloaded
			radioactivityUnpadded = radioactivityValue.ToString ("0.00");
			if (radioactivityUnpadded.Length < 7){ //low value, needs to be padded for digital display
				radioactivityPadded = radioactivityUnpadded.PadLeft(7);
			}
			else if (radioactivityUnpadded.Length == 7){ //value is good, no padding needed
				radioactivityPadded = radioactivityUnpadded;
			}
			else{ //debug for values with more than 7 digits (should not happen)
				radioactivityPadded = " OVERLD";
			}
		}

		//set padded value to the text of the screen
		transform.parent.GetChild (1).GetChild (0).GetComponent<TextMesh> ().text = radioactivityPadded;

		//update text again in a random time (between 0 and 0.2 seconds from current update)
        Invoke("setGeigerText", Random.Range(0f,0.2f));
    }

	// Find radioactive item that is closest to the player
	float DistanceToClosestRadioactive(){

		GameObject[] radioactives;
		radioactives =  GameObject.FindGameObjectsWithTag("radioactive");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject radioactive in radioactives) {
			float curdistance = Vector3.Distance(radioactive.transform.position,transform.position);

			if (curdistance<distance){
				distance = curdistance;
			}
		}

		return distance;
	}

	// check what clicking level to use
 	int ChooseAudioClip(float radiodistance, int previousaudiolevel){
		if (radiodistance < 50 && radiodistance > 40) {
			audiolevel = 2;
			if (audiolevel != previousaudiolevel){
				GeigerAudioSource.clip = GeigerLevel2;
				GeigerAudioSource.Play();
			}
			return audiolevel;
		}
		else if (radiodistance < 40 && radiodistance > 30) {
			audiolevel = 3;
			if (audiolevel != previousaudiolevel){
				GeigerAudioSource.clip = GeigerLevel3;
				GeigerAudioSource.Play();
			}
			return audiolevel;
		}
		else if (radiodistance < 30 && radiodistance > 20) {
			audiolevel = 4;
			if (audiolevel != previousaudiolevel){
			GeigerAudioSource.clip = GeigerLevel4;
			GeigerAudioSource.Play();
			}
				return audiolevel;
		}
		else if (radiodistance < 20 && radiodistance > 10) {
			audiolevel = 5;
			if (audiolevel != previousaudiolevel){
				GeigerAudioSource.clip = GeigerLevel5;
				GeigerAudioSource.Play();
			}
				return audiolevel;
		}
		else if (radiodistance < 10) {
			audiolevel = 6;
			if (audiolevel != previousaudiolevel){
				GeigerAudioSource.clip = GeigerLevel6;
				GeigerAudioSource.Play();
			}
				return audiolevel;
		}
		audiolevel = 1;
		if (audiolevel != previousaudiolevel){
			GeigerAudioSource.clip = GeigerLevel1;
			GeigerAudioSource.Play();
		}
		return audiolevel;

	}
}
