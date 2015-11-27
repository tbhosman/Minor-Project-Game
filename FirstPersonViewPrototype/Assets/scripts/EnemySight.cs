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

	void Update(){
		seeingPlayer = CanSeePlayer();
		float Distance = Vector3.Distance(transform.position, Player.transform.position);

		if ((seeingPlayer == true)) {
			//transform.LookAt(Player.transform.position);
			Debug.Log("I can see you.");
		}

		hearingPlayer = CanHearPlayer ();
		if (hearingPlayer) {
			Debug.Log ("I can hear you.");
		}

		seeingPlayerLight = CanSeePlayerLight ();
		if (seeingPlayerLight) {
			Debug.Log ("I can see your light.");
		}
	}

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

	protected bool CanHearPlayer(){

		if (Player.GetComponent<FirstPersonController>().MakingWalkingSound == true) {
			hearDistance = hearDistanceWalking;
		} else {
			hearDistance = hearDistanceStanding;
		}

		if (Player.GetComponent<FirstPersonController>().MakingRunningSound == true) {
			hearDistance = hearDistanceRunning;
		}

		if (Vector3.Distance(Player.transform.position, transform.position) < hearDistance)
		{
			return true;
		}
		return false;
	}

	protected bool CanSeePlayerLight(){
		if (GameObject.Find("FirstPersonCharacter").transform.GetChild(0).GetComponent<Light>().enabled){

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
