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

	private int audiolevel = 0;
	private bool canPlayAudio = true;
	public float GeigerParameter;
	public float radioactivityConstant = 2000;
	public float overloadValue = 2000;
	private float radiodistance;

	void Start () {

		GeigerAudioSource = GetComponent<AudioSource> ();
		GeigerAudioSource.clip = GeigerLevel1;
		GeigerAudioSource.loop = true;
		GeigerAudioSource.Play();
	}

	void Update () {

		radiodistance = DistanceToClosestRadioactive ();
		setGeigerText ();
		audiolevel = ChooseAudioClip (radiodistance, audiolevel);
	}

	void setGeigerText(){
		float radioactivityValue = radioactivityConstant / radiodistance;
		string radioactivityPadded = "";
		string radioactivityUnpadded = "";
		if (radioactivityValue > overloadValue) {
			radioactivityPadded = " OVERLD";
		} else { //counter is not overloaded
			radioactivityUnpadded = radioactivityValue.ToString ("0.00");
			if (radioactivityUnpadded.Length < 7){ //low value, needs to be padded for digital display
				radioactivityPadded = radioactivityUnpadded.PadLeft(7);
			}
			else if (radioactivityUnpadded.Length == 7){ //value is good, no padding needed
				radioactivityPadded = radioactivityUnpadded;
			}
			else{ //debug for values with more than 7 digits
				radioactivityPadded = " OVERLD";
			}
		}

		transform.parent.GetChild (1).GetChild (0).GetComponent<TextMesh> ().text = radioactivityPadded;
	}

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
	
 	int ChooseAudioClip(float radiodistance, int previousaudiolevel){
		if (radiodistance < 20 && radiodistance > 12) {
			audiolevel = 2;
			if (audiolevel != previousaudiolevel){
			GeigerAudioSource.clip = GeigerLevel2;
			GeigerAudioSource.Play();
			}
				return audiolevel;
		}
		if (radiodistance < 12 && radiodistance > 8) {
			audiolevel = 3;
			if (audiolevel != previousaudiolevel){
				GeigerAudioSource.clip = GeigerLevel3;
				GeigerAudioSource.Play();
			}
				return audiolevel;
		}
		if (radiodistance < 8) {
			audiolevel = 4;
			if (audiolevel != previousaudiolevel){
				GeigerAudioSource.clip = GeigerLevel4;
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
