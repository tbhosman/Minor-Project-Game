using UnityEngine;
using System.Collections;

public class JumpScareScript : MonoBehaviour {
	
	private AudioSource audiosource;
	public bool Scare;
	public float scaredistance;
	private GameObject Player;
	public float Scarelength;
	public static bool triggered;
	
	void Start () {
		triggered = false;
		Player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<MeshRenderer>().enabled = false;
		bool Scare = false;
		audiosource = this.GetComponent<AudioSource>();
		audiosource.Pause ();
	}
	
	void Update(){
		if (triggered){
			gameObject.SetActive(false);
		}
			if (Scare == true) {
				if (Vector3.Distance (transform.position, Player.transform.position)<scaredistance){
					audiosource.UnPause();
					StartCoroutine(ShutdownAfter3());
			}
		}
	}

	IEnumerator ShutdownAfter3(){
		yield return new WaitForSeconds (1.0f);
		GetComponent<MeshRenderer>().enabled = true;
		Scare = false;
		yield return new WaitForSeconds(Scarelength);
		gameObject.SetActive (false);
	}

}
