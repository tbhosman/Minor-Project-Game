/// <summary>
/// Starts the alarm audio for the alarm objects as soon as the game is started (from the beginning). 
/// Syncs with the alarm audio that already started during the intro.
/// </summary>

using UnityEngine;
using System.Collections;

public class StartAlarm : MonoBehaviour {

	private GameObject MMC;
	private AudioSource MMCAlarm;
	private AudioSource LocalAlarm;

	// Use this for initialization
	void Start () {
		LocalAlarm = GetComponent<AudioSource> ();

		if (GameObject.Find ("MainMusicController")) {
			MMC = GameObject.Find ("MainMusicController");
		} else {
			MMC = GameObject.Find ("MainMusicController(Clone)");
		}

		if (MMC.transform.FindChild ("Alarm").gameObject.activeSelf) {
			MMCAlarm = MMC.transform.FindChild ("Alarm").GetComponent<AudioSource>();
			LocalAlarm.Play();
		}
	}

	void Update(){
		if (MMC != null && MMCAlarm.isPlaying) {
			LocalAlarm.timeSamples = MMCAlarm.timeSamples;
		}
	}
}
