using UnityEngine;
using System.Collections;

//Object verdwijnt als de player bijnen fieldofvanishdegree kijkt naar het object. Zet noAnimation true als hij geen animatie bevat.
public class VanishScript : MonoBehaviour {

	public GameObject player;
	private Ray ObjectToPlayerRay;
	private RaycastHit RayHit;
	public float FieldOfVanishDegree;
	private Vector3 RayDirection;
	public bool noAnimation;
	public bool ActivateObjectOnLookAt;
	public GameObject ObjectToActivateOnLookAt;

	void Start () {
		if (!noAnimation) {
			gameObject.GetComponent<Animation> ().Stop ();
		}
	}
	
	void Update(){
		RayDirection = player.transform.position - transform.position;
		ObjectToPlayerRay = new Ray (transform.position, RayDirection);
		if (Physics.Raycast (ObjectToPlayerRay, out RayHit)) {
			if (RayHit.collider.gameObject.tag == "Player"){
				if (Vector3.Angle(transform.position-player.transform.position, player.transform.forward) <= FieldOfVanishDegree* 0.5f){
					Debug.Log("VanishActivated");
					if (noAnimation) {
						gameObject.SetActive(false);
					}
					else{
					gameObject.GetComponent<PlayAudioClip>().enabled = true;
					gameObject.GetComponent<Animation>().Play();
					}
					if (ActivateObjectOnLookAt){
						ObjectToActivateOnLookAt.SetActive (true);
					}
				}
			}
		}
	}


	//To use in animation events!!
	void Deactivate(){
		gameObject.SetActive (false);
	}
}	

