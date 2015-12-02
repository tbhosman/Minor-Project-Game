using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectScript : MonoBehaviour {

	private bool Inspected;
	private GameObject Player;
	private Animator PlayerAnimator;
	public Text InspectInstructions;
	public Animation DontLookAnim;
	// Use this for initialization
	void Start () {
		Inspected = false;
		InspectInstructions.enabled = false;
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerAnimator = Player.GetComponentInParent<Animator>();
	}

	void Update () {
		if (!Inspected) {
			if (Vector3.Distance (gameObject.transform.position, Player.transform.position) < 2) {
				InspectInstructions.enabled = true;
				if (Input.GetKeyDown ("e")) {
					InspectInstructions.enabled = false;
					DontLookAnim.Play ();
					PlayerAnimator.SetTrigger("Trigger");
					Inspected = true;
				}
			}
			if (2 < Vector3.Distance (gameObject.transform.position, Player.transform.position) && Vector3.Distance (gameObject.transform.position, Player.transform.position) < 3){
					InspectInstructions.enabled = false;
			}
		}
	
	}
}
