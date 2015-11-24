using UnityEngine;
using System.Collections;

public class JumpScareActivation : MonoBehaviour {

	private RaycastHit RayHit;
	private Ray FirstPersonRay;
	private float triggerDistance;
	private GameObject ScareObject;
	public float VieldOfVanishDegrees;

	// Use this for initialization
	void Start () {
		triggerDistance = Mathf.Infinity;
	}

	// Update is called once per frame
	void Update () {
		FirstPersonRay = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (FirstPersonRay, out RayHit, triggerDistance)){
			if (RayHit.collider.tag == "jumpscare"){
				ScareObject = RayHit.collider.gameObject;
				ScareObject.GetComponent<JumpScareScript>().Scare = true;
			}
		}
	}
}
