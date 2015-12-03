using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectScript : MonoBehaviour {

	private bool Inspected;
	private GameObject Player;
	private Animator PlayerAnimator;
	public Text InspectInstructions;
	public Animation DontLookAnim;
	private Ray PlayerRay;
	private RaycastHit RayHit;
	public GameObject DontLookMonster;
	public float animationlength;
	// Use this for initialization
	void Start () {
		Inspected = false;
		InspectInstructions.enabled = false;
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerAnimator = Player.GetComponentInChildren<Animator>();
	}
	

	void OnTriggerStay() {
		PlayerRay = new Ray (Player.transform.position, Player.transform.forward);
		Physics.Raycast (PlayerRay, out RayHit);
		if (RayHit.collider.gameObject == gameObject) {
			if (!Inspected) {
				InspectInstructions.text = "Press E to inspect";
				InspectInstructions.enabled = true;
				if (Input.GetKeyDown ("e")) {
					DontLookMonster.SetActive(true);
					InspectInstructions.enabled = false;
					DontLookAnim.Play ();
					PlayerAnimator.SetTrigger ("Inspect");
					Inspected = true;
				}
			}
		}else {
			InspectInstructions.enabled = false;
		}
	}

	IEnumerator disablePlayer(){
	}

	void OnTriggerExit(){
		InspectInstructions.enabled = false;
	}
}
