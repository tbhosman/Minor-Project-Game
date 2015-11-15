using UnityEngine;
using System.Collections;

public class VanishScript : MonoBehaviour {
	
	private AudioSource audiosource;
	public bool Trigger;

	void Start () {
		Trigger = false;
		audiosource = GetComponent<AudioSource> ();
		gameObject.SetActive (false);
	}
	
	void Update(){
		if (Trigger) {
			gameObject.SetActive(true);
		}
	}
}	

