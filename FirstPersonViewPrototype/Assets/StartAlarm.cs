/// <summary>
/// Starts the alarm audio for the alarm objects. Syncs with the alarm audio that starts during the intro
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
		}
	}

	void Update(){
		LocalAlarm.timeSamples = MMCAlarm.timeSamples;
		Debug.Log( LocalAlarm.isPlaying);
	}
}
