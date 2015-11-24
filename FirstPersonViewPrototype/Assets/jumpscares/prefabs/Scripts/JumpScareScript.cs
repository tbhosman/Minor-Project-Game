using UnityEngine;
using System.Collections;

public class JumpScareScript : MonoBehaviour {
	
	private AudioSource audiosource;
	public float scaredistance;
	private GameObject Player;
	public float Scarelength;
	public bool Scare;
	
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<MeshRenderer>().enabled = false;
		Scare = false;
		audiosource = this.GetComponent<AudioSource>();
		audiosource.Pause ();
	}
	
	void Update(){

	if (Scare) {
			if (Vector3.Distance (transform.position, Player.transform.position) < scaredistance) {
				StartCoroutine (ShutdownAfterScareLength ());
				Scare = false;
			}
		}
	}

	IEnumerator ShutdownAfterScareLength(){
		audiosource.UnPause();
		GetComponent<MeshRenderer>().enabled = true;
		yield return new WaitForSeconds(Scarelength);
		gameObject.SetActive (false);
	}

}
