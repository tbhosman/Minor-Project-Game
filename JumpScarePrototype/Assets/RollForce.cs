using UnityEngine;
using System.Collections;

public class RollForce : MonoBehaviour {
	public float thrust;
	public bool applyforce = false;
	// Use this for initialization
	void Start () {
		applyforce = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (applyforce) {
			gameObject.GetComponent<Rigidbody> ().AddForce(transform.forward * thrust, ForceMode.Impulse);
		}
		applyforce = false;
	}
}
