using UnityEngine;
using System.Collections;

public class PlayAudioSourceOnCol : MonoBehaviour {

	private AudioSource audiosource;
	public AudioClip clip;
	private bool play;

	// Use this for initialization
	void Start () {
		play = true;
		audiosource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col){
		if (play){
			PlaySound();
		}
	}

	IEnumerator PlaySound(){
		play = false;
		audiosource.PlayOneShot (clip, 0.5f);
		yield return new WaitForSeconds (0.5f);
		play = true;
	}


}
