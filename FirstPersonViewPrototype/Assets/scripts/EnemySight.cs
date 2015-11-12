using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemySight : MonoBehaviour {

	public GameObject Player;
	public float fieldOfViewDegrees = 30;
	public float visibilityDistance = 50;
	public bool SeeingPlayer;
	public float deathDistance;
	public float hearDistance;
	public float hearDistanceStanding = 10;
	public float hearDistanceWalking = 50;
	public float hearDistanceRunning = 100;

	void Update(){
		SeeingPlayer = CanSeePlayer();
		float Distance = Vector3.Distance(transform.position, Player.transform.position);

		if ((SeeingPlayer == true)) {
			transform.LookAt(Player.transform.position);

			if (Distance < deathDistance){
				Debug.Log("You died");
				//Game over sequence starts here
			}
		}

		if (CanHearPlayer () == true) {
			Debug.Log ("I can hear you.");
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

}
