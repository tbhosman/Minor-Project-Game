using UnityEngine;
using System.Collections;

public class PlayAudioClip : MonoBehaviour {

	public AudioClip audioclip;
	public float playtime;
	public AudioSource audiosource;
	// Use this for initialization
	void Start () {
		audiosource.clip = audioclip;
		audiosource.Pause ();
		if (playtime == 0) {
			audiosource.Play ();
		} else {
			StartCoroutine (PlayAudioSource ());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlayAudioSource(){
		audiosource.Play ();
		yield return new WaitForSeconds (playtime);
		audiosource.Stop ();

	}
}
