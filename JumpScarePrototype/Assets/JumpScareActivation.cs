using UnityEngine;
using System.Collections;

public class JumpScareActivation : MonoBehaviour {

	private RaycastHit RayHit;
	private Ray FirstPersonRay;
	public float triggerDistance;
	private GameObject ScareObject;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		FirstPersonRay = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (FirstPersonRay, out RayHit, triggerDistance)){
			if (RayHit.collider.tag == "jumpscare"){
				ScareObject = RayHit.collider.gameObject;
				ScareObject.GetComponent<OnActivation>().Scare = true;
			}
			if (RayHit.collider.tag == "VanishObject"){
				ScareObject = RayHit.collider.gameObject;
			}
		}
	}
}
