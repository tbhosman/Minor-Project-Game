///<summary>
/// jumpscare in the storage where an enemy suddenly appears
/// </summary>


using UnityEngine;
using System.Collections;

//Object verdwijnt als de player bijnen fieldofvanishdegree kijkt naar het object. Zet noAnimation true als hij geen animatie bevat.
public class VanishScript : MonoBehaviour {

	private GameObject player;
	private Ray ObjectToPlayerRay;
	private RaycastHit RayHit;
	public float FieldOfVanishDegree;
	private Vector3 RayDirection;
	public bool noAnimation;
	public bool ActivateObjectOnLookAt;
	public GameObject ObjectToActivateOnLookAt;
	public bool useDistance;
	public float minDistance;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		if (!noAnimation) {
			gameObject.GetComponent<Animation> ().Stop ();
		}
	}
	
	void Update(){
		RayDirection = player.transform.position - transform.position;
		ObjectToPlayerRay = new Ray (transform.position, RayDirection);

		if (useDistance) {

			if (Vector3.Distance (player.transform.position, gameObject.transform.position) < minDistance) {

				if (Physics.Raycast (ObjectToPlayerRay, out RayHit)) {

					if (RayHit.collider.gameObject.tag == "Player") {

						if (Vector3.Angle (transform.position - player.transform.position, player.transform.forward) <= FieldOfVanishDegree * 0.5f) {

							if (noAnimation) {
								gameObject.SetActive (false);
							} else {
								gameObject.GetComponent<PlayAudioClip> ().enabled = true;
								gameObject.GetComponent<Animation> ().Play ();
							}

							if (ActivateObjectOnLookAt) {
								ObjectToActivateOnLookAt.SetActive (true);
							}
						}
					}
				}
			}
		} else {

			if (Physics.Raycast (ObjectToPlayerRay, out RayHit)) {

				if (RayHit.collider.gameObject.tag == "Player") {

					if (Vector3.Angle (transform.position - player.transform.position, player.transform.forward) <= FieldOfVanishDegree * 0.5f) {

						if (noAnimation) {
							gameObject.SetActive (false);
						} else {
							gameObject.GetComponent<PlayAudioClip> ().enabled = true;
							gameObject.GetComponent<Animation> ().Play ();
						}

						if (ActivateObjectOnLookAt) {
							ObjectToActivateOnLookAt.SetActive (true);
						}
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

