using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InspectScript : MonoBehaviour {

	private bool Inspected;
	private GameObject Player;
	private Animator PlayerAnimator;
	public Text InspectInstructions;
	public Animation DontLookAnim;
	private Ray PlayerRay;
	private RaycastHit RayHit;
	public GameObject DontLookMonster;
	public int animationlength;
	private GameObject Inventory;
	public GameObject CrowbarPicture;

	void Start () {
		Inspected = false;
		InspectInstructions.enabled = false;
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerAnimator = Player.GetComponentInChildren<Animator>();
		Inventory = GameObject.FindGameObjectWithTag ("Inventory");
	}
	

	void OnTriggerStay() {
		PlayerRay = new Ray (Player.transform.position, Player.transform.forward);
		Physics.Raycast (PlayerRay, out RayHit);
		if (RayHit.collider.gameObject == gameObject) {
			if (!Inspected) {
				InspectInstructions.text = "Press E to inspect";
				InspectInstructions.enabled = true;
				if (Input.GetKeyDown ("e")) {
					StartCoroutine(Player.GetComponent<FirstPersonController>().Wait(animationlength));
					DontLookMonster.SetActive(true);
					InspectInstructions.enabled = false;
					DontLookAnim.Play ();
					PlayerAnimator.SetTrigger ("Inspect");
					Inspected = true;
					StartCoroutine(OnDontLookAnimationEnd());
				}
			}
		}else {
			InspectInstructions.enabled = false;
		}
	}


	void OnTriggerExit(){
		InspectInstructions.enabled = false;
	}

	IEnumerator OnDontLookAnimationEnd(){
		yield return new WaitForSeconds (animationlength);
		InspectInstructions.enabled = true;
		InspectInstructions.text = "You found a crowbar!!";
		CrowbarPicture.SetActive(true);
		GameObject.Find ("DataAquisitie").GetComponent<DataAquisitie> ().PickedUpItem (2);
		yield return new WaitForSeconds (2);
		InspectInstructions.enabled =false;
	}
}
