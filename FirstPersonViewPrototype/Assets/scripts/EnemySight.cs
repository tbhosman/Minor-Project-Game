/// <summary>
/// Keeps track of the senses of the enemy
/// </summary>

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemySight : MonoBehaviour {

	public GameObject Player;
	public float fieldOfViewDegrees = 70;
	public float visibilityDistance = 50;
	public float deathDistance;
	private float hearDistance;
	public float hearDistanceStanding = 10;
	public float hearDistanceWalking = 50;
	public float hearDistanceRunning = 100;
	public bool hearingPlayer;
	public bool seeingPlayer;
	public float canSeeLocationError = 0.05f;
	public bool seeingPlayerLight;
	public float fieldOfViewDegreesPlayerLight = 120;
	public bool canHearClearly;

	void Update(){
		seeingPlayer = CanSeePlayer();
		float Distance = Vector3.Distance(transform.position, Player.transform.position);

		if ((seeingPlayer == true)) {
			//Debug.Log("I can see you.");
		}

		hearingPlayer = CanHearPlayer ();
		if (hearingPlayer) {
			//Debug.Log ("I can hear you.");
		}

		seeingPlayerLight = CanSeePlayerLight ();
		if (seeingPlayerLight) {
			//Debug.Log ("I can see your light.");
		}
	}

	// check if player can be seen
	protected bool CanSeePlayer()
	{
		RaycastHit hit;
		Vector3 rayDirection = Player.transform.position - transform.position;
		
		if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
		{
			// Detect if player is within the field of view
			if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
			{
				return (hit.transform.CompareTag("Player"));
			}
		}
		
		return false;
	}

	// check if player can be heard
	protected bool CanHearPlayer(){

		//check if player is standing, walking or running. Set hearDistance accordingly
		if (Player.GetComponent<FirstPersonController>().MakingWalkingSound == true) {
			hearDistance = hearDistanceWalking;
		} else {
			hearDistance = hearDistanceStanding;
		}

		if (Player.GetComponent<FirstPersonController>().MakingRunningSound == true) {
			hearDistance = hearDistanceRunning;
		}

		float PEdist = Vector3.Distance (Player.transform.position, transform.position);
		if (PEdist < hearDistance / 2) { //hearing player clearly
			canHearClearly = true;
			return true;
		} else if (PEdist < hearDistance) { // hearing player vaguely
			canHearClearly = false;
			return true;
		} else { //cannot hear player
			canHearClearly = false;
			return false;
		}
	}

	// check if player light can be seen
	protected bool CanSeePlayerLight(){
		//check if player has light and if it is on
		if (GameObject.Find("FirstPersonCharacter").transform.GetChild(0).gameObject.activeSelf && GameObject.Find("FirstPersonCharacter").transform.GetChild(0).GetComponent<Light>().enabled){

			if (CanSeeLocation (GameObject.Find("PlayerLight").GetComponent<FlashlightHitArea>().hitDownPosition)){
				return true;
			}
			if (CanSeeLocation (GameObject.Find("PlayerLight").GetComponent<FlashlightHitArea>().hitUpPosition)){
				return true;
			}
			if (CanSeeLocation (GameObject.Find("PlayerLight").GetComponent<FlashlightHitArea>().hitLeftPosition)){
				return true;
			}
			if (CanSeeLocation (GameObject.Find("PlayerLight").GetComponent<FlashlightHitArea>().hitRightPosition)){
				return true;
			}
			if (CanSeeLocation (GameObject.Find("PlayerLight").GetComponent<FlashlightHitArea>().hitMiddlePosition)){
				return true;
			}

		}
		return false;
	}

	// check if enemy can see a certain location
	protected bool CanSeeLocation(Vector3 hitLocation){
		RaycastHit hit;
		Vector3 rayDirection = hitLocation - transform.position;
		
		if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegreesPlayerLight * 0.5f)
		{
			// Detect if player is within the field of view
			if (Physics.Raycast(transform.position, rayDirection, out hit))
			{
				return (Vector3.Distance (hit.point,hitLocation) < canSeeLocationError);
			}
		}
		
		return false;
	}

}
