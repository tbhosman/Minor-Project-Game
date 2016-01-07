using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InspectScript : MonoBehaviour {

	private AudioSource audiosource;
	private bool Inspected;
	public GameObject Player;
	private Animator PlayerAnimator;
	public Text InspectInstructions;
	public Animation DontLookAnim;
	private Ray PlayerRay;
	private RaycastHit RayHit;
	public GameObject DontLookMonster;
	public int animationlength;
	public GameObject Inventory;
	public GameObject CrowbarPicture;
	public GameObject Crowbar;
	public GameObject SaveLoadManager;

	void Start () {
		if (SaveLoadManager.GetComponent<SaveLoadScript>().keyObjectsPickedUp[1]){
			Crowbar.SetActive(false);
			DontLookAnim.Play ();
			gameObject.GetComponent<Collider>().enabled = false;
		}
		audiosource = gameObject.GetComponent<AudioSource> ();
		Inspected = false;
		InspectInstructions.enabled = false;
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
					audiosource.Play();
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
		InspectInstructions.text = "You found a crowbar! Press i to inspect your inventory";
		Crowbar.SetActive (false);
		CrowbarPicture.SetActive(true);
		GameObject.Find ("DataAquisitie").GetComponent<DataAquisitie> ().PickedUpItem (2);
		yield return new WaitForSeconds (2);
		InspectInstructions.enabled =false;
	}
}
