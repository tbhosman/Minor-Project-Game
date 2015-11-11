using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public GameObject Player;
	public float fieldOfViewDegrees = 30;
	public float visibilityDistance = 50;
	public bool SeeingPlayer;
	public float deathDistance;

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

}
