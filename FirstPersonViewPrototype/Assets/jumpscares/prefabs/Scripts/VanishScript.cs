using UnityEngine;
using System.Collections;

public class VanishScript : MonoBehaviour {

	public GameObject player;
	private Ray ObjectToPlayerRay;
	private RaycastHit RayHit;
	public float FieldOfVanishDegree;
	private Vector3 RayDirection;

	void Start () {

	}
	
	void Update(){
		RayDirection = player.transform.position - transform.position;
		ObjectToPlayerRay = new Ray (transform.position, RayDirection);
		if (Physics.Raycast (ObjectToPlayerRay, out RayHit)) {
			if (Vector3.Angle(transform.position-player.transform.position, player.transform.forward) <= FieldOfVanishDegree* 0.5f){
				gameObject.GetComponent<vanishafterseconds>().enabled = true;
			}
		}
	}
}	

