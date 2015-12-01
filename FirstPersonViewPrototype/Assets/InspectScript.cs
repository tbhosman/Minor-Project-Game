using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectScript : MonoBehaviour {

	private bool Inspected;
	private GameObject Player;
	public Text InspectInstructions;
	public Animation DontLookAnim;
	// Use this for initialization
	void Start () {
		Inspected = false;
		InspectInstructions.enabled = false;
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (!Inspected) {
			if (Vector3.Distance (gameObject.transform.position, Player.transform.position) < 2) {
				InspectInstructions.enabled = true;
				if (Input.GetKeyDown ("e")) {
					InspectInstructions.enabled = false;
					DontLookAnim.Play ();
					Inspected = true;
				}
			}
			if (2 < Vector3.Distance (gameObject.transform.position, Player.transform.position) && Vector3.Distance (gameObject.transform.position, Player.transform.position) < 3){
					InspectInstructions.enabled = false;
			}
		}
	
	}
}
