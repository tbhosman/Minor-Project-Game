using UnityEngine;
using System.Collections;

public class VanishScript : MonoBehaviour {

	public GameObject player;
	private Ray ObjectToPlayerRay;
	private RaycastHit RayHit;
	public float FieldOfVanishDegree;
	private Vector3 RayDirection;
	public bool deactivate;

	void Start () {
		deactivate = false;
		gameObject.GetComponent<Animation> ().Stop ();

	}
	
	void Update(){
		RayDirection = player.transform.position - transform.position;
		ObjectToPlayerRay = new Ray (transform.position, RayDirection);
		if (Physics.Raycast (ObjectToPlayerRay, out RayHit)) {
			if (RayHit.collider.gameObject.tag == "Player"){
				if (Vector3.Angle(transform.position-player.transform.position, player.transform.forward) <= FieldOfVanishDegree* 0.5f){
					Debug.Log("VanishActivated");
					if (deactivate) {
						gameObject.SetActive(false);
					}
					else{
					gameObject.GetComponent<PlayAudioClip>().enabled = true;
					gameObject.GetComponent<Animation>().Play();
					}
				}
			}
		}
		if (deactivate) {
			gameObject.SetActive(false);
		}
	}
}	

