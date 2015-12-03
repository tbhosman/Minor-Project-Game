using UnityEngine;
using System.Collections;

public class JumScareSystem : MonoBehaviour {

	public GameObject ScareObject;
	public AudioClip ScareSound;
	private AudioSource audiosource;
	public float jumpscareseconds;
	
	void Start(){
		audiosource = this.GetComponent<AudioSource> (); 
		gameObject.GetComponent<Animation> ().Stop();
		ScareObject.GetComponent<Animation> ().Stop ();

		//stop looping (walk, run or any) animation on scare object
	}
	
	public void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {		      //play jumpscare animation
			audiosource.clip = ScareSound;                    //set scare sound
			audiosource.Play();
			gameObject.GetComponent<Animation> ().Play ();//play scare sound                     //disable collider for repeat scar     //enable sanity
			ScareObject.GetComponent<Animation> ().Play ();
			StartCoroutine(ScaredWait());            //wait for destroy and sanity
		}
	}
	
	IEnumerator ScaredWait(){
		yield return new WaitForSeconds(jumpscareseconds);
		audiosource.Stop ();
		gameObject.GetComponent<Animation> ().Stop();
		ScareObject.GetComponent<Animation> ().Stop();
		gameObject.SetActive (false);
	}
}

